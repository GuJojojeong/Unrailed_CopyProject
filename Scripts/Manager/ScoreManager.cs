using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int playerPoints = 1000;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 ScoreManager 유지

            if (transform.parent != null)
            {
                transform.SetParent(null); // 루트 오브젝트로 이동
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool SpendScore(int cost)
    {
        if (playerPoints >= cost)
        {
            playerPoints -= cost;
            return true;
        }
        return false;
    }
}
