using UnityEngine;

public class Bucket : MonoBehaviour
{
    [SerializeField] private float maxWaterAmount = 50f; // 버킷에 채울 최대 물의 양
    [SerializeField] private Renderer bucketRenderer; // 버킷의 Renderer 컴포넌트
    private float currentWaterAmount; // 현재 버킷에 있는 물의 양

    private void Start()
    {
        if (bucketRenderer == null)
        {
            bucketRenderer = GetComponent<Renderer>();
        }

        currentWaterAmount = maxWaterAmount; // 시작 시 최대 물의 양으로 설정
        UpdateBucketColor();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("충돌 발생");

        if (collision.gameObject.CompareTag("Train"))
        {
            Debug.Log("물 공급");
            // Train 객체의 Train 컴포넌트를 가져옴
            Train train = collision.gameObject.GetComponent<Train>();

            if (train != null)
            {
                // 물탱크 메서드를 호출하여 온도를 감소시킴
               /* train.WaterTank(currentWaterAmount);

                // 물을 다 비움
                SetWaterAmount(0);*/
            }
        }
        else if (collision.gameObject.CompareTag("Water"))
        {
            Debug.Log("물 채우기");
            // 물 태그 객체와 충돌 시, 버킷의 물을 최대치로 채움
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
            bucketRenderer.material.color = Color.gray; // 물이 없을 때 기본 색상
        }
    }

    public void SetWaterAmount(float amount)
    {
        currentWaterAmount = amount;
        UpdateBucketColor();
    }
}
