using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletFactory : MonoBehaviour
{
    [Inject] private DiContainer _container;
    [SerializeField] private Transform _parent;
    private List<Bullet> _bulletList = new List<Bullet>();
    private List<HitBullet> _bulletHitList = new List<HitBullet>();
    private Bullet _bulletPrefab;
    private HitBullet _shootHitPrefab;
    public void Initialize(Bullet bulletPrefab, HitBullet shootHitPrefab)
    {
        _bulletPrefab = bulletPrefab;
        _shootHitPrefab = shootHitPrefab;
    }
    public HitBullet GetHitBulllet(Vector2 pos)
    {
        HitBullet hitBullet = default;
        if (_bulletHitList.Count==0)
        {
            hitBullet = GameObject.Instantiate(_shootHitPrefab);
            hitBullet.transform.parent = _parent;
            hitBullet.transform.position = pos;
            _bulletHitList.Add(hitBullet);
        }
        else
        {
            hitBullet= _bulletHitList[0];
            hitBullet.Activate();
            hitBullet.transform.position = pos;
        }
        return hitBullet;
    }
    public Bullet GetBullet(Vector2 pos)
    {
        Bullet bullet = default;
        if (_bulletList.Count == 0)
        {
            bullet = _container.InstantiatePrefab(_bulletPrefab.gameObject).GetComponent<Bullet>();
            bullet.transform.parent = _parent;
            bullet.transform.position = pos;
        }
        else
        {
            bullet = _bulletList[0];
            bullet.Activate();
            bullet.transform.position = pos;
        }
        return bullet;
    }
    public void ReturnToPool(Bullet bullet)
    {
        bullet.Disable();
        _bulletList.Add(bullet);
    }
    public void ReturnToPool(HitBullet hitBullet)
    {
        hitBullet.Disable();
        _bulletHitList.Add(hitBullet);
    }
}
