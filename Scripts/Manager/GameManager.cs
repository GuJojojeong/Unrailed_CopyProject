using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Train train;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 GameManager 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnterUpgradeScene()
    {
        SceneManager.LoadScene("UpgradeScene");
        if (train != null)
        {
            train.enabled = false; // Train 스크립트 비활성화
        }
    }

    public void EnterGameScene()
    {
        SceneManager.LoadScene("GameScene");
        if (train != null)
        {
            train.enabled = true; // Train 스크립트 활성화
        }
    }
}
