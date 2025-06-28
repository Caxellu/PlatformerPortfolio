using UnityEngine;
using UnityEditor.Animations;
using Zenject;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class AnimationController : KinematicObject
{
    [Inject] private SignalBus _signalBus;
    private float _maxSpeed;
    public Vector2 move;

    private bool isRightDirection;
    public bool ISRightDirection => isRightDirection;
    SpriteRenderer spriteRenderer;
    Animator animator;
    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        _signalBus.Subscribe<FreezeSignal>(Freeze);
        _signalBus.Subscribe<UnFreezeSignal>(Unfreeze);
    }
    public void Initialize(AnimatorController animatorController, float maxSpeed)
    {
        _maxSpeed = maxSpeed;
        animator.runtimeAnimatorController= animatorController;
    }
    protected override void ComputeVelocity()
    {
        if (move.x > 0.01f)
        {
            spriteRenderer.flipX = false;
            isRightDirection = true;
        }
        else if (move.x < -0.01f)
        {
            spriteRenderer.flipX = true;
            isRightDirection = false;
        }

        targetVelocity = move * _maxSpeed;
    }
}