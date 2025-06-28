using UnityEngine;
using UnityEditor.Animations;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class AnimationController : KinematicObject
{
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