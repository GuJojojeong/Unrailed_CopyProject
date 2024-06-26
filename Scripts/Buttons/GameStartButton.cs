using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class GameStartButton : ButtonBase
{
    protected override void OnButtonClick()
    {
        if (PhotonNetwork.IsMasterClient) // 방장인지 확인
        {
            base.OnButtonClick();
        }
    }

    public override void Execute()
    {
        GameObject lobbyUI = GameObject.Find("LobbyUI");
        if (lobbyUI != null)
        {
            Destroy(lobbyUI);
        }

        RoomManager.Instance.DeactivateRoomOptions();                
        PhotonManager.Instance.StartMultiPlayerGame();
    }
}