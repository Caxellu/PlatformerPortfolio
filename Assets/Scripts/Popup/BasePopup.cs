using System;
using UnityEngine;

using Zenject;
public interface IBasePopup
{
    void Show();
    void Hide();
    public Enum Type { get; }
}
[RequireComponent(typeof(CanvasGroup))]
public abstract class BasePopup : MonoBehaviour, IBasePopup
{
    [Inject] private IRegisterPopup _registerPopup;
    public abstract Enum Type { get; }

    private CanvasGroup canvasGroup;
    public virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        _registerPopup.RegisterPopup(this);
    }
    public void Show()
    {
        OnShow();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        transform.SetSiblingIndex(100);
    }

    public virtual void Hide()
    {
        OnHide();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
    protected virtual void OnShow() { }
    protected virtual void OnHide() { }


}