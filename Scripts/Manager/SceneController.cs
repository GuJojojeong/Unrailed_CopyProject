using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private Train trainInstance;

    private void Start()
    {
        trainInstance = FindObjectOfType<Train>();
    }

    public void LoadUpgradeScene()
    {
        SceneManager.LoadScene("UpgradeScene");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "UpgradeScene")
        {
            if (trainInstance != null)
            {
                trainInstance.enabled = false; // Train ��ũ��Ʈ ��Ȱ��ȭ
            }
        }
        else if (scene.name == "GameScene")
        {
            if (trainInstance != null)
            {
                trainInstance.enabled = true; // Train ��ũ��Ʈ Ȱ��ȭ
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
