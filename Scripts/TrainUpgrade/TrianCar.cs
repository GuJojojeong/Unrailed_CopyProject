using UnityEngine;

[CreateAssetMenu(fileName = "NewTrainCar", menuName = "Train/TrainCar")]
public class TrainCar : ScriptableObject
{
    public string carName;
    public int scoreCost; // ������ ���� ���
    public GameObject prefab;
    public string description;
    public bool isPurchased = false;
    public float specialEffectDuration; // Ư�� ȿ�� ���� �ð�
}
