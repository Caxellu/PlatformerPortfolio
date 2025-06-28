using UnityEngine;
using Zenject;

public class LevelSceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private BulletFactory _bulletFactory;
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
        Container.DeclareSignal<EnemyCollisionSignal>();
        Container.DeclareSignal<PlayerDeadSignal>();
        Container.DeclareSignal<BulletHitSignal>();

        Container.Bind<EnemyFactory>().AsSingle().NonLazy();
        Container.Bind<BulletController>().AsSingle().NonLazy();
        Container.Bind<BulletFactory>().FromInstance(_bulletFactory).AsSingle().NonLazy();
        Container.Bind<EnemyManager>().FromInstance(_enemyManager).AsSingle().NonLazy();
        Container.Bind<Health>().FromInstance(_playerHealth).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerController>().FromInstance(_playerController).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerInputController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<LevelController>().AsSingle().NonLazy();
    }
}