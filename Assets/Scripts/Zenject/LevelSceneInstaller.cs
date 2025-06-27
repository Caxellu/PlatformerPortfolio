using Zenject;

public class LevelSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.DeclareSignal<StartRightMoveSignal>();
        Container.DeclareSignal<StopRightMoveSignal>();
        Container.DeclareSignal<StartLeftMoveSignal>();
        Container.DeclareSignal<JumpSignal>();
        Container.DeclareSignal<StartFireSignal>();
        Container.DeclareSignal<StopFireSignal>();
        Container.DeclareSignal<PauseSignal>();

    }
}