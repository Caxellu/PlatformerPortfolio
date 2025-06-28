using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Gameplay/LevelSO")]
public class LevelSO : ScriptableObject
{
    public List<Tilemap> Tilemaps;
    public Vector3 spawnPlayerPos;
    public List<EnemyDTO> enemyDTOs;
    public GameObject LevelCompleteObj;
    public PolygonCollider2D CameraPlygonCollider;
}
[Serializable]
public class EnemyDTO
{
    public Vector3 Pos;
    public EnemyType EnemyType;
    public PatrolPathDTO PatrolPath;
}