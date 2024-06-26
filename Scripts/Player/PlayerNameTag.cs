using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerNameTag : MonoBehaviourPun
{
    public TMP_Text playerNameText;

    private void Start()
    {
        if (photonView.Owner != null)
        {
            playerNameText.text = photonView.Owner.NickName;            
        }
        return;
    }        
}
