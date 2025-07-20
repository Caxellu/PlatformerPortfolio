using System;
using Zenject;

public class PlayerPresenter: IDisposable
{
    private readonly SignalBus _signalBus;
    private readonly PlayerModel _model;
    private readonly IPlayerView _view;
    private readonly IPlayerMovement _movement;

    public PlayerPresenter(PlayerModel model, IPlayerView view, IPlayerMovement movement, SignalBus signalBus)
    {
        _signalBus = signalBus;
        _model = model;
        _view = view;
        _movement = movement;

        _view.OnTakeDamageAction += _model.Health.TakeDamage;

        _movement.OnDirectionAction += _view.SetPlayerDirection;
        _model.Health.OnDie += OnPlayerDied;
        _model.Health.OnHurt += _view.SetHurt;
        _model.Health.OnHurt += OnUpdatePlayerHp;
        OnUpdatePlayerHp();

        _movement.Initialize(_model.MaxSpeed,_model.JumpForce, _view.SetGrounded, _view.SetVelocityX,_view.SetVelocityY);

        _signalBus.Subscribe<RestartSignal>(RespawnPlayer);
        _signalBus.Subscribe<PlayerDeadSignal>(PlayerDead);
        _signalBus.Subscribe<FireSignal>(_view.SetFire);
        _signalBus.Subscribe<FreezeSignal>(_movement.SetFreeze);
        _signalBus.Subscribe<UnFreezeSignal>(_movement.SetUnFreeze);
    }

    public void Dispose()
    {
        _movement.OnDirectionAction -= _view.SetPlayerDirection;
        _model.Health.OnDie -= OnPlayerDied;
        _model.Health.OnHurt -= _view.SetHurt;
        _model.Health.OnHurt -= OnUpdatePlayerHp;

        _signalBus.Unsubscribe<RestartSignal>(RespawnPlayer);
        _signalBus.Unsubscribe<PlayerDeadSignal>(PlayerDead);
        _signalBus.Unsubscribe<FireSignal>(_view.SetFire);
        _signalBus.Unsubscribe<FreezeSignal>(_movement.SetFreeze);
        _signalBus.Unsubscribe<UnFreezeSignal>(_movement.SetUnFreeze);
    }
    private void RespawnPlayer()
    {
        _movement.SetPosition(_model.SpawnPos);
        _model.ResetHP();
        _view.SetActivePlayer(true);
    }
    private void OnUpdatePlayerHp()
    {
        _signalBus.Fire(new PlayerHPUpdateSignal(_model.Health.Hp));
    }
    private void OnPlayerDied()
    {
        _signalBus.Fire<PlayerDeadSignal>();
    }
    private void PlayerDead()
    {
        _movement.StopMove();
        _view.SetActivePlayer(false);
    }
}