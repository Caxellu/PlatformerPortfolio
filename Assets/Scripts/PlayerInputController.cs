using System;
using Zenject;
public class PlayerInputController: IInitializable, IDisposable
{
    [Inject] private SignalBus _signalBus;
    [Inject] private PlayerController _playerController;
    PlayerInputAction playerInputAction;
    public void Initialize()
    {
        _signalBus.Subscribe<StartRightMoveSignal>(_playerController.StartRightMove);
        _signalBus.Subscribe<StopRightMoveSignal>(_playerController.StopRightMove);
        _signalBus.Subscribe<StartLeftMoveSignal>(_playerController.StartLeftMove);
        _signalBus.Subscribe<StartLeftMoveSignal>(_playerController.StopLeftMove);
        _signalBus.Subscribe<JumpSignal>(_playerController.TryJump);
        _signalBus.Subscribe<FireSignal>(_playerController.TryFire);

        playerInputAction = new PlayerInputAction();
        playerInputAction.Enable();

        playerInputAction.Gameplay.RightMove.started += ctx=> _playerController.StartRightMove();
        playerInputAction.Gameplay.RightMove.canceled += ctx => _playerController.StopRightMove();
        playerInputAction.Gameplay.LeftMove.started += ctx => _playerController.StartLeftMove();
        playerInputAction.Gameplay.LeftMove.canceled += ctx => _playerController.StopLeftMove();
        playerInputAction.Gameplay.Jump.started += ctx => _playerController.TryJump();
        playerInputAction.Gameplay.Fire.started += ctx => _playerController.TryFire();
    }
    public void Dispose()
    {
        _signalBus.Unsubscribe<StartRightMoveSignal>(_playerController.StartRightMove);
        _signalBus.Unsubscribe<StopRightMoveSignal>(_playerController.StopRightMove);
        _signalBus.Unsubscribe<StartLeftMoveSignal>(_playerController.StartLeftMove);
        _signalBus.Unsubscribe<StartLeftMoveSignal>(_playerController.StopLeftMove);
        _signalBus.Unsubscribe<JumpSignal>(_playerController.TryJump);
        _signalBus.Unsubscribe<FireSignal>(_playerController.TryFire);


        playerInputAction.Gameplay.RightMove.started -= ctx => _playerController.StartRightMove();
        playerInputAction.Gameplay.RightMove.canceled -= ctx => _playerController.StopRightMove();
        playerInputAction.Gameplay.LeftMove.started -= ctx => _playerController.StartLeftMove();
        playerInputAction.Gameplay.LeftMove.canceled -= ctx => _playerController.StopLeftMove();
        playerInputAction.Gameplay.Jump.started -= ctx => _playerController.TryJump();
        playerInputAction.Gameplay.Fire.started -= ctx => _playerController.TryFire();
    }
}
