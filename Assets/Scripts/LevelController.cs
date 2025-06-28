using System;
using UnityEngine;
using Zenject;
public class LevelController : IInitializable, IDisposable
{
    [Inject] private SignalBus _signalBus;
    [Inject] private Health _playerHealth;
    public void Initialize()
    {
        //_signalBus.Subscribe<PauseSignal>();
        _playerHealth.Initialize(() => _signalBus.Fire<PlayerDeadSignal>()) ;

        _signalBus.Subscribe<PlayerDeadSignal>(PlayerDead);
        _signalBus.Subscribe<EnemyCollisionSignal>(PlayerTakeDamage);
    }
    private void PlayerTakeDamage(EnemyCollisionSignal arg)
    {
        _playerHealth.TakeDamage(arg.Damage);
    }
    private void PlayerDead()
    {
        Debug.Log("Dead");
    }
    public void Dispose()
    {
    }
}
