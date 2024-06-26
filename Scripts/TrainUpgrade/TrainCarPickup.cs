using UnityEngine;

public class TrainCarPickup : MonoBehaviour
{
    public TrainCar trainCar;
    public GameObject purchaseUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어 태그와 비교
        {
            purchaseUI.SetActive(true);
            purchaseUI.GetComponent<PurchaseUI>().Setup(trainCar, this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            purchaseUI.SetActive(false);
        }
    }

    public void PurchaseCar()
    {
        // 기차 칸을 구매하는 로직을 여기서 처리
        // TrainManager를 통해 처리 가능
    }
}
