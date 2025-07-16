using UnityEngine;

public class EnemyView : MonoBehaviour, IDamageable
{
    public EnemyAnimationView Animation { get; private set; }
    public Health Health { get; private set; }

    private EnemyControllerLogic _logic;

    public void Construct(EnemyControllerLogic logic)
    {
        _logic = logic;
    }

    private void Awake()
    {
        Animation = GetComponent<EnemyAnimationView>();
        Health = GetComponent<Health>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            _logic.OnPlayerCollision(player);
        }
    }

    private void Update()
    {
        _logic?.Update();
    }
    public void TakeDamage(int amount)
    {
        _logic.TakeDamage(amount);
    }
}