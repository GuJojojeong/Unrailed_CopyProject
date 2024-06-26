using UnityEngine;
using Photon.Pun;

public class NetworkObjectManager : MonoBehaviourPun
{
    public GameObject[] interactableObjects;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            foreach (var obj in interactableObjects)
            {
                var photonView = obj.GetComponent<PhotonView>();
                if (photonView == null)
                {
                    photonView = obj.AddComponent<PhotonView>();
                }

                if (photonView.ViewID == 0)
                {
                    photonView.ViewID = PhotonNetwork.AllocateViewID(false); // 고유 ID 할당
                }
            }
        }
    }
}