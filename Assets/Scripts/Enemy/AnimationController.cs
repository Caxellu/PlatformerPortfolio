using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class AnimationController : KinematicObject
{
    [SerializeField] private float maxSpeed = 7;
    [SerializeField] private float jumpTakeOffSpeed = 7;
    public float MaxSpeed => maxSpeed;
    public Vector2 move;

    private bool isRightDirection;
    public bool ISRightDirection => isRightDirection;
    SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        targetVelocity = move * maxSpeed;
    }
}