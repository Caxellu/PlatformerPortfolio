using UnityEngine;
using Zenject;
[RequireComponent(typeof(Collider2D))]
public class BulletView : KinematicObject
{
    private BulletLogic _logic;
    private Vector2 _move;
    private SpriteRenderer _spriteRenderer;
    private IBulletFactory _bulletFactory; 
    private SignalBus _signalBus;

    public void Construct(BulletLogic logic, IBulletFactory bulletFactory, SignalBus signalBus)
    {
        _spriteRenderer=GetComponent<SpriteRenderer>(); 
        _logic = logic;
        _bulletFactory = bulletFactory;
        _signalBus=signalBus;
        _logic.OnHit += HandleHit;
        _logic.OnDeactivate += HandleDeactivate;
    }

    public void Initialize(bool isRightDir)
    {
        _spriteRenderer.flipX = !isRightDir;
        _move = isRightDir == true ? new Vector2(1, 0) : new Vector2(-1, 0);
    }

    protected override void ComputeVelocity()
    {
        targetVelocity = _move* _logic.Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageable = collision.GetComponent<IDamageable>();
        Vector2 hitPoint = collision.ClosestPoint(transform.position);
        _logic.OnCollision(damageable, hitPoint);
    }
    private void HandleHit(Vector2 hitPoint)
    {
        _signalBus.Fire(new BulletHitSignal(hitPoint));
        gameObject.SetActive(false);
    }
    private void HandleDeactivate()
    {
        _bulletFactory.ReturnToPool(this);
    }
}
