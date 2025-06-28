using System;
using UnityEngine;
using Zenject;
public class LevelController :  IInitializable, IDisposable
{
    [Inject] private SignalBus _signalBus;
    [Inject] private Health _playerHealth;
    [Inject] private EnemyFactory _enemyFactory;
    [Inject] private EnemyManager _enemyManager;
    [Inject] private EnemyService _enemyService;
    [Inject] private PlayerController _playerController;
    [Inject] private PlayerConfigService _playerConfigService;
    [Inject] private BulletFactory _bulletFactory;
    [Inject] private BulletController _bulletController;
    [Inject] private LevelEnvironmentController _levelEnvironmentController;
    [Inject] private IPopupsController _popupsController;
    [Inject] private LevelService _levelService;
    public void Initialize()
    {
        _levelEnvironmentController.Initialize(_levelService.CurrentLevelSo);
        _playerHealth.Initialize(() => _signalBus.Fire<PlayerDeadSignal>(),()=>_playerController.Hurt(), _playerConfigService.Config.maxHp);
        _enemyFactory.Initialize(_enemyService.EnemyList);
        _enemyManager.Initialize(_enemyFactory);
        _bulletFactory.Initialize(_playerConfigService.Config.bulletPrefab, _playerConfigService.Config.hitPrefab);
        _bulletController.Initialize(_playerConfigService.Config.bulletSpeed, _playerConfigService.Config.bulletDamage, _playerConfigService.Config.fireCooldown, _playerConfigService.Config.startAmmo);
        _playerController.Initialize(_playerConfigService.Config.speed, _playerConfigService.Config.jumpTakeOffSpeed, _playerConfigService.Config.fireDuration);

        _signalBus.Subscribe<PauseSignal>(Pause);
        _signalBus.Subscribe<UnPauseSignal>(UnPause);
        _signalBus.Subscribe<PlayerDeadSignal>(PlayerDead);
        _signalBus.Subscribe<LevelCompleteSignal>(LevelComplete);
        _signalBus.Subscribe<EnemyCollisionSignal>(PlayerTakeDamage);
    }
    private void LevelComplete()
    {
        _signalBus.Fire<FreezeSignal>();
        _popupsController.ShowPopup(LevelPopupType.LevelComplete,true);
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
    private void PlayerTakeDamage(EnemyCollisionSignal arg)
    {
        _playerHealth.TakeDamage(arg.Damage);
    }
    private void PlayerDead()
    {
        _playerController.Die();
    }
    public void Dispose()
    {
    }
}
