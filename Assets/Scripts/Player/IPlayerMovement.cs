using System;
using UnityEngine;

public interface IPlayerMovement
{
    void Initialize(float maxSpeed, float jumpSpeed, Action<bool> groundedAction,
        Action<float> velocityXAction, Action<float> velocityYAction);
    void SetPosition(Vector2 vector2);
    void StartRightMove();
    void StopMove();
    void StartLeftMove();
    void TryJump();
    void SetFreeze();
    void SetUnFreeze(); 
    public event Action<bool> OnDirectionAction;
}