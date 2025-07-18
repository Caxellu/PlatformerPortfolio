using System;
using Zenject;
public class LevelController : IInitializable, IDisposable
{
    private readonly SignalBus _signalBus;
    private readonly PlayerConfigService _playerConfigService;
    private readonly IBulletFactory _bulletFactory;
    private readonly LevelEnvironmentController _levelEnvironmentController;
    private readonly IPopupsController _popupsController;
    private readonly LevelService _levelService;
    private readonly ICoroutineManager _coroutineManager;
    private readonly PlayerInputController _playerInputController;
    private readonly EnemyFactory _enemyFactory;
    private readonly IPlayerView _playerView;
    private readonly IPlayerPos _playerPos;
    private readonly IPlayerMovement _playerMovement;
    private readonly IBullletSpawnPos _bullletSpawnPos;
    private readonly IUnitDirection _playerDirection;

    public LevelController(
        SignalBus signalBus,
        PlayerConfigService playerConfigService,
        IBulletFactory bulletFactory,
        LevelEnvironmentController levelEnvironmentController,
        IPopupsController popupsController,
        LevelService levelService,
        ICoroutineManager coroutineManager,
        PlayerInputController playerInputController,
        EnemyFactory enemyFactory,
        IPlayerView playerView,
        IPlayerPos playerPos,
        IPlayerMovement playerMovement,
        IBullletSpawnPos bullletSpawnPos,
        IUnitDirection playerDirection)
    {
        _signalBus = signalBus;
        _playerConfigService = playerConfigService;
        _bulletFactory = bulletFactory;
        _levelEnvironmentController = levelEnvironmentController;
        _popupsController = popupsController;
        _levelService = levelService;
        _coroutineManager = coroutineManager;
        _playerInputController = playerInputController;
        _enemyFactory = enemyFactory;
        _playerView = playerView;
        _playerPos = playerPos;
        _playerMovement = playerMovement;
        _bullletSpawnPos = bullletSpawnPos;
        _playerDirection = playerDirection;
    }
    public void Initialize()
    {
        _bulletFactory.Initialize(_playerConfigService.Config.bulletPrefab, _playerConfigService.Config.hitPrefab, _signalBus);

        PlayerModel playerModel = new PlayerModel(_playerConfigService.Config.speed, _playerConfigService.Config.jumpTakeOffSpeed, _playerConfigService.Config.maxHp, _levelService.CurrentLevelSo.spawnPlayerPos);
        _playerView.Initialize(_playerConfigService.Config.fireDuration, _coroutineManager);
        PlayerPresenter playerPresenter = new PlayerPresenter(playerModel, _playerView, _playerMovement, _signalBus);
        

        EnemyPresenter enemyPresenter = new EnemyPresenter(_enemyFactory, _playerPos, _signalBus);
        
        _levelEnvironmentController.Initialize(_levelService.CurrentLevelSo, enemyPresenter);
        PlayerFireUseCase playerFireUseCase = new PlayerFireUseCase(_signalBus, _bulletFactory, _bullletSpawnPos, _playerDirection, _coroutineManager);
        playerFireUseCase.Initialize(_playerConfigService.Config.bulletSpeed, _playerConfigService.Config.bulletDamage, _playerConfigService.Config.fireCooldown, _playerConfigService.Config.startAmmo);

        _playerInputController.Initialize(_playerMovement);


        _signalBus.Subscribe<TryFireSignal>(playerFireUseCase.TryFire);
        _signalBus.Subscribe<PauseSignal>(Pause);
        _signalBus.Subscribe<UnPauseSignal>(UnPause);
        _signalBus.Subscribe<PlayerDeadSignal>(PlayerDead);
        _signalBus.Subscribe<LevelCompleteSignal>(LevelComplete);
    }
    private void LevelComplete()
    {
        _signalBus.Fire<FreezeSignal>();
        _popupsController.ShowPopup(LevelPopupType.LevelComplete, true);
    }
    private void Pause()
    {
        _popupsController.ShowPopup(LevelPopupType.Pause, "Level 1");
        _signalBus.Fire<FreezeSignal>();
    }
    private void UnPause()
    {
        _signalBus.Fire<UnFreezeSignal>();
    }
    private void PlayerDead()
    {
        _popupsController.ShowPopup(LevelPopupType.GameOver, "Level " + _levelService.CurrentLevelSo.LevelIndex);
    }
    public void Dispose()
    {
        _signalBus.Unsubscribe<PauseSignal>(Pause);
        _signalBus.Unsubscribe<UnPauseSignal>(UnPause);
        _signalBus.Unsubscribe<PlayerDeadSignal>(PlayerDead);
        _signalBus.Unsubscribe<LevelCompleteSignal>(LevelComplete);
    }
}
