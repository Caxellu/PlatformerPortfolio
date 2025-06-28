using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<EnemyController> _enemyList;
    private EnemyFactory _enemyFactory;
    public void Initialize(EnemyFactory enemyFactory)
    {
        _enemyFactory = enemyFactory;
        foreach (EnemyController enemy in _enemyList)
        {
            enemy.Initialize(_enemyFactory.Get(enemy.EnemyType));
        }
    }
}
