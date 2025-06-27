using UnityEngine;
using Zenject;

public class LevelSceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerController _playerController;
    public override void InstallBindings()
    {
        Container.DeclareSignal<StartRightMoveSignal>();
        Container.DeclareSignal<StopRightMoveSignal>();
        Container.DeclareSignal<StartLeftMoveSignal>();
        Container.DeclareSignal<JumpSignal>();
        Container.DeclareSignal<FireSignal>();
        Container.DeclareSignal<PauseSignal>();


        Container.Bind<PlayerController>().FromInstance(_playerController).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerInputController>().AsSingle().NonLazy();
    }
}