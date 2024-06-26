using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgradeData", menuName = "Upgrade/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public string description;
    public int cost;
    public float buildIntervalMultiplier = 1.0f;
    public int capacityIncrease = 0;
    public int upgradeLevel = 1; // 업그레이드 레벨
}
