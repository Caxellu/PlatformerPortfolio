using System;
using UnityEngine;

public class EnemyMovement : KinematicObject, IEnemyMovement
{
    public event Action<bool> OnDirectionAction;
    private Vector2 _move;
    private float _maxSpeed;
    private PatrolPath _path;
    private PatrolPath.Mover _mover;
    private IPlayerPos _playerPos;
    
    private bool _isPlayerFound;
    public void Initialize(float maxSpeed, PatrolPath patrolPath,  IPlayerPos playerPos)
    {
        _maxSpeed = maxSpeed;
        _path= patrolPath;
        _playerPos= playerPos;
    }
    protected override void Update()
    {
        if (_isPlayerFound)
        {
            float dx = _playerPos.Position.x - transform.position.x;
            _move = new Vector2(Mathf.Clamp(dx, -1, 1), 0f);
        }
        else if (_path != null)
        {
            _mover ??= _path.CreateMover(_maxSpeed * 0.5f);
            float dx = _mover.Position.x - transform.position.x;
            _move = new Vector2(Mathf.Clamp(dx, -1, 1), 0f);
        }

        base.Update();
    }
    public void SetFreeze()
    {
        Freeze();
    }
    public void SetUnFreeze()
    {
        Unfreeze();
    }
    public void PlayerLost()
    {
        _isPlayerFound = false;
    }
    public void PlayerFound()
    {
        _isPlayerFound=true;
    }
    public void SetPosition(Vector3 position)
    {
        transform.position= position;
    }
    protected override void ComputeVelocity()
    {
        if (_move.x > 0.01f)
        {
            OnDirectionAction?.Invoke(false);
        }
        else if (_move.x < -0.01f)
        {
            OnDirectionAction?.Invoke(true);
        }

        targetVelocity = _move * _maxSpeed;
    }
}
