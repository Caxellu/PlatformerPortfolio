using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletFactory : MonoBehaviour, IBulletFactory
{
    [SerializeField] private Transform _parent;

    private Queue<BulletView> _bulletPool = new Queue<BulletView>();
    private Queue<HitView> _hitPool = new Queue<HitView>();

    private BulletView _bulletViewPrefab;
    private HitView _hitViewPrefab;
    private SignalBus _signalBus;


    public void Initialize(BulletView bulletPrefab, HitView hitViewPrefab, SignalBus signalBus)
    {
        _bulletViewPrefab = bulletPrefab;
        _hitViewPrefab = hitViewPrefab;
        _signalBus = signalBus;
    }

    public BulletView SpawnBullet(Vector2 position, bool isRightDir, int damage, float speed)
    {
        BulletView bulletView;

        if (_bulletPool.Count > 0)
        {
            bulletView = _bulletPool.Dequeue();
            bulletView.gameObject.SetActive(true);
        }
        else
        {
            bulletView = Instantiate(_bulletViewPrefab, _parent);
        }

        var logic = new BulletLogic(damage, speed);
        bulletView.Construct(logic, this, _signalBus);
        bulletView.transform.position = position;
        bulletView.Initialize(isRightDir);

        return bulletView;
    }

    public void ReturnToPool(BulletView bulletView)
    {
        bulletView.gameObject.SetActive(false);
        _bulletPool.Enqueue(bulletView);
    }

    public HitView SpawnHitBullet(Vector2 pos)
    {
        HitView hitView;
        if (_hitPool.Count > 0)
        {
            hitView = _hitPool.Dequeue();
        }else
        {
            hitView = Instantiate(_hitViewPrefab, _parent);
        }
        hitView.Construct(this);
        hitView.LifeCycle.Construct(2.5f);
        hitView.LifeCycle.StartLife();
        hitView.transform.position = pos;
        return hitView;
    }

    public void ReturnToPool(HitView hitView)
    {
        _hitPool.Enqueue(hitView);
    }
}