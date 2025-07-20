using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Gameplay/LevelSO")]
public class LevelSO : ScriptableObject
{
    public int LevelIndex;
    public List<TileDTO> tiles;
    public List<TileDTO> tilesback;
    public List<TileBase> uniqueTileBase; 
    public List<EnemyDTO> enemyDTOs;
    public Vector3 spawnPlayerPos;
    [Space]
    public WinColliderDTO winColliderDTO;
    [Space]
    public PolygonColliderDTO polygonColliderDTO;
}