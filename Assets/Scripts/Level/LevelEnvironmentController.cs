using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class LevelEnvironmentController : MonoBehaviour
{
    [Inject] private IPlayerMovement _playerMovement;
    [SerializeField] private CinemachineConfiner _confiner;
    [Space]
    [SerializeField] private PatrolPath _patrolPathPrefab;
    [Space]
    [SerializeField] private Grid _grid;
    [SerializeField] private Transform _enemyParentTr;
    [SerializeField] private Transform _patrolsPathParentTr;
    [SerializeField] private Transform _levelParentTr;
    [SerializeField] private PolygonCollider2D polygonCollider2D;
    [SerializeField] private BoxCollider2D completeLevelCollider;

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap tilemapBack;

    private LevelSO _levelSO;
    private EnemyPresenter _enemyPresenter;
    public void Initialize(LevelSO levelSO, EnemyPresenter enemyPresenter)
    {
        _levelSO = levelSO;
        _enemyPresenter= enemyPresenter;
        SetUpLevelEnvironment(_levelSO);
    }
    private void SetUpLevelEnvironment(LevelSO levelSO)
    {
        ShowTile(levelSO.tiles, tilemap, levelSO.uniqueTileBase);
        ShowTile(levelSO.tilesback, tilemapBack, levelSO.uniqueTileBase);

        _playerMovement.SetPosition(levelSO.spawnPlayerPos);

        _enemyPresenter.Intialize(levelSO.enemyDTOs, _enemyParentTr);

        completeLevelCollider.transform.position = levelSO.winColliderDTO.wordPositionWinCollider;
        completeLevelCollider.offset = levelSO.winColliderDTO.winColliderOffset;
        completeLevelCollider.size = levelSO.winColliderDTO.winColliderSize;


        polygonCollider2D.transform.position = levelSO.polygonColliderDTO.worldPosition;
        polygonCollider2D.points = levelSO.polygonColliderDTO.paths;
        _confiner.m_BoundingShape2D = polygonCollider2D;
    }
    private void ShowTile(List<TileDTO> tiles, Tilemap tilemap, List<TileBase> knownTiles)
    {
        foreach (var t in tiles)
        {
            var tile = knownTiles[t.tileId];
            tilemap.SetTile(new Vector3Int(t.x, t.y, 0), tile);
        }
    }
}