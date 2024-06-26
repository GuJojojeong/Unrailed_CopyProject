using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public static PhotonManager Instance;
    public GameObject playerPrefab;
    private GameObject localPlayer;
    private GameObject networkPlayer;
    private Vector3 spawnPosition = new Vector3(0, 1.5f, 0);
    private int maxPlayers = 4;

    public GameObject gameStartButton;
    private PhotonView photonView;
    private bool isSceneLoading = false;

    private string[] koreanNames = {"효승", "우석", "유록", "정호", "관우", "성언", "지원",
                                   "성훈", "수진", "재철", "명민", "동욱", "재경", "승환" };
    private List<string> shuffledNames = new List<string>();
    private int nameIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        photonView = GetComponent<PhotonView>() ?? gameObject.AddComponent<PhotonView>();
        photonView.ViewID = 1;

        PhotonNetwork.AutomaticallySyncScene = true;
        ShuffleNames(); // 이름을 섞어서 초기화
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public void CreateLocalPlayer()
    {
        if (localPlayer == null)
        {
            localPlayer = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            DontDestroyOnLoad(localPlayer);
            SetCameraTarget(localPlayer);
            Debug.Log("Local player created.");
        }
    }

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;
        roomOptions.IsOpen = true; 
        roomOptions.IsVisible = true; 
        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        if (!PhotonNetwork.CurrentRoom.IsOpen)
        {            
            PhotonNetwork.LeaveRoom();
            return;
        }

        if (string.IsNullOrEmpty(PhotonNetwork.NickName))
        {
            PhotonNetwork.NickName = GenerateUniqueName();
        }

        if (networkPlayer == null)
        {
            networkPlayer = PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);
            if (localPlayer != null)
            {
                Destroy(localPlayer);
                localPlayer = null;
            }
            SetCameraTarget(networkPlayer);
        }
        else
        {
            SetCameraTarget(networkPlayer);
        }

        RoomManager.Instance.ActivateRoomOptions();
        UpdateRoomPlayerCount();
    }

    private string GenerateUniqueName()
    {
        if (nameIndex >= shuffledNames.Count)
        {
            Debug.LogError("All names have been used.");
            return null; // 모든 이름이 사용된 경우
        }
        return shuffledNames[nameIndex++];
    }

    private void ShuffleNames()
    {
        shuffledNames = new List<string>(koreanNames);
        for (int i = 0; i < shuffledNames.Count; i++)
        {
            int rnd = Random.Range(0, shuffledNames.Count);
            string temp = shuffledNames[i];
            shuffledNames[i] = shuffledNames[rnd];
            shuffledNames[rnd] = temp;
        }
        nameIndex = 0; // 인덱스를 초기화하여 처음부터 사용하게 함
    }

    private void ClearUsedNames()
    {
        ShuffleNames();
        PhotonNetwork.NickName = string.Empty; 
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        UpdateRoomPlayerCount();
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        UpdateRoomPlayerCount();
    }

    public void StartSinglePlayerGame()
    {
        if (!isSceneLoading)
        {
            isSceneLoading = true;
            SceneManager.LoadScene(2);
            StartCoroutine(SetSinglePlayerCameraTargetAfterLoad());

            Vector3 newPosition = localPlayer.transform.position;
            newPosition.y += 2;
            localPlayer.transform.position = newPosition;
        }
    }

    public void StartMultiPlayerGame()
    {
        if (!PhotonNetwork.IsMasterClient || isSceneLoading)
        {
            return;
        }

        isSceneLoading = true;
        photonView.RPC("LoadGameScene", RpcTarget.All);

        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
    }

    [PunRPC]
    private void LoadGameScene()
    {
        PhotonNetwork.LoadLevel(3);
        StartCoroutine(SetMultiPlayerCameraTargetAfterLoad());
    }

    private IEnumerator SetSinglePlayerCameraTargetAfterLoad()
    {
        yield return new WaitForSeconds(0.5f);

        if (localPlayer != null)
        {
            SetCameraTarget(localPlayer);            
        }

        CleanupLobbyUI();
        isSceneLoading = false;
    }

    private IEnumerator SetMultiPlayerCameraTargetAfterLoad()
    {
        yield return new WaitForSeconds(0.5f);

        photonView.RPC("CreateNetworkPlayerAfterLoad", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void CreateNetworkPlayerAfterLoad()
    {
        if (networkPlayer != null)
        {
            PhotonNetwork.Destroy(networkPlayer);
            networkPlayer = null;
        }
        Vector3 designatedPosition = new Vector3(10, 1, 4);

        networkPlayer = PhotonNetwork.Instantiate(playerPrefab.name, designatedPosition, Quaternion.identity);
        DontDestroyOnLoad(networkPlayer);
        SetCameraTarget(networkPlayer);

        CleanupLobbyUI();
        isSceneLoading = false;
    }

    private void CleanupLobbyUI()
    {
        GameObject lobbyUI = GameObject.Find("LobbyUI");
        if (lobbyUI != null)
        {
            Destroy(lobbyUI);
        }
    }

    private void SetCameraTarget(GameObject player)
    {
        if (player != null)
        {
            Player playerScript = player.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.SetCameraTarget();
            }
        }
    }

    public override void OnLeftRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
        }

        if (networkPlayer != null && networkPlayer.GetPhotonView().IsMine)
        {
            PhotonNetwork.Destroy(networkPlayer);
            networkPlayer = null;
        }

        if (localPlayer != null)
        {
            PhotonNetwork.Destroy(localPlayer);
            localPlayer = null;
        }

        ClearUsedNames(); 

        CreateLocalPlayer();

        RoomManager.Instance.DeactivateRoomOptions();
        LobbyManager.Instance.ActivateLobbyOptions();
    }

    private void OnDestroy()
    {
        if (networkPlayer != null && networkPlayer.GetPhotonView().IsMine)
        {
            PhotonNetwork.Destroy(networkPlayer);
            networkPlayer = null;
        }

        if (localPlayer != null)
        {
            PhotonNetwork.Destroy(localPlayer);
            localPlayer = null;
        }
    }

    private void UpdateRoomPlayerCount()
    {
        RoomManager.Instance.UpdateRoomPlayerCount(PhotonNetwork.CurrentRoom.PlayerCount, maxPlayers);
    }
}
