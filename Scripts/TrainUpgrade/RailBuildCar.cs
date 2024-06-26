using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailBuilderCar : MonoBehaviourPun, IInUpgrade, IHighlightable
{
    public CargoCar cargoCar;
    public float buildInterval = 3.0f;
    public GameObject trackPrefab;
    public int woodRequired = 1;
    public int stoneRequired = 1;
    public UpgradeData[] upgradeData;
    public UpgradeUI upgradeUI;

    private float buildTimer = 0f;
    public int currentUpgradeLevel = 0;
    private Material originalMaterial;
    public Material highlightMaterial;
    private List<GameObject> producedTracks = new List<GameObject>();
    private const int maxTracks = 3; // 최대 쌓을 수 있는 레일 수
    private const float initialYPosition = 1.3f; // 초기 Y 좌표
    private const float yOffset = 0.2f; // 각 레일 간의 Y 좌표 간격

    private void Start()
    {
        cargoCar = FindObjectOfType<CargoCar>();
        if (cargoCar == null)
        {
            Debug.LogError("CargoCar not found in the scene!");
            return;
        }

        originalMaterial = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if (cargoCar == null) return;

        if (producedTracks.Count < maxTracks)
        {
            buildTimer += Time.deltaTime;

            if (buildTimer >= buildInterval)
            {
                BuildTrack();
                buildTimer = 0f;
            }
        }
    }

    private void BuildTrack()
    {
        if (cargoCar.GetResourceCount(CargoCar.ResourceType.Wood) >= woodRequired &&
            cargoCar.GetResourceCount(CargoCar.ResourceType.Stone) >= stoneRequired)
        {
            for (int i = 0; i < woodRequired; i++)
            {
                cargoCar.RemoveResource(CargoCar.ResourceType.Wood);
            }
            for (int i = 0; i < stoneRequired; i++)
            {
                cargoCar.RemoveResource(CargoCar.ResourceType.Stone);
            }

            float yPos = initialYPosition + (producedTracks.Count * yOffset);
            Vector3 trackPosition = new Vector3(transform.position.x, yPos, transform.position.z);
            GameObject newTrack = PhotonNetwork.Instantiate("object/rail", trackPosition, Quaternion.identity);
            newTrack.transform.SetParent(transform); // 레일을 제작칸의 자식으로 설정
            producedTracks.Add(newTrack);
        }
    }

    public void TakeTrack()
    {
        if (producedTracks.Count > 0)
        {
            GameObject trackToTake = producedTracks[producedTracks.Count - 1];
            producedTracks.RemoveAt(producedTracks.Count - 1);
            Destroy(trackToTake);
        }
    }

    public void Upgrade(UpgradeData upgradeData)
    {
        if (currentUpgradeLevel < 3)
        {
            buildInterval *= upgradeData.buildIntervalMultiplier;
            currentUpgradeLevel++;
            Debug.Log("RailBuilderCar upgraded to level " + currentUpgradeLevel);
        }
        else
        {
            Debug.Log("RailBuilderCar is already at maximum level");
        }
    }

    public void Interact()
    {
        upgradeUI.ShowUpgradeUI(this, upgradeData, currentUpgradeLevel);
    }

    public void Highlight()
    {
        GetComponent<Renderer>().material = highlightMaterial;
    }

    public void ResetHighlight()
    {
        GetComponent<Renderer>().material = originalMaterial;
    }
}