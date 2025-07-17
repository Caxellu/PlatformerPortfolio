using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Gameplay/LevelSO")]
public class LevelSO : ScriptableObject
{
    public int LevelIndex;
    public List<Tilemap> Tilemaps;
    public Vector3 spawnPlayerPos;
    public List<EnemyMoveDTO> enemyDTOs;
    public GameObject LevelCompleteObj;
    public PolygonCollider2D CameraPlygonCollider;
}
[Serializable]
public class EnemyMoveDTO
{
    public Vector3 Pos;
    public EnemyType Type;
    public PatrolPathDTO PatrolPath;
}