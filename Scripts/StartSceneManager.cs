using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartSceneManager : MonoBehaviour
{
    private void Start()
    {
        // PhotonManager 초기화
        if (PhotonManager.Instance == null)
        {
            GameObject photonManagerObj = new GameObject("PhotonManager");
            PhotonManager photonManager = photonManagerObj.AddComponent<PhotonManager>();            
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            LoadLobbyScene();
        }
    }

    private void LoadLobbyScene()
    {
        SceneManager.LoadScene(1);
    }
}
