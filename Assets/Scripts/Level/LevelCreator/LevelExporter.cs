using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class LevelExporter : MonoBehaviour
{
    public int levelIndex;
    public Tilemap tilemap;
    public Tilemap tileMapBack;
    public List<PatrolPathLevelCreator> patrolPathLevelCreators;
    public Transform SpawnPlayerPos;
    public BoxCollider2D levelCompleteCollider;
    public PolygonCollider2D CameraPlygonCollider;

    [ContextMenu("Export Level")]
    public void ExportLevel()
    {
        List<TileBase> uniqueTile = new List<TileBase>();

        var tiles = GetTileData(tilemap, ref uniqueTile);
        var tilesback = GetTileData(tileMapBack, ref uniqueTile);
        var uniqueTileBase = uniqueTile;

        var spawnPlayerPos = SpawnPlayerPos.position;

        List<EnemyDTO> enemyData = new List<EnemyDTO>();
        foreach (PatrolPathLevelCreator patrolPathLevelCreator in patrolPathLevelCreators)
        {
            EnemyDTO enemyDTO = new EnemyDTO();
            PatrolPathDTO patrolPath = new PatrolPathDTO();
            patrolPath.endPosition = patrolPathLevelCreator.endPosition;
            patrolPath.startPosition = patrolPathLevelCreator.startPosition;
            patrolPath.globalPosition = patrolPathLevelCreator.transform.position;

            enemyDTO.PatrolPath = patrolPath;
            enemyDTO.Type = patrolPathLevelCreator.Type;
            enemyDTO.EnemySpawnPos = patrolPath.startPosition + patrolPath.globalPosition;

            enemyData.Add(enemyDTO);
        }

        WinColliderDTO winColliderDTO = new WinColliderDTO();
        winColliderDTO.wordPositionWinCollider = levelCompleteCollider.transform.position;
        winColliderDTO.winColliderOffset = levelCompleteCollider.offset;
        winColliderDTO.winColliderSize = levelCompleteCollider.size;

        PolygonColliderDTO polygonColliderDTO = new PolygonColliderDTO();
        polygonColliderDTO.worldPosition = CameraPlygonCollider.transform.position;
        polygonColliderDTO.paths = CameraPlygonCollider.points;

        LevelDataCreator levelDataCreator = new LevelDataCreator();
        levelDataCreator.CreateLevelData(levelIndex, tiles, tilesback, uniqueTileBase, enemyData, spawnPlayerPos,
            winColliderDTO, polygonColliderDTO);
    }

    private List<TileDTO> GetTileData(Tilemap tilemap, ref List<TileBase> uniqueTiles)
    {
        List<TileDTO> tileData = new List<TileDTO>();
        var bounds = tilemap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                var pos = new Vector3Int(x, y, 0);
                var tile = tilemap.GetTile(pos);
                if (tile == null) continue;

                if (!uniqueTiles.Contains(tile))
                    uniqueTiles.Add(tile);

                TileDTO temp = new TileDTO()
                {
                    tileId = uniqueTiles.IndexOf(tile),
                    x = x,
                    y = y
                };
                tileData.Add(temp);
            }
        return tileData;
    }
}


