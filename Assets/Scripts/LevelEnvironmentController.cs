using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class LevelEnvironmentController : MonoBehaviour
{
    [Inject] private DiContainer _container;
    [Inject] private EnemyFactory _enemyFactory;
    [SerializeField] private CinemachineConfiner _confiner;
    [Space]
    [SerializeField] private PatrolPath _patrolPathPrefab;
    [Space]
    [SerializeField] private Transform _spawnPlayerTr;
    [SerializeField] private Grid _grid;
    [SerializeField] private Transform _enemyParentTr;
    [SerializeField] private Transform _patrolsPathParentTr;
    [SerializeField] private Transform _levelParentTr;
    LevelSO _levelSO;

    public void Initialize(LevelSO levelSO)
    {
        _levelSO = levelSO;
        SetUpLevelEnvironment(_levelSO);
    }
    private void SetUpLevelEnvironment(LevelSO levelSO)
    {
        foreach (Tilemap tilemap in levelSO.Tilemaps)
        {
            GameObject.Instantiate(tilemap, _grid.transform);
        }

        _spawnPlayerTr.transform.position = levelSO.spawnPlayerPos;

        foreach (EnemyMoveDTO dTO in levelSO.enemyDTOs)
        {
            PatrolPath patrolPath = GameObject.Instantiate(_patrolPathPrefab, _patrolsPathParentTr);
            patrolPath.transform.position = dTO.PatrolPath.globalPosition;
            patrolPath.startPosition = dTO.PatrolPath.startPosition;
            patrolPath.endPosition = dTO.PatrolPath.endPosition;

            _enemyFactory.Spawn(dTO.Type, patrolPath, dTO.Pos, _enemyParentTr);
           /* EnemyController enemy = _container.InstantiatePrefab(_enemyPrefab).GetComponent<EnemyController>();
            enemy.transform.parent = _enemyParentTr;
            enemy.transform.position = dTO.Pos;
            enemy.PreInitialize(dTO.EnemyType, patrolPath);
            spawnedEnemy.Add(enemy);*/
        }
        _container.InstantiatePrefab(levelSO.LevelCompleteObj, _levelParentTr);
        _confiner.m_BoundingShape2D = GameObject.Instantiate(levelSO.CameraPlygonCollider, _levelParentTr);
    }
}
