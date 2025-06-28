using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
public interface IInitializablePopup<T>
{
    void Init(T data);
}
public interface IRegisterPopup
{
    public void RegisterPopup(BasePopup popup);
}
public class PopupsController<T> : IPopupsController, IRegisterPopup where T : Enum
{
    public class Factory : PlaceholderFactory<PopupsController<T>> { }
    private List<BasePopup> _popups = new List<BasePopup>();
    private BasePopup _currentPopup;
    private UnityAction _onCloseAction;

    private Dictionary<T, Type> _popupTypes = new Dictionary<T, Type>();
    public void RegisterPopup(BasePopup popup)
    {
        _popups.Add(popup);
        _popupTypes.Add((T)popup.Type, popup.GetType());
    }
    public BasePopup ShowPopup(Enum type)
    {
        if (type is not T typed)
        {
            Debug.LogError($"[PopupsManager] Invalid popup type passed: {type}");
            return null;
        }

        BasePopup p = _popups.Find(x => x.Type.Equals(typed));
        if (p != null)
        {
            p.Show();
            _currentPopup = p;
            return p;
        }

        Debug.LogError($"[PopupsManager] Popup of type {type} not found.");
        return null;
    }

    public void ShowPopup<TData>(Enum type, TData data, UnityAction onCloseAction = null)
    {
        if (type is not T typed)
        {
            Debug.LogError($"[PopupsManager] Invalid popup type passed: {type}");
            return;
        }

        if (!_popupTypes.TryGetValue(typed, out var popupClassType))
        {
            Debug.LogError($"[PopupsManager] PopupType {typed} not mapped to any popup class.");
            return;
        }

        var popup = _popups.FirstOrDefault(p => p.GetType() == popupClassType);
        if (popup == null)
        {
            Debug.LogError($"[PopupsManager] Popup of type {popupClassType.Name} not found.");
            return;
        }

        if (popup is not IInitializablePopup<TData> initializable)
        {
            Debug.LogError($"[PopupsManager] Popup does not implement IInitializablePopup<{typeof(TData).Name}>.");
            return;
        }

        initializable.Init(data);
        popup.Show();
        _currentPopup = popup;
        _onCloseAction = onCloseAction;
    }
    public void HideCurrentPopup()
    {
        if (_currentPopup != null)
        {
            _onCloseAction?.Invoke();
            _currentPopup.Hide();
            _currentPopup = null;
        }
    }
}