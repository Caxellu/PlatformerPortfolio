using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;


public class BulletController 
{
    [Inject] private SignalBus _signalBus;
    [Inject] private BulletFactory _bulletFactory;
    [Inject] private IBullletSpawnPos _bullletSpawnPos;
    [Inject] private IPlayerDirection _playerDirection;
    [Inject] private CorutineManager _corutineManager;
    private int maxAmmo;
    private int ammoCount;
    private float _bulletSpeed;
    private int _damage;
    private float _fireCooldown;
    private bool isCooldown;
    public  void Initialize(float bulletSpeed, int damage, float fireCooldown, int startAmmo)
    {
        _bulletSpeed = bulletSpeed;
        _damage=damage;
        _fireCooldown=fireCooldown;
        maxAmmo = startAmmo;
        ammoCount =startAmmo;
        _signalBus.Subscribe<BulletHitSignal>(BulletHit);
        _signalBus.Subscribe<FireSignal>(Fire);
        _signalBus.Fire(new UpdateAmmoSignal(ammoCount, maxAmmo));
    }
    public void TryFire()
    {
        if(!isCooldown && ammoCount>0)
        {
            isCooldown = true;
            ammoCount--;
            _corutineManager.WaitAndActionCorutineCall(_fireCooldown, () => { isCooldown = false; });
            _signalBus.Fire(new UpdateAmmoSignal(ammoCount, maxAmmo));
            _signalBus.Fire(new FireSignal(_playerDirection.IsRightDir));
        }
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
