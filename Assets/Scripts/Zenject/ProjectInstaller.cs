using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private GlobalGameController _globalGameController;
    [SerializeField] private CoroutineManager _corutineManager;
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<NextLevelSignal>();
        
        Container.Bind<ICoroutineManager>().FromInstance(_corutineManager).AsSingle().NonLazy();
        Container.Bind<SceneLoaderController>().AsSingle().NonLazy();
        Container.Bind<LevelService>().AsSingle().NonLazy();
        Container.Bind<EnemyService>().AsSingle().NonLazy();
        Container.Bind<PlayerConfigService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GlobalGameController>().FromInstance(_globalGameController).AsSingle().NonLazy();
    }
}
