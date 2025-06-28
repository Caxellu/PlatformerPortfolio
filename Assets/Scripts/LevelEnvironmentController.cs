using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class LevelEnvironmentController : MonoBehaviour
{
    [Inject] private EnemyManager _enemyManager;
    [SerializeField] private EnemyController _enemyPrefab;
    [SerializeField] private PatrolPath _patrolPathPrefab;

    [SerializeField] private Transform _spawnPlayerTr;
    [SerializeField] private Grid _grid;
    [SerializeField] private Transform _enemyParentTr;
    [SerializeField] private Transform _patrolsPathParentTr;
    [SerializeField] private Transform _levelParentTr;
    LevelSO _levelSO;

    private List<EnemyController> spawnedEnemy = new List<EnemyController>();
    public void Initialize(LevelSO levelSO)
    {
        _levelSO = levelSO;
        SetUpLevelEnvironment(_levelSO);
        _enemyManager.EnemyList = spawnedEnemy;
    }
    private void SetUpLevelEnvironment(LevelSO levelSO)
    {
        foreach (Tilemap tilemap in levelSO.Tilemaps)
        {
            GameObject.Instantiate(tilemap, _grid.transform);
        }

        _spawnPlayerTr.transform.position = levelSO.spawnPlayerPos;

        foreach (EnemyDTO dTO in levelSO.enemyDTOs)
        {
            PatrolPath patrolPath = GameObject.Instantiate(_patrolPathPrefab, _patrolsPathParentTr);
            patrolPath.transform.position = dTO.PatrolPath.globalPosition;
            patrolPath.startPosition = dTO.PatrolPath.startPosition;
            patrolPath.endPosition = dTO.PatrolPath.endPosition;

            EnemyController enemy = GameObject.Instantiate(_enemyPrefab, _enemyParentTr);
            enemy.PreInitialize(dTO.EnemyType, patrolPath);
            spawnedEnemy.Add(enemy);
        }
        GameObject.Instantiate(levelSO.LevelCompleteObj, _levelParentTr);
        GameObject.Instantiate(levelSO.CameraPlygonCollider, _levelParentTr);
    }
}
