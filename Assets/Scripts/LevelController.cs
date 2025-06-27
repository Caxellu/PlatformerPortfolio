using System;
using Zenject;
public class LevelController : IInitializable, IDisposable
{
    [Inject] private SignalBus _signalBus;
    public void Initialize()
    {
        //_signalBus.Subscribe<PauseSignal>();
    }
    public void Dispose()
    {
    }
}
