using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHp;
    private UnityAction _dieAction;

    private int currentHp;
    public bool IsAlive => currentHp > 0;
    private void Awake()
    {
        currentHp = _maxHp;
    }
    public void Initialize(UnityAction dieAction)
    {
        _dieAction = dieAction;
    }
    public void TakeDamage(int damage)
    {
        currentHp= Mathf.Clamp(currentHp-damage, 0, _maxHp);
        if (currentHp == 0)
            Die();
    }
    private void Die()
    {
        _dieAction?.Invoke();
    }
}
