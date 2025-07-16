using UnityEngine;
using Zenject;

public class EnemyControllerLogic
{
    private readonly EnemyView _view;
    private readonly EnemySO _data;
    private readonly IPlayerPos _playerPos;
    private readonly SignalBus _signalBus;
    private readonly PatrolPath _path;

    private PatrolPath.Mover _mover;
    private bool _isPlayerFound;

    public EnemyControllerLogic(EnemyView view, EnemySO data, IPlayerPos playerPos, SignalBus signalBus, PatrolPath path)
    {
        _view = view;
        _data = data;
        _playerPos = playerPos;
        _signalBus = signalBus;
        _path = path;

        _view.Health.Initialize(OnDeath, null, _data.MaxHp);
        _view.Animation.Initialize(_data.AnimatorController, _data.Speed);
    }

    public void Update()
    {
        Vector2 enemyPos = _view.transform.position;
        Vector2 playerPos = _playerPos.Position;

        if (!_isPlayerFound)
        {
            float dx = playerPos.x - enemyPos.x;

            bool isRight = _view.Animation.ISRightDirection; 

            if (Vector2.Distance(enemyPos, playerPos) < 6f)
            {
                if ((isRight && dx > 0) || (!isRight && dx < 0))
                {
                    _isPlayerFound = true;
                }
            }
        }

        if (_isPlayerFound)
        {
            float dx = playerPos.x - enemyPos.x;
            _view.Animation.move = new Vector2(Mathf.Clamp(dx, -1, 1), 0f);
        }
        else if (_path != null)
        {
            _mover ??= _path.CreateMover(_data.Speed * 0.5f);
            float dx = _mover.Position.x - enemyPos.x;
            _view.Animation.move = new Vector2(Mathf.Clamp(dx, -1, 1), 0f);
        }
    }

    public void TakeDamage(int amount)
    {
        _view.Health.TakeDamage(amount);
    }

    public void OnPlayerCollision(PlayerController player)
    {
        _signalBus.Fire(new EnemyCollisionSignal(_data.Damage));
    }

    private void OnDeath()
    {
        _view.gameObject.SetActive(false);
    }
}