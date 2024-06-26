using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviourPun
{        
    public PlayerMovements controllers;
    private PlayerNameTag playerNameTag;

    private void Awake()
    {        
        controllers = GetComponent<PlayerMovements>();
        playerNameTag = GetComponentInChildren<PlayerNameTag>();
    }

    private void Start()
    {
        if (photonView.IsMine)
        {
            SetCameraTarget();
        }
    }

    public void SetCameraTarget()
    {
        CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
        if (cameraFollow != null)
        {
            cameraFollow.SetTarget(transform);
        }
    }    
}


