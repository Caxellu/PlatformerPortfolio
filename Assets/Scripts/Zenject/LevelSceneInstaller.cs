using UnityEngine;
using Zenject;

public class LevelSceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private EnemyManager _enemyManager;
    public override void InstallBindings()
    {
        Container.DeclareSignal<StartRightMoveSignal>();
        Container.DeclareSignal<StopRightMoveSignal>();
        Container.DeclareSignal<StartLeftMoveSignal>();
        Container.DeclareSignal<StopLeftMoveSignal>();
        Container.DeclareSignal<JumpSignal>();
        Container.DeclareSignal<FireSignal>();
        Container.DeclareSignal<PauseSignal>();
        Container.DeclareSignal<EnemyCollisionSignal>();
        Container.DeclareSignal<PlayerDeadSignal>();
        
        Container.Bind<EnemyFactory>().AsSingle().NonLazy();
        Container.Bind<EnemyManager>().FromInstance(_enemyManager).AsSingle().NonLazy();
        Container.Bind<Health>().FromInstance(_playerHealth).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerController>().FromInstance(_playerController).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerInputController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<LevelController>().AsSingle().NonLazy();
    }
}