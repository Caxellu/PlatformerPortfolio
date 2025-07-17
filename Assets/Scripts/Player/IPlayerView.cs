public interface IPlayerView
{
    void Initialize(float fireDuraion, ICoroutineManager coroutineManager);
    float FireDuration { get; }
    void SetRunning(bool isRunning);
    void Flip(bool facingRight);
    public void SetFire();
    public void StopFire();
    void SetHurt();
    void SetGrounded(bool isGrounded);
    void SetVelocityX(float velocity);
    void SetVelocityY(float velocity);
    void SetActivePlayer(bool isActive);
    void SetPlayerDirection(bool playerDir);
}