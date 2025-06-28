using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonInputProxy : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private UnityAction _onActionPointerDown;
    private UnityAction _onActionPointerUp;

    public void Initialize(UnityAction onActionPointerDown)
    {
        this._onActionPointerDown = onActionPointerDown;
    }
    public void Initialize(UnityAction onActionPointerDown, UnityAction onActionPointerUp)
    {
        this._onActionPointerDown = onActionPointerDown;
        this._onActionPointerUp = onActionPointerUp;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _onActionPointerDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _onActionPointerUp?.Invoke();
    }
}