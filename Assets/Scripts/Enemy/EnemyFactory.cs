using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyFactory : MonoBehaviour
{
    [Inject] private DiContainer _container;
    [Inject] private EnemyService _enemyService;
    [Inject] private SignalBus _signalBus;

    private IPlayerPos _playerPos;

    [SerializeField] private GameObject EnemyPrefab;
    public void Construct(IPlayerPos playerPos)
    {
        _playerPos = playerPos;
    }
    public void Spawn(EnemyType type, PatrolPath path, Vector3 pos, Transform parent)
    {
        var enemySO = _enemyService.GetByType(type);
        if (enemySO == null)
        {
            Debug.LogError($"No EnemySO found for type {type}");
            return;
        }

        
        var go = _container.InstantiatePrefab(EnemyPrefab, pos, Quaternion.identity,parent);
        var view = go.GetComponent<EnemyView>();

        var data = new EnemySO
        {
            Type = enemySO.Type,
            Speed = enemySO.Speed,
            Damage = enemySO.Damage,
            MaxHp = enemySO.MaxHp,
            AnimatorController = enemySO.AnimatorController
        };

        var logic = new EnemyControllerLogic(view, data, _playerPos, _signalBus, path);
        view.Construct(logic);
    }
}
