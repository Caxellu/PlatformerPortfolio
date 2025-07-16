using UnityEngine;
using Zenject;

public interface IBulletFactory
{
    void Initialize (BulletView bulletPrefab, HitView hitBulletPrefab, SignalBus signalBus);
    BulletView SpawnBullet(Vector2 position, bool isRightDir, int damage, float speed);
    HitView SpawnHitBullet(Vector2 pos);
    void ReturnToPool(BulletView bullet);
    void ReturnToPool(HitView hit);
}