using UnityEngine;
using Zenject;

public class LevelSceneInstaller : MonoInstaller
{
    [SerializeField] private BulletFactory _bulletFactory;
    [SerializeField] private LevelEnvironmentController _levelEnvironmentController;
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerMovement _playerMovement;
    public override void InstallBindings()
    {
        Container.DeclareSignal<StartRightMoveSignal>();
        Container.DeclareSignal<StopRightMoveSignal>();
        Container.DeclareSignal<StartLeftMoveSignal>();
        Container.DeclareSignal<StopLeftMoveSignal>();
        Container.DeclareSignal<JumpSignal>();
        Container.DeclareSignal<TryFireSignal>();
        Container.DeclareSignal<FireSignal>();
        Container.DeclareSignal<PauseSignal>();
        Container.DeclareSignal<UnPauseSignal>();
        Container.DeclareSignal<PlayerDeadSignal>();
        Container.DeclareSignal<BulletHitSignal>();
        Container.DeclareSignal<UpdateAmmoSignal>();
        Container.DeclareSignal<LevelCompleteSignal>();
        Container.DeclareSignal<FreezeSignal>();
        Container.DeclareSignal<UnFreezeSignal>();
        Container.DeclareSignal<RestartSignal>();


        Container.Bind<IPlayerView>().FromInstance(_playerView).AsSingle().NonLazy();
        Container.Bind<IBullletSpawnPos>().FromInstance(_playerView).AsSingle().NonLazy();
        Container.Bind<IPlayerDirection>().FromInstance(_playerView).AsSingle().NonLazy();
        Container.Bind<IPlayerPos>().FromInstance(_playerView).AsSingle().NonLazy();

        Container.Bind<IPlayerMovement>().FromInstance(_playerMovement).AsSingle().NonLazy();
        Container.BindInterfacesTo<PopupsController<LevelPopupType>>().AsSingle().NonLazy();
        Container.Bind<EnemyFactory>().FromInstance(_enemyFactory).AsSingle().NonLazy();
        Container.Bind<LevelEnvironmentController>().FromInstance(_levelEnvironmentController).AsSingle().NonLazy();
        Container.Bind<IBulletFactory>().FromInstance(_bulletFactory).AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerInputController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<LevelController>().AsSingle().NonLazy();
    }
}