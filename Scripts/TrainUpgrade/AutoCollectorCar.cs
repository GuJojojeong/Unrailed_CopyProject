using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCollectorCar : MonoBehaviour
{
    public CargoCar cargoCar; // 접근 수준을 public으로 설정
    public float collectionInterval = 3.0f; // 자원 수집 간격

    private float collectionTimer = 0f;

    private void Start()
    {
        cargoCar = FindObjectOfType<CargoCar>(); // 씬에서 CargoCar 컴포넌트를 찾아 설정
    }

    private void Update()
    {
        collectionTimer += Time.deltaTime;

        if (collectionTimer >= collectionInterval)
        {
            CollectResources();
            collectionTimer = 0f;
        }
    }

    private void CollectResources()
    {
        if (cargoCar == null) return; // cargoCar가 설정되지 않았다면 실행하지 않음

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5.0f); // 범위 내의 모든 콜라이더 검색
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Wood"))
            {
                cargoCar.AddResource(hitCollider.gameObject, CargoCar.ResourceType.Wood);
                Destroy(hitCollider.gameObject); // 수집한 자원을 파괴
            }
            else if (hitCollider.CompareTag("Stone"))
            {
                cargoCar.AddResource(hitCollider.gameObject, CargoCar.ResourceType.Stone);
                Destroy(hitCollider.gameObject); // 수집한 자원을 파괴
            }
        }
    }
}
