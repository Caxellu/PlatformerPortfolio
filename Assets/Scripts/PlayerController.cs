using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : KinematicObject, IPlayerPos, IBullletSpawnPos
{
    [Inject] private SignalBus _signalBus;
    [SerializeField] private Transform _bulletSpawnTr;
    private float _maxSpeed;
    private float _jumpTakeOffSpeed;
    private float _fireCooldown;
    private float _fireDuration;
    private JumpState jumpState = JumpState.Grounded;

    private Animator animator;

    private Vector2 move;

    private bool jump;
    private bool stopJump;
    private bool isCooldown;
    private bool isFire;
    private bool isDead;
    private bool isRightDir;

    Vector2 IPlayerPos.Position => transform.position;

    Vector2 IBullletSpawnPos.Position => _bulletSpawnTr.position;


    public void Initialize(float speed, float jumpTakeOffSpeed, float fireCooldown, float fireDuration)
    {
        _jumpTakeOffSpeed = jumpTakeOffSpeed;
        _fireCooldown = fireCooldown;
        _fireDuration = fireDuration;
        _maxSpeed = speed;

    }
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
            _signalBus.Fire(new FireSignal(isRightDir));
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
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            isRightDir = true;
        }
        else if (move.x < -0.01f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            isRightDir = false;
        }

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
public interface IBullletSpawnPos
{
    public Vector2 Position { get; }
}