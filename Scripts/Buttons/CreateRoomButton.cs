using UnityEngine;
using UnityEngine.UI;

public class CreateRoomButton : ButtonBase
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
        PhotonManager.Instance.CreateRoom();
    }        
}
