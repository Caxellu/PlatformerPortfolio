using System;
using UnityEngine;

public class BulletLogic
{
    public float Speed { get; private set; }
    public int Damage { get; private set; }

    public event Action<Vector2> OnHit;
    public event Action OnDeactivate;

    public BulletLogic(int damage, float speed)
    {
        Damage = damage;
        Speed = speed;
    }

    public void OnCollision(IDamageable target, Vector2 hitPoint)
    {
        target?.TakeDamage(Damage);
        OnHit?.Invoke(hitPoint);
        OnDeactivate?.Invoke();
    }
}