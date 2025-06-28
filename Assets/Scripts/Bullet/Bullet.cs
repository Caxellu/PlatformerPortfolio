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

    public void Initialize(float speed,int damage, bool isRightDir, UnityAction<Bullet> returnToPoolAction)
    { 
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.flipX = !isRightDir;
        _speed = speed;
        _damage= damage;
        _returnToPoolAction= returnToPoolAction;
        move = isRightDir==true? new Vector2(1,0) : new Vector2(-1,0);
    }
    protected override void ComputeVelocity()
    {
        targetVelocity = move* _speed;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        var obj= collision.gameObject.GetComponent<EnemyController>();
        if (obj!= null)
        {
            obj.TakeDamage(_damage);
        }
        _signalBus.Fire(new BulletHitSignal(collision.contacts[0].point));
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
