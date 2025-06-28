using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/EnemySO")]
public class EnemySO : ScriptableObject
{
    public EnemyType enemyType;
    public int Damage;
    public int Hp;
    public float Speed;
    public RuntimeAnimatorController controller;
}
