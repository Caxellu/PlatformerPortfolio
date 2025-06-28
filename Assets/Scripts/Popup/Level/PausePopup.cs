using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PausePopup : BasePopup, IInitializablePopup<string>
{
    [Inject] private SceneLoaderController _sceneLoaderController;
    public override Enum Type => LevelPopupType.Pause;
    [SerializeField] private Button _closeBtn;
    [SerializeField] private Button _continueBtn;
    [SerializeField] private Button _homeBtn;
    [SerializeField] private TMP_Text _levelInfoText;
    public override void Awake()
    {
        _closeBtn.onClick.AddListener(ContinueBtnOnClick);
        _continueBtn.onClick.AddListener(ContinueBtnOnClick);
        _homeBtn.onClick.AddListener(MainMenuBtnOnClick);

        base.Awake();
    }
    public void Init(string levelName)
    {
        _levelInfoText.text = levelName;
    }
    private void MainMenuBtnOnClick()
    {
        _sceneLoaderController.LoadScene(SceneType.MainMenu);
    }
    private void ContinueBtnOnClick()
    {

    }
}
