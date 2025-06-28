using UnityEngine;
using Zenject;

[RequireComponent(typeof(AnimationController), typeof(Collider2D))]
public class EnemyController : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;
    [Inject] private IPlayerPos _playerPos;
    [SerializeField] private PatrolPath _path;
    [SerializeField] private EnemyType _enemyType;
    public EnemyType EnemyType => _enemyType;
    private EnemySO _enemySO;
    private int damage => _enemySO.Damage;
    private float maxSpeed => _enemySO.Speed;

    private int currentHp;

    internal PatrolPath.Mover mover;
    internal AnimationController control;

    private bool isPlayerFound;
    public void Initialize(EnemySO enemySO)
    {
        control = GetComponent<AnimationController>();

        _enemySO = enemySO;
        control.Initialize(enemySO.controller, maxSpeed);
        currentHp = _enemySO.Hp;
    }
    public void TakeDamage(int damage)
    {
        currentHp = Mathf.Clamp(currentHp - damage, 0, _enemySO.Hp);
        if (currentHp == 0)
            Debug.Log("Enemy Die");
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
