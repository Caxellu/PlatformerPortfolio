using System;
using UnityEngine;
public interface IEnemyMovement
{
    void Initialize(float maxSpeed, PatrolPath patrolPath, IPlayerPos playerPos);
    void PlayerLost();
    void PlayerFound();
    void SetPosition(Vector3 position);
    void SetFreeze();
    void SetUnFreeze();
    event Action<bool> OnDirectionAction;
}
