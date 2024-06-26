using UnityEngine;
using Photon.Pun;

public class RoomCancelButton : ButtonBase
{    
    public override void Execute()
    {
        PhotonNetwork.LeaveRoom();
    }
}