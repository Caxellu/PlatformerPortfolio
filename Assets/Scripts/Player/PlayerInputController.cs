using System;
using Zenject;
public class PlayerInputController: IInitializable, IDisposable
{
    [Inject] private SignalBus _signalBus;
    [Inject] private PlayerController _playerController;
    [Inject] private PlayerFireUseCase _bulletController;
    PlayerInputAction playerInputAction;
    private bool _isRightHeld = false;
    private bool _isLeftHeld = false;
    public void Initialize()
    {
        _signalBus.Subscribe<StartRightMoveSignal>(StartRightMove);
        _signalBus.Subscribe<StopRightMoveSignal>(StopRightMove);
        _signalBus.Subscribe<StartLeftMoveSignal>(StartLeftMove);
        _signalBus.Subscribe<StopLeftMoveSignal>(StopLeftMove);
        _signalBus.Subscribe<JumpSignal>(_playerController.TryJump);
        _signalBus.Subscribe<TryFireSignal>(_bulletController.TryFire);

        playerInputAction = new PlayerInputAction();
        playerInputAction.Enable();

        playerInputAction.Gameplay.RightMove.performed += ctx=> StartRightMove();
        playerInputAction.Gameplay.RightMove.canceled += ctx => StopRightMove();
        playerInputAction.Gameplay.LeftMove.performed += ctx => StartLeftMove();
        playerInputAction.Gameplay.LeftMove.canceled += ctx => StopLeftMove();
        playerInputAction.Gameplay.Jump.started += ctx => _playerController.TryJump();
        playerInputAction.Gameplay.Fire.started += ctx => _bulletController.TryFire();
    }
    private void StartRightMove()
    {
        _isRightHeld = true;
        ReevaluateMove();
    }
    private void StopRightMove()
    {
        _isRightHeld = false;
        ReevaluateMove();
    }
    private void StartLeftMove()
    {
        _isLeftHeld = true;
        ReevaluateMove();
    }
    private void StopLeftMove()
    {
        _isLeftHeld = false;
        ReevaluateMove();
    }
    private void ReevaluateMove()
    {
        if (_isLeftHeld && !_isRightHeld)
        {
            _playerController.StartLeftMove();
        }
        else if (_isRightHeld && !_isLeftHeld)
        {
            _playerController.StartRightMove();
        }
        else
        {
            _playerController.StopMove();
        }
    }
    public void Dispose()
    {
        _signalBus.Unsubscribe<StartRightMoveSignal>(StartRightMove);
        _signalBus.Unsubscribe<StopRightMoveSignal>(StopRightMove);
        _signalBus.Unsubscribe<StartLeftMoveSignal>(StartLeftMove);
        _signalBus.Unsubscribe<StopLeftMoveSignal>(StopLeftMove);
        _signalBus.Unsubscribe<JumpSignal>(_playerController.TryJump);
        _signalBus.Unsubscribe<TryFireSignal>(_bulletController.TryFire);

        playerInputAction.Disable();

        playerInputAction.Gameplay.RightMove.performed -= ctx => StartRightMove();
        playerInputAction.Gameplay.RightMove.canceled -= ctx => StopRightMove();
        playerInputAction.Gameplay.LeftMove.performed -= ctx => StartLeftMove();
        playerInputAction.Gameplay.LeftMove.canceled -= ctx => StopLeftMove();
        playerInputAction.Gameplay.Jump.started -= ctx => _playerController.TryJump();
        playerInputAction.Gameplay.Fire.started -= ctx => _bulletController.TryFire();
    }
}
