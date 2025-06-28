using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelUIController : MonoBehaviour, IDisposable
{
    [Inject] SignalBus _signalBus;
    [SerializeField] private ButtonInputProxy _rightMoveBtn;
    [SerializeField] private ButtonInputProxy _leftMoveBtn;
    [SerializeField] private ButtonInputProxy _jumpBtn;
    [SerializeField] private ButtonInputProxy _fireBtn;
    [SerializeField] private ButtonInputProxy _pauseBtn;
    [SerializeField] private TextMeshProUGUI _patronText;
    private void OnEnable()
    {
        _rightMoveBtn.Initialize(RightMoveOnclick_started, RightMoveOnClick_canceled);
        _leftMoveBtn.Initialize(LeftMoveOnClick_started, LeftMoveOnClick_canceled);
        _jumpBtn.Initialize(JumpOnClick);
        _fireBtn.Initialize(FireOnClick);
        _pauseBtn.Initialize(PauseOnClick);
        _signalBus.Subscribe<UpdateAmmoSignal>(UpdateAmmo);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<UpdateAmmoSignal>(UpdateAmmo);
    }
    private void UpdateAmmo(UpdateAmmoSignal arg)
    {
        _patronText.text = $"{arg.Current:0}/{arg.Max:0}";
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