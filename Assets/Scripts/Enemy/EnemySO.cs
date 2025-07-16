using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/EnemySO")]
public class EnemySO : ScriptableObject
{
    public EnemyType Type;
    public int Damage;
    public int MaxHp;
    public float Speed;
    public RuntimeAnimatorController AnimatorController;
}
