using System;
using UnityEngine;
public interface IEnemyView
{
    event Action OnUpdateAction;
    event Action<int> OnTakeDamageAction;
    event Action<IDamageable> OnCauseDamage;
    void Initialize(RuntimeAnimatorController animatorController);
    void SetDirection(bool isRight);
    void TakeDamage(int damage);
    void SetActive(bool flag);
    Vector3 Position { get; }
}