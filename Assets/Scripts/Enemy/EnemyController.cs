using UnityEngine;
using Zenject;

[RequireComponent(typeof(AnimationController), typeof(Collider2D), typeof(Health))]
public class EnemyController : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;
    [Inject] private IPlayerPos _playerPos;
    public PatrolPath _path;
    public EnemyType _enemyType;
    public EnemyType EnemyType => _enemyType;
    private EnemySO _enemySO;
    private int damage => _enemySO.Damage;
    private float maxSpeed => _enemySO.Speed;

    private Health health;

    internal PatrolPath.Mover mover;
    internal AnimationController control;

    private bool isPlayerFound;
    public void PreInitialize(EnemyType enemyType, PatrolPath path)
    {
        _path = path;
        _enemyType=enemyType;
    }
    public void Initialize(EnemySO enemySO)
    {
        health = GetComponent<Health>();
        control = GetComponent<AnimationController>();

        _enemySO = enemySO;
        control.Initialize(enemySO.controller, maxSpeed);
        health.Initialize(Die,null, _enemySO.Hp);

    }
    private void Die()
    {
        gameObject.SetActive(false);
    }
    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            _signalBus.Fire(new EnemyCollisionSignal(damage));
        }
    }

    void Update()
    {
        if (!isPlayerFound)
            if (Vector2.Distance(_playerPos.Position, transform.position) < 6f)
            {
                if (control.ISRightDirection && _playerPos.Position.x > transform.position.x)
                    isPlayerFound = true;
                else if (!control.ISRightDirection && _playerPos.Position.x < transform.position.x)
                    isPlayerFound = true;
            }

        if (isPlayerFound)
        {
            control.move.x = Mathf.Clamp(_playerPos.Position.x - transform.position.x, -1, 1);
        }
        else if (_path != null)
        {
            if (mover == null) mover = _path.CreateMover(maxSpeed * 0.5f);
            control.move.x = Mathf.Clamp(mover.Position.x - transform.position.x, -1, 1);
        }
    }

}
