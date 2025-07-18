using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyFactory : MonoBehaviour
{
    [Inject] private DiContainer _container;
    [Inject] private EnemyService _enemyService;

    [SerializeField] private GameObject EnemyPrefab;
    public IEnemyView Spawn(EnemyType type, Vector3 pos, Transform parent, out EnemyModel model, out IEnemyMovement enemyMovement)
    {
        var enemySO = _enemyService.GetByType(type);
        if (enemySO == null)
        {
            model = new EnemyModel(Vector3.zero, 0, 0,0);
            enemyMovement = null;
            Debug.LogError($"No EnemySO found for type {type}");
            return null;
        }


        var go = _container.InstantiatePrefab(EnemyPrefab, pos, Quaternion.identity, parent);
        var view = go.GetComponent<IEnemyView>();
        view.Initialize(enemySO.AnimatorController);
        enemyMovement = go.GetComponent<IEnemyMovement>();    
        model = new EnemyModel(pos, enemySO.MaxHp, enemySO.Speed, enemySO.Damage);
        return view;
    }
}
