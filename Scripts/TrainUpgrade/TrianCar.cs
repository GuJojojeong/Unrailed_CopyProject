using UnityEngine;

[CreateAssetMenu(fileName = "NewTrainCar", menuName = "Train/TrainCar")]
public class TrainCar : ScriptableObject
{
    public string carName;
    public int scoreCost; // 점수로 구매 비용
    public GameObject prefab;
    public string description;
    public bool isPurchased = false;
    public float specialEffectDuration; // 특수 효과 지속 시간
}
