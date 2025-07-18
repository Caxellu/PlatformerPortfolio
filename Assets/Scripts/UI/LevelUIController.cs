using UnityEngine;
using Zenject;

public class LevelUIController : MonoBehaviour
{
    [Inject] SignalBus _signalBus;
    [SerializeField] private ButtonInputProxy _rightMoveBtn;
    [SerializeField] private ButtonInputProxy _leftMoveBtn;
    [SerializeField] private ButtonInputProxy _jumpBtn;
    [SerializeField] private ButtonInputProxy _fireBtn;
    [SerializeField] private ButtonInputProxy _pauseBtn;
    private void OnEnable()
    {
        _rightMoveBtn.Initialize(RightMoveOnclick_started, RightMoveOnClick_canceled);
        _leftMoveBtn.Initialize(LeftMoveOnClick_started, LeftMoveOnClick_canceled);
        _jumpBtn.Initialize(JumpOnClick);
        _fireBtn.Initialize(FireOnClick);
        _pauseBtn.Initialize(PauseOnClick);
    }

    private void PauseOnClick()
    {
        _signalBus.Fire<PauseSignal>();
    }
    private void JumpOnClick()
    {
        _signalBus.Fire<JumpSignal>();
    }
    private void FireOnClick()
    {
        _signalBus.Fire<TryFireSignal>();
    }
    private void RightMoveOnclick_started()
    {
        _signalBus.Fire<StartRightMoveSignal>();
    }
    private void RightMoveOnClick_canceled()
    {
        _signalBus.Fire<StopRightMoveSignal>();
    }
    private void LeftMoveOnClick_started()
    {
        _signalBus.Fire<StartLeftMoveSignal>();
    }
    private void LeftMoveOnClick_canceled()
    {
        _signalBus.Fire<StopLeftMoveSignal>();
    }

}