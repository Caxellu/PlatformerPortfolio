using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : KinematicObject
{
    [Inject] private SignalBus _signalBus;
    private float _speed;
    private SpriteRenderer _spriteRenderer;
    private int _damage;
    private Vector2 move;
    private UnityAction<Bullet> _returnToPoolAction;

    public void Initialize(float speed, int damage, bool isRightDir, UnityAction<Bullet> returnToPoolAction)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.flipX = !isRightDir;
        _speed = speed;
        _damage = damage;
        _returnToPoolAction = returnToPoolAction;
        move = isRightDir == true ? new Vector2(1, 0) : new Vector2(-1, 0);
    }
    protected override void ComputeVelocity()
    {
        targetVelocity = move * _speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(_damage);
        }
        Vector2 hitPoint = collision.ClosestPoint(transform.position);
        _signalBus.Fire(new BulletHitSignal(hitPoint));
        _returnToPoolAction?.Invoke(this);
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
    public void Activate()
    {
        gameObject.SetActive(true);
    }
}
