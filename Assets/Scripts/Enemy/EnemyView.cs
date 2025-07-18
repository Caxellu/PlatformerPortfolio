using System;
using UnityEngine;

public class EnemyView : MonoBehaviour, IDamageable, IEnemyView
{
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    public event Action OnUpdateAction;
    public event Action<int> OnTakeDamageAction;
    public event Action<IDamageable> OnCauseDamage;
    public Vector3 Position { get { return transform.position; } }
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }
    public void Initialize(RuntimeAnimatorController animatorController)
    {
        _animator.runtimeAnimatorController = animatorController;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            OnCauseDamage?.Invoke(damageable);
        }
    }
    void Update()
    {
        OnUpdateAction?.Invoke();
    }
    public void SetDirection(bool isRight)
    {
        _spriteRenderer.flipX = isRight;
    }
    public void TakeDamage(int damage)
    {
        OnTakeDamageAction?.Invoke(damage);
    }
    public void SetActive(bool flag)
    {
        gameObject.SetActive(flag);
    }
}