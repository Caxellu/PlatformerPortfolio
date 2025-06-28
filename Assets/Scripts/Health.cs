using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private UnityAction _dieAction;

    private int currentHp;
    private int _maxHp;
    public bool IsAlive => currentHp > 0;
    public void Initialize(UnityAction dieAction, int maxHp)
    {
        _dieAction = dieAction;
        _maxHp = maxHp;
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
