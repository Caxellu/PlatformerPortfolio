using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class BulletController 
{
    [Inject] private SignalBus _signalBus;
    [Inject] private BulletFactory _bulletFactory;
    [Inject] private IBullletSpawnPos _bullletSpawnPos;

    private float _bulletSpeed;
    private int _damage;
    public  void Initialize(float bulletSpeed, int damage)
    {
        _bulletSpeed = bulletSpeed;

        _signalBus.Subscribe<BulletHitSignal>(BulletHit);
        _signalBus.Subscribe<FireSignal>(Fire);
    }
    private void BulletHit(BulletHitSignal arg)
    {
        HitBullet hitObj = _bulletFactory.GetHitBulllet(arg.Pos);
        hitObj.Initialize(1.5f, (hitBullet) => { _bulletFactory.ReturnToPool(hitBullet); });
    }
    private void Fire(FireSignal arg)
    {
        Bullet bullet = _bulletFactory.GetBullet(_bullletSpawnPos.Position);
        bullet.Initialize(_bulletSpeed, _damage, arg.IsRightDir, (bullet) => { _bulletFactory.ReturnToPool(bullet); });
    }
}
