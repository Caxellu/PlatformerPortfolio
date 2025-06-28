using System;
using UnityEngine.Events;

public interface IPopupsController
{
    BasePopup ShowPopup(Enum type);
    void ShowPopup<TData>(Enum type, TData data, UnityAction onCloseAction = null);
    void HideCurrentPopup();
}