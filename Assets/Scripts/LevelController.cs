using System;
using UnityEngine;
using Zenject;
public class LevelController : IInitializable, IDisposable
{
    [Inject] private SignalBus _signalBus;
    [Inject] private Health _playerHealth;
    [Inject] private EnemyFactory _enemyFactory;
    [Inject] private EnemyManager _enemyManager;
    [Inject] private EnemyService _enemyService;

    public void Initialize()
    {
        //_signalBus.Subscribe<PauseSignal>();
        _playerHealth.Initialize(() => _signalBus.Fire<PlayerDeadSignal>()) ;
        _enemyFactory.Initialize(_enemyService.EnemyList);
        _enemyManager.Initialize(_enemyFactory);

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
