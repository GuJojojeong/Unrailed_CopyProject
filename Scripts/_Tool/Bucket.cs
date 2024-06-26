using UnityEngine;

public class Bucket : MonoBehaviour
{
    [SerializeField] private float maxWaterAmount = 50f; // ��Ŷ�� ä�� �ִ� ���� ��
    [SerializeField] private Renderer bucketRenderer; // ��Ŷ�� Renderer ������Ʈ
    private float currentWaterAmount; // ���� ��Ŷ�� �ִ� ���� ��

    private void Start()
    {
        if (bucketRenderer == null)
        {
            bucketRenderer = GetComponent<Renderer>();
        }

        currentWaterAmount = maxWaterAmount; // ���� �� �ִ� ���� ������ ����
        UpdateBucketColor();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("�浹 �߻�");

        if (collision.gameObject.CompareTag("Train"))
        {
            Debug.Log("�� ����");
            // Train ��ü�� Train ������Ʈ�� ������
            Train train = collision.gameObject.GetComponent<Train>();

            if (train != null)
            {
                // ����ũ �޼��带 ȣ���Ͽ� �µ��� ���ҽ�Ŵ
               /* train.WaterTank(currentWaterAmount);

                // ���� �� ���
                SetWaterAmount(0);*/
            }
        }
        else if (collision.gameObject.CompareTag("Water"))
        {
            Debug.Log("�� ä���");
            // �� �±� ��ü�� �浹 ��, ��Ŷ�� ���� �ִ�ġ�� ä��
            SetWaterAmount(maxWaterAmount);
        }
    }

    private void UpdateBucketColor()
    {
        if (currentWaterAmount > 0)
        {
            bucketRenderer.material.color = Color.blue;
        }
        else
        {
            bucketRenderer.material.color = Color.gray; // ���� ���� �� �⺻ ����
        }
    }

    public void SetWaterAmount(float amount)
    {
        currentWaterAmount = amount;
        UpdateBucketColor();
    }
}
