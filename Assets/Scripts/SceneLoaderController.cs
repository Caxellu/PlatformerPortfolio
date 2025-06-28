using UnityEngine.SceneManagement;

public class SceneLoaderController
{
    private SceneType _targetSceneType;
    public void LoadScene(SceneType sceneType)
    {
        _targetSceneType = sceneType;
        switch (sceneType)
        {
            case SceneType.MainMenu:
                SceneManager.LoadScene(0);
                break;
            case SceneType.Level:
                SceneManager.LoadScene(1);
                break;
        }
    }

}
public enum SceneType
{
     MainMenu, Level
}