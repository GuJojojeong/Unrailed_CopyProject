using UnityEngine;
using UnityEngine.SceneManagement;

public class SinglePlayerButton : ButtonBase
{    
    public override void Execute()
    {
        GameObject lobbyUI = GameObject.Find("LobbyUI");
        if (lobbyUI != null)
        {
            Destroy(lobbyUI);
        }

        // 플레이어 객체 유지
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            DontDestroyOnLoad(player);
        }

        PhotonManager.Instance.StartSinglePlayerGame();
    }
}
