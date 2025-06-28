using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<EnemyController> EnemyList;
    private EnemyFactory _enemyFactory;
    public void Initialize(EnemyFactory enemyFactory)
    {
        _enemyFactory = enemyFactory;
        foreach (EnemyController enemy in EnemyList)
        {
            enemy.Initialize(_enemyFactory.Get(enemy.EnemyType));
        }
    }
}
