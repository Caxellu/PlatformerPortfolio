using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelCompletePopup : BasePopup, IInitializablePopup<bool>
{
    [Inject] private SceneLoaderController _sceneLoaderController;
    public override Enum Type => LevelPopupType.LevelComplete;
    [SerializeField] private Button _nextBtn;
    [SerializeField] private Button _homeBtn;
    public override void Awake()
    {
        _nextBtn.onClick.AddListener(NextBtnOnClick);
        _homeBtn.onClick.AddListener(MainMenuBtnOnClick);

        base.Awake();
    }
    public void Init(bool isAvailableNext)
    {
        if(!isAvailableNext)
            _nextBtn.gameObject.SetActive(false);
    }
    private void MainMenuBtnOnClick()
    {
        _sceneLoaderController.LoadScene(SceneType.MainMenu);
    }
    private void NextBtnOnClick()
    {

    }
}
