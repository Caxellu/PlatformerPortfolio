using UnityEngine;
using Zenject;

public class LevelSceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private BulletFactory _bulletFactory;
    [SerializeField] private LevelEnvironmentController _levelEnvironmentController;
    [SerializeField] private EnemyFactory _enemyFactory;
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
        Container.DeclareSignal<EnemyCollisionSignal>();
        Container.DeclareSignal<PlayerDeadSignal>();
        Container.DeclareSignal<BulletHitSignal>();
        Container.DeclareSignal<UpdateAmmoSignal>();
        Container.DeclareSignal<LevelCompleteSignal>();
        Container.DeclareSignal<FreezeSignal>();
        Container.DeclareSignal<UnFreezeSignal>();
        


        Container.BindInterfacesTo<PopupsController<LevelPopupType>>().AsSingle().NonLazy();
        Container.Bind<BulletController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<EnemyFactory>().FromInstance(_enemyFactory).AsSingle().NonLazy();
        Container.Bind<LevelEnvironmentController>().FromInstance(_levelEnvironmentController).AsSingle().NonLazy();
        Container.Bind<BulletFactory>().FromInstance(_bulletFactory).AsSingle().NonLazy();
        Container.Bind<Health>().FromInstance(_playerHealth).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerController>().FromInstance(_playerController).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerInputController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<LevelController>().AsSingle().NonLazy();
    }
}