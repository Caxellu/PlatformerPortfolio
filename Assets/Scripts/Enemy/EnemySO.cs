using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/EnemySO")]
public class EnemySO : ScriptableObject
{
    public int Damage;
    public int Hp;
    public float Speed;
    public AnimatorController controller;
}
