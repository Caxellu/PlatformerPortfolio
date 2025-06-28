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
    [Inject] private PlayerController _playerController;
    [Inject] private PlayerConfigService _playerConfigService;
    [Inject] private BulletFactory _bulletFactory;
    [Inject] private BulletController _bulletController;

    public void Initialize()
    {
        //_signalBus.Subscribe<PauseSignal>();
        _playerHealth.Initialize(() => _signalBus.Fire<PlayerDeadSignal>(), _playerConfigService.Config.maxHp);
        _enemyFactory.Initialize(_enemyService.EnemyList);
        _enemyManager.Initialize(_enemyFactory);
        _bulletFactory.Initialize(_playerConfigService.Config.bulletPrefab, _playerConfigService.Config.hitPrefab);
        _bulletController.Initialize(_playerConfigService.Config.bulletSpeed, _playerConfigService.Config.bulletDamage);
        _playerController.Initialize(_playerConfigService.Config.speed, _playerConfigService.Config.jumpTakeOffSpeed, _playerConfigService.Config.fireCooldown, _playerConfigService.Config.fireDuration);

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
