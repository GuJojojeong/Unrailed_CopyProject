using UnityEngine;

public class TrainCarPickup : MonoBehaviour
{
    public TrainCar trainCar;
    public GameObject purchaseUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾� �±׿� ��
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
        // ���� ĭ�� �����ϴ� ������ ���⼭ ó��
        // TrainManager�� ���� ó�� ����
    }
}
