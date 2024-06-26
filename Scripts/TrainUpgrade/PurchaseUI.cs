using UnityEngine;
using UnityEngine.UI;

public class PurchaseUI : MonoBehaviour
{
    public Text descriptionText; // TextMeshPro �ؽ�Ʈ ������Ʈ
    public Button purchaseButton;
    public Button cancelButton;
    private TrainCar currentTrainCar;
    private TrainCarPickup currentPickup;

    public void Setup(TrainCar trainCar, TrainCarPickup pickup)
    {
        currentTrainCar = trainCar;
        currentPickup = pickup;
        descriptionText.text = $"{trainCar.carName}\n{trainCar.description}\n ���: {trainCar.scoreCost} ����Ʈ";
        purchaseButton.onClick.AddListener(PurchaseCar);
        cancelButton.onClick.AddListener(CancelPurchase);
    }

    public void PurchaseCar()
    {
        TrainManagers trainManager = FindObjectOfType<TrainManagers>();
        if (trainManager.PurchaseTrainCar(currentTrainCar))
        {
            currentPickup.PurchaseCar();
            Destroy(currentPickup.gameObject); // Ư�� ����ĭ�� ����
            gameObject.SetActive(false); // ���� UI�� ��Ȱ��ȭ
        }
    }

    public void CancelPurchase()
    {
        gameObject.SetActive(false); // ���� UI�� ��Ȱ��ȭ
    }

    private void OnDisable()
    {
        purchaseButton.onClick.RemoveListener(PurchaseCar);
        cancelButton.onClick.RemoveListener(CancelPurchase);
    }
}
