using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Animator))]
public class PlayerController : KinematicObject, IPlayerPos, IBullletSpawnPos, IPlayerDirection, IDisposable
{
    [Inject] private SignalBus _signalBus;
    [Inject] private CorutineManager _corutineManager;
    [SerializeField] private Transform _bulletSpawnTr;
    private float _maxSpeed;
    private float _jumpTakeOffSpeed;
    private float _fireDuration;
    private JumpState jumpState = JumpState.Grounded;

    private Animator animator;

    private Vector2 move;

    private bool jump;
    private bool stopJump;
    private bool isFire;
    private bool isRightDir = true;

    Vector2 IPlayerPos.Position => transform.position;

    Vector2 IBullletSpawnPos.Position => _bulletSpawnTr.position;

    bool IPlayerDirection.IsRightDir => isRightDir;

    public void Initialize(float speed, float jumpTakeOffSpeed, float fireDuration)
    {
        _jumpTakeOffSpeed = jumpTakeOffSpeed;
        _fireDuration = fireDuration;
        _maxSpeed = speed;
        _signalBus.Subscribe<FireSignal>(Fire);
        _signalBus.Subscribe<FreezeSignal>(Freeze);
        _signalBus.Subscribe<UnFreezeSignal>(Unfreeze);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<FireSignal>(Fire);
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
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
    public void Fire()
    {
        animator.SetBool("fire", true);
        isFire = true;
        _corutineManager.WaitAndActionCorutineCall(_fireDuration, () => { isFire = false; animator.SetBool("fire", false); });
    }
    public void Hurt()
    {
        animator.SetTrigger("hurt");
    }
    public void Die()
    {
        StopMove();
        gameObject.SetActive(false);
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
public interface IPlayerDirection
{
    public bool IsRightDir { get; }
}