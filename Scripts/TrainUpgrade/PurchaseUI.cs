using UnityEngine;
using UnityEngine.UI;

public class PurchaseUI : MonoBehaviour
{
    public Text descriptionText; // TextMeshPro 텍스트 컴포넌트
    public Button purchaseButton;
    public Button cancelButton;
    private TrainCar currentTrainCar;
    private TrainCarPickup currentPickup;

    public void Setup(TrainCar trainCar, TrainCarPickup pickup)
    {
        currentTrainCar = trainCar;
        currentPickup = pickup;
        descriptionText.text = $"{trainCar.carName}\n{trainCar.description}\n 비용: {trainCar.scoreCost} 포인트";
        purchaseButton.onClick.AddListener(PurchaseCar);
        cancelButton.onClick.AddListener(CancelPurchase);
    }

    public void PurchaseCar()
    {
        TrainManagers trainManager = FindObjectOfType<TrainManagers>();
        if (trainManager.PurchaseTrainCar(currentTrainCar))
        {
            currentPickup.PurchaseCar();
            Destroy(currentPickup.gameObject); // 특수 기차칸을 제거
            gameObject.SetActive(false); // 구매 UI를 비활성화
        }
    }

    public void CancelPurchase()
    {
        gameObject.SetActive(false); // 구매 UI를 비활성화
    }

    private void OnDisable()
    {
        purchaseButton.onClick.RemoveListener(PurchaseCar);
        cancelButton.onClick.RemoveListener(CancelPurchase);
    }
}
