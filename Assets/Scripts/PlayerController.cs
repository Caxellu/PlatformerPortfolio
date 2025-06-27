using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : KinematicObject
{
    [SerializeField] private float _maxSpeed = 7;
    [SerializeField] private float _jumpTakeOffSpeed = 7;
    private Collider2D collider2d;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector2 move;
    private bool jump;
    private bool stopJump;
    [SerializeField] private JumpState jumpState = JumpState.Grounded;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        collider2d = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void StartRightMove()
    {
        Debug.Log("StartRightMove");
    }
    public void StopRightMove()
    {

        Debug.Log("StopRightMove");
    }
    public void StartLeftMove()
    {

        Debug.Log("StartLeftMove");
    }
    public void StopLeftMove()
    {

        Debug.Log("StopLeftMove");
    }
    public void TryJump()
    {

        Debug.Log("TryJump");
    }
    public void TryFire()
    {

        Debug.Log("TryFire");
    }
    protected override void Update()
    {
        UpdateJumpState();
        base.Update();
    }
    void UpdateJumpState()
    {
        jump = false;
        switch (jumpState)
        {
            case JumpState.PrepareToJump:
                jumpState = JumpState.Jumping;
                jump = true;
                stopJump = false;
                break;
            case JumpState.Jumping:
                if (!IsGrounded)
                {
                    jumpState = JumpState.InFlight;
                }
                break;
            case JumpState.InFlight:
                if (IsGrounded)
                {
                    jumpState = JumpState.Landed;
                }
                break;
            case JumpState.Landed:
                jumpState = JumpState.Grounded;
                break;
        }
    }
    protected override void ComputeVelocity()
    {
        if (jump && IsGrounded)
        {
            velocity.y = _jumpTakeOffSpeed;
            jump = false;
        }
        else if (stopJump)
        {
            stopJump = false;
        }

        if (move.x > 0.01f)
            spriteRenderer.flipX = false;
        else if (move.x < -0.01f)
            spriteRenderer.flipX = true;

       /* animator.SetBool("grounded", IsGrounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / _maxSpeed);
*/
        targetVelocity = move * _maxSpeed;
    }
    public enum JumpState
    {
        Grounded,
        PrepareToJump,
        Jumping,
        InFlight,
        Landed
    }
}
