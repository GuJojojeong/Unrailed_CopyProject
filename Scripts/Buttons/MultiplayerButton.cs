using UnityEngine;

public class MultiplayerButton : ButtonBase
{
    private LobbyManager lobbyManager;
    protected override void Awake()
    {
        base.Awake();
        lobbyManager = FindObjectOfType<LobbyManager>();
    }

    public override void Execute()
    {
        lobbyManager.ActivateMultiplayerOptions();
    }
}