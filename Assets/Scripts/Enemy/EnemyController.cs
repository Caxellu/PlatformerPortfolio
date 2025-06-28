using UnityEngine;
using Zenject;

[RequireComponent(typeof(AnimationController), typeof(Collider2D))]
public class EnemyController : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;
    [Inject] private IPlayerPos _playerPos;
    [SerializeField] private PatrolPath _path;
    [SerializeField] private int _damage;

    internal PatrolPath.Mover mover;
    internal AnimationController control;

    private bool isPlayerFound;
    void Awake()
    {
        control = GetComponent<AnimationController>();
    }
    public void Initialize(EnemySO enemySO)
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            _signalBus.Fire(new EnemyCollisionSignal(_damage));
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
            if (mover == null) mover = _path.CreateMover(control.MaxSpeed * 0.5f);
            control.move.x = Mathf.Clamp(mover.Position.x - transform.position.x, -1, 1);
        }
    }

}
