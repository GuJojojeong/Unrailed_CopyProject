using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargoCar : MonoBehaviour, IInUpgrade, IHighlightable
{
    public int maxCapacity = 100;
    private int currentCapacity = 0;

    private List<GameObject> woodResources = new List<GameObject>();
    private List<GameObject> stoneResources = new List<GameObject>();
    public UpgradeData[] upgradeData;
    public UpgradeUI upgradeUI;
    public int currentUpgradeLevel = 0;
    private Material originalMaterial;
    public Material highlightMaterial;

    public enum ResourceType
    {
        Wood,
        Stone
    }

    void Start()
    {
        InitializeResources();
        originalMaterial = GetComponent<Renderer>().material;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeResources();
    }

    void InitializeResources()
    {
        currentCapacity = 0;
        woodResources.Clear();
        stoneResources.Clear();
    }

    public bool IsFull()
    {
        return currentCapacity >= maxCapacity;
    }

    public void AddResource(GameObject resource, ResourceType type)
    {
        if (!IsFull())
        {
            currentCapacity++;
            if (type == ResourceType.Wood)
            {
                woodResources.Add(resource);
            }
            else if (type == ResourceType.Stone)
            {
                stoneResources.Add(resource);
            }
        }
    }

    public void RemoveResource(ResourceType type)
    {
        if (currentCapacity > 0)
        {
            currentCapacity--;
            if (type == ResourceType.Wood && woodResources.Count > 0)
            {
                woodResources.RemoveAt(woodResources.Count - 1);
            }
            else if (type == ResourceType.Stone && stoneResources.Count > 0)
            {
                stoneResources.RemoveAt(stoneResources.Count - 1);
            }
        }
    }

    public int GetResourceCount(ResourceType type)
    {
        if (type == ResourceType.Wood)
        {
            return woodResources.Count;
        }
        else if (type == ResourceType.Stone)
        {
            return stoneResources.Count;
        }
        return 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wood"))
        {
            AddResource(other.gameObject, ResourceType.Wood);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Stone"))
        {
            AddResource(other.gameObject, ResourceType.Stone);
            Destroy(other.gameObject);
        }
    }

    public void Upgrade(UpgradeData upgradeData)
    {
        if (currentUpgradeLevel < 3)
        {
            maxCapacity += upgradeData.capacityIncrease;
            currentUpgradeLevel++;
            Debug.Log("CargoCar upgraded to level " + currentUpgradeLevel);
        }
        else
        {
            Debug.Log("CargoCar is already at maximum level");
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
