using Cinemachine;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class LevelEnvironmentController : MonoBehaviour
{
    [Inject] private IPlayerMovement _playerMovement;
    [Inject] private DiContainer _container;
    [SerializeField] private CinemachineConfiner _confiner;
    [Space]
    [SerializeField] private PatrolPath _patrolPathPrefab;
    [Space]
    [SerializeField] private Grid _grid;
    [SerializeField] private Transform _enemyParentTr;
    [SerializeField] private Transform _patrolsPathParentTr;
    [SerializeField] private Transform _levelParentTr;
    LevelSO _levelSO;
    private EnemyPresenter _enemyPresenter;
    public void Initialize(LevelSO levelSO, EnemyPresenter enemyPresenter)
    {
        _levelSO = levelSO;
        _enemyPresenter= enemyPresenter;
        SetUpLevelEnvironment(_levelSO);
    }
    private void SetUpLevelEnvironment(LevelSO levelSO)
    {
        foreach (Tilemap tilemap in levelSO.Tilemaps)
        {
            GameObject.Instantiate(tilemap, _grid.transform);
        }
        _playerMovement.SetPosition(levelSO.spawnPlayerPos);

        _enemyPresenter.Intialize(levelSO.enemyDTOs, _patrolPathPrefab, _enemyParentTr);

       /* foreach (EnemyMoveDTO dTO in levelSO.enemyDTOs)
        {
            PatrolPath patrolPath = GameObject.Instantiate(_patrolPathPrefab, _patrolsPathParentTr);
            patrolPath.transform.position = dTO.PatrolPath.globalPosition;
            patrolPath.startPosition = dTO.PatrolPath.startPosition;
            patrolPath.endPosition = dTO.PatrolPath.endPosition;

            _enemyFactory.Spawn(dTO.Type, patrolPath, dTO.Pos, _enemyParentTr);
        }*/
        _container.InstantiatePrefab(levelSO.LevelCompleteObj, _levelParentTr);
        _confiner.m_BoundingShape2D = GameObject.Instantiate(levelSO.CameraPlygonCollider, _levelParentTr);
    }
}
