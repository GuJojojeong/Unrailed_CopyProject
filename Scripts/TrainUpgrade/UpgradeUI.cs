using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public Text costText;
    public Text effectText;
    public Button upgradeButton;
    public Button cancelButton;

    private RailBuilderCar railBuilderCar;
    private CargoCar cargoCar;
    private UpgradeData[] upgradeDataArray;
    private int currentUpgradeIndex;

    private void Start()
    {
        upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
        cancelButton.onClick.AddListener(OnCancelButtonClick);
        gameObject.SetActive(false);
    }

    public void ShowUpgradeUI(RailBuilderCar railCar, UpgradeData[] data, int currentLevel)
    {
        railBuilderCar = railCar;
        cargoCar = null;
        upgradeDataArray = data;
        currentUpgradeIndex = currentLevel;
        SetUpgradeInfo();
        gameObject.SetActive(true);
    }

    public void ShowUpgradeUI(CargoCar car, UpgradeData[] data, int currentLevel)
    {
        cargoCar = car;
        railBuilderCar = null;
        upgradeDataArray = data;
        currentUpgradeIndex = currentLevel;
        SetUpgradeInfo();
        gameObject.SetActive(true);
    }

    private void SetUpgradeInfo()
    {
        if (currentUpgradeIndex < upgradeDataArray.Length)
        {
            UpgradeData data = upgradeDataArray[currentUpgradeIndex];
            costText.text = "Cost: " + data.cost.ToString();
            effectText.text = data.description;
        }
        else
        {
            costText.text = "Max Level Reached";
            effectText.text = "";
            upgradeButton.interactable = false;
        }
    }

    private void OnUpgradeButtonClick()
    {
        if (currentUpgradeIndex < upgradeDataArray.Length && ScoreManager.Instance.SpendScore(upgradeDataArray[currentUpgradeIndex].cost))
        {
            if (railBuilderCar != null)
            {
                railBuilderCar.Upgrade(upgradeDataArray[currentUpgradeIndex]);
            }
            if (cargoCar != null)
            {
                cargoCar.Upgrade(upgradeDataArray[currentUpgradeIndex]);
            }
            currentUpgradeIndex++;
            SetUpgradeInfo();
        }
        else
        {
            Debug.Log("Not enough score or max level reached!");
        }
    }

    private void OnCancelButtonClick()
    {
        gameObject.SetActive(false);
    }
}
