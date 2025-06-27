using UnityEngine;
using Zenject;

[RequireComponent(typeof(AnimationController), typeof(Collider2D))]
public class EnemyController : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;
    [SerializeField] private PatrolPath path;

    internal PatrolPath.Mover mover;
    internal AnimationController control;

    void Awake()
    {
        control = GetComponent<AnimationController>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            _signalBus.Fire<EnemyCollisionSignal>();
        }
    }

    void Update()
    {
        if (path != null)
        {
            if (mover == null) mover = path.CreateMover(control.MaxSpeed * 0.5f);
            control.move.x = Mathf.Clamp(mover.Position.x - transform.position.x, -1, 1);
        }
    }

}