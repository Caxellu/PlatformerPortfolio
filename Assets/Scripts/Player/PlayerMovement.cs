using System;
using UnityEngine;
public class PlayerMovement : KinematicObject, IPlayerMovement
{
    public event Action<bool> OnDirectionAction;
    private JumpState jumpState = JumpState.Grounded;
    private float _jumpTakeOffSpeed;
    private float _maxSpeed;
    private bool jump;
    private bool stopJump;
    private Vector2 move;
    private Action<bool> _groundedAction;
    private Action<float> _velocityXAction;
    private Action<float> _velocityYAction;
    public void Initialize(float maxSpeed, float jumpSpeed, Action<bool> groundedAction,
        Action<float> velocityXAction, Action<float> velocityYAction)
    {
        _maxSpeed = maxSpeed;
        _jumpTakeOffSpeed= jumpSpeed;
        _groundedAction = groundedAction;
        _velocityXAction = velocityXAction;
        _velocityYAction = velocityYAction;
    }
    public void SetFreeze()
    {
        Freeze();
    }
    public void SetUnFreeze()
    {
        Unfreeze();
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
    protected override void Update()
    {
        UpdateJumpState();
        base.Update();
    }
    private void UpdateJumpState()
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
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            OnDirectionAction?.Invoke(true);
        }
        else if (move.x < -0.01f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            OnDirectionAction?.Invoke(false);
        }
        _groundedAction?.Invoke(IsGrounded);
        _velocityXAction?.Invoke(Mathf.Abs(velocity.x) / _maxSpeed);
        _velocityYAction?.Invoke(velocity.y / _maxSpeed);

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