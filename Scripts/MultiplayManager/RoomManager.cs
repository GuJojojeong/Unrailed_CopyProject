using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    public Button roomCancelButton;
    public Button gameStartButton;
    public TMP_Text currentRoomPlayerText;

    private Button[] roomButtons;

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
        }
    }

    private void Start()
    {
        roomButtons = new Button[] { roomCancelButton, gameStartButton };
        DeactivateRoomOptions();

        if (gameStartButton != null)
        {            
            gameStartButton.onClick.AddListener(OnGameStartButtonClicked);
        }
    }

    public void ActivateRoomOptions()
    {
        SetButtonsActive(true);
        if (currentRoomPlayerText != null) currentRoomPlayerText.gameObject.SetActive(true);

        if (PhotonNetwork.IsMasterClient)
        {
            gameStartButton.gameObject.SetActive(true);
            gameStartButton.interactable = true;
        }
        else
        {
            gameStartButton.gameObject.SetActive(false);
        }
    }

    public void DeactivateRoomOptions()
    {
        SetButtonsActive(false);
        if (currentRoomPlayerText != null) currentRoomPlayerText.gameObject.SetActive(false);
    }

    private void SetButtonsActive(bool isActive)
    {
        foreach (Button button in roomButtons)
        {
            if (button != null)
            {
                button.gameObject.SetActive(isActive);
            }
        }
    }    

    private void OnGameStartButtonClicked()
    {        
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonManager.Instance.StartMultiPlayerGame();
        }
    }

    public void UpdateRoomPlayerCount(int currentPlayerCount, int maxPlayers)
    {
        if (currentRoomPlayerText != null)
        {
            currentRoomPlayerText.text = $"참여 인원 : {currentPlayerCount} / {maxPlayers}";
        }
    }
}
