using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private GlobalGameController _globalGameController;
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container); 
        Container.Bind<SceneLoaderController>().AsSingle().NonLazy();
        Container.Bind<EnemyService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GlobalGameController>().FromInstance(_globalGameController).AsSingle().NonLazy();
    }
}
