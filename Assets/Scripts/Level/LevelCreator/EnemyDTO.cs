using UnityEngine;

[System.Serializable]
public class EnemyDTO
{
    public EnemyType Type;
    public PatrolPathDTO PatrolPath;
    public Vector3 EnemySpawnPos;
}