using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelCompletePopup : BasePopup
{
    [Inject] private SceneLoaderController _sceneLoaderController;
    public override Enum Type => LevelPopupType.LevelComplete;
    [SerializeField] private Button _homeBtn;
    public override void Awake()
    {
        _homeBtn.onClick.AddListener(MainMenuBtnOnClick);

        base.Awake();
    }
    private void MainMenuBtnOnClick()
    {
        _sceneLoaderController.LoadScene(SceneType.MainMenu);
    }
}
