using System;
using UnityEngine;

public class PlayerView : MonoBehaviour, IPlayerView, IBullletSpawnPos, IUnitDirection, IPlayerPos, IDamageable
{
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private Transform _bulletSpawnTr;
    public event Action<int> OnTakeDamageAction;
    private float _fireDuration;
    private bool _isRightDir=true;

    private ICoroutineManager _corutineManager;
    Vector2 IPlayerPos.Position => transform.position;
    bool IUnitDirection.IsRightDir => _isRightDir;
    Vector2 IBullletSpawnPos.Position => _bulletSpawnTr.position;
    public float FireDuration => _fireDuration;
    public void Initialize (float fireDuraion, ICoroutineManager coroutineManager)
    {
        _fireDuration = fireDuraion;
        _corutineManager = coroutineManager;
    }
    public void SetActivePlayer(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
    public void SetPlayerDirection(bool playerDir)
    {
        _isRightDir = playerDir;
    }
    public void SetFire()
    {
        animator.SetBool("fire", true);
        _corutineManager.RunDelayedAction(_fireDuration, StopFire);
    }
    public void StopFire()
    {
        animator.SetBool("fire", false);
    }
    public void SetRunning(bool isRunning)
    {
        animator.SetBool("IsRunning", isRunning);
    }
    public void SetHurt()
    {
        animator.SetTrigger("hurt");
    }
    public void Flip(bool facingRight)
    {
        spriteRenderer.flipX = !facingRight;
    }
    public void SetGrounded(bool isGrounded)
    {
        animator.SetBool("grounded", isGrounded);
    }
    public void SetVelocityX(float velocity)
    {
        animator.SetFloat("velocityX",velocity);
    }
    public void SetVelocityY(float velocity)
    {
        animator.SetFloat("velocityY", velocity);
    }

    public void TakeDamage(int damage)
    {
        OnTakeDamageAction?.Invoke(damage);
    }
}