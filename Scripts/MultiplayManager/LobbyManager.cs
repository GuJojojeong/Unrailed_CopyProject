using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance;

    public Button[] mainMenuButtons; 
    public Button[] multiplayerMenuButtons; 

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
        ActivateLobbyOptions();
        PhotonManager.Instance.CreateLocalPlayer();
    }

    public void ActivateMultiplayerOptions()
    {
        SetButtonsActive(mainMenuButtons, false);
        SetButtonsActive(multiplayerMenuButtons, true);
    }

    public void ActivateLobbyOptions()
    {
        SetButtonsActive(mainMenuButtons, true);
        SetButtonsActive(multiplayerMenuButtons, false);
    }

    public void DeactivateOptions()
    {
        SetButtonsActive(mainMenuButtons, false);
        SetButtonsActive(multiplayerMenuButtons, false);
    }

    private void SetButtonsActive(Button[] buttons, bool isActive)
    {
        foreach (Button button in buttons)
        {
            if (button != null)
            {
                button.gameObject.SetActive(isActive);
            }
        }
    }
}
