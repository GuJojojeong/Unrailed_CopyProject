using UnityEngine;
using UnityEngine.UI;

public class JoinRoomButton : ButtonBase
{
    private LobbyManager lobbyManager;

    protected override void Awake()
    {
        base.Awake();
        lobbyManager = FindObjectOfType<LobbyManager>();
    }

    public override void Execute()
    {
        lobbyManager.DeactivateOptions();
        PhotonManager.Instance.JoinRoom();
    }        
}