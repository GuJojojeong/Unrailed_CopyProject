using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadUpgradeScene()
    {
        SceneManager.LoadScene("UpgradeScene");
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
