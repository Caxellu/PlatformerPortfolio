using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuController : MonoBehaviour
{
    [Inject] private SceneLoaderController _sceneLoaderController;
    [SerializeField] private Button _startBtn;

    private void Awake()
    {
        _startBtn.onClick.AddListener(()=>_sceneLoaderController.LoadScene(SceneType.Level));
    }
}
