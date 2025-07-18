using System;
using UnityEngine;

public class EnemyModel
{
    public event Action OnPlayerFound;

    private IPlayerPos _playerPos;
    private int _damage;
    private bool _isPlayerFound;
    
    public Health Health { get; private set; }
    public float Speed { get; private set; }
    public Vector3 SpawnPos { get; }
    public bool IsRightDirection { get; private set; }
    public EnemyModel( Vector3 vector3, int maxHp, float speed, int damage)
    {
        SpawnPos = vector3;
        Health = new Health(maxHp);
        Speed = speed;
        _damage = damage;
    }
    public void Initialize(IPlayerPos playerPos)
    {
        _playerPos = playerPos;
    }
    public void Reset()
    {
        Health.SetMaxHp();
        _isPlayerFound = false;
        IsRightDirection = true;
    }
    public void CheckIsPlayerFound(Vector2 enemyPos)
    {
        if (!_isPlayerFound)
        {
            Vector2 playerPos = _playerPos.Position;
            float dx = playerPos.x - enemyPos.x;

            if (Vector2.Distance(enemyPos, playerPos) < 6f)
            {
                if ((IsRightDirection && dx > 0) || (!IsRightDirection && dx < 0))
                {
                    _isPlayerFound = true;
                    OnPlayerFound?.Invoke();
                }
            }
        }
    }
    public void SetDirection(bool isRight)
    {
        IsRightDirection= isRight;
    }
    public void TakeDamage(int damage)
    {
        Health.TakeDamage(damage);
    }
    public void CauseDamage(IDamageable damageable)
    {
        damageable.TakeDamage(_damage);
    }
}
