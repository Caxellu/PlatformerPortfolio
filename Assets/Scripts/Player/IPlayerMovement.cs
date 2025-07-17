using System;

public interface IPlayerMovement
{
    public void Initialize(float maxSpeed, float jumpSpeed, Action<bool> groundedAction,
        Action<float> velocityXAction, Action<float> velocityYAction);
    void StartRightMove();
    void StopMove();
    void StartLeftMove();
    void TryJump();
    void SetFreeze();
    void SetUnFreeze(); 
    public event Action<bool> OnDirectionAction;
}