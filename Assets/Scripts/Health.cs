using UnityEngine;
using System;

public class Health: IDamageable
{
    public event Action OnHurt;
    public event Action OnDie;

    private int _currentHp;
    private int _maxHp;
    public bool IsAlive => _currentHp > 0;
    public Health(int maxHp)
    {
        _maxHp = maxHp;
        _currentHp = maxHp;
    }
    public void TakeDamage(int amount)
    {
        if (!IsAlive) return;

        _currentHp = Mathf.Clamp(_currentHp - amount, 0, _maxHp);

        if (_currentHp == 0)
            OnDie?.Invoke();
        else
            OnHurt?.Invoke();
    }
    public void SetMaxHp()
    {
        _currentHp=_maxHp;
    }

}
