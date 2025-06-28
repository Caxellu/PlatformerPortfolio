using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : KinematicObject, IPlayerPos
{
    [SerializeField] private float _maxSpeed = 7;
    [SerializeField] private float _jumpTakeOffSpeed = 7;
    [SerializeField] private float _fireCooldown = 1f;
    [SerializeField] private float _fireDuration = 0.5f;
    [SerializeField] private JumpState jumpState = JumpState.Grounded;
    
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector2 move;
    private bool jump;
    private bool stopJump;
    private bool isCooldown;
    private bool isFire;
    private bool isDead;

    public Vector2 Position => transform.position;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void StartRightMove()
    {
        move.x = 1;
    }
    public void StopMove()
    {
        move.x = 0;
    }
    public void StartLeftMove()
    {
        move.x = -1;
    }
    public void TryJump()
    {
        if (jumpState == JumpState.Grounded)
            jumpState = JumpState.PrepareToJump;
        else 
        {
            stopJump = true;
        }
    }
    public void TryFire()
    {
        if (!isCooldown)
        {
            animator.SetBool("fire", true);
            isCooldown = true;
            isFire = true;
            StartCoroutine(WaitAndAction(_fireCooldown, () => { isCooldown = false; }));
            StartCoroutine(WaitAndAction(_fireDuration, () => { isFire = false; animator.SetBool("fire", false); }));
        }
    }
    public void Die()
    {
        isDead = true;
        StopMove();
    }
    IEnumerator WaitAndAction(float duration, UnityAction action)
    {
        yield return new WaitForSeconds(duration);
        action?.Invoke();
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

        animator.SetBool("grounded", IsGrounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / _maxSpeed);
        animator.SetFloat("velocityY", velocity.y / _maxSpeed);

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
public interface IPlayerPos
{
    public Vector2 Position { get; }
}