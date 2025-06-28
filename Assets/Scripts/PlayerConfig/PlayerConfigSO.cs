using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/PlayerConfig")]
public class PlayerConfigSO : ScriptableObject
{
    public float speed;
    public float jumpTakeOffSpeed;
    public int bulletDamage;
    public float fireCooldown;
    public float fireDuration;
    public int maxHp;
    [Space]
    public Bullet bulletPrefab;
    public HitBullet hitPrefab;
    public float bulletSpeed;
    public int startAmmo;
}
