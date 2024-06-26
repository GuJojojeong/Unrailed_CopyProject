using System.Collections.Generic;
using UnityEngine;

public class TrainUpgradeManager : MonoBehaviour
{
    [SerializeField]
    private TrainManager trainManager;

    [SerializeField]
    private Transform newCarPrefab;

    // 기차칸을 업그레이드합니다.
    public void UpgradeCar(int targetIndex)
    {
        if (targetIndex <= 0 || targetIndex > trainManager.cars.Count)
        {
            Debug.LogError("Invalid target index");
            return;
        }

        // 새로운 기차칸을 생성하고 추가합니다.
        Transform newCar = Instantiate(newCarPrefab, trainManager.transform);
        trainManager.AddCarAt(targetIndex, newCar);
    }
}

public class TrainManager : MonoBehaviour
{
    public List<Transform> cars;
    public float distanceBetweenCars = 2.0f;

    public void AddCarAt(int index, Transform newCar)
    {
        cars.Insert(index, newCar);

        // 새로운 기차칸의 초기 위치와 회전 설정
        newCar.localPosition = Vector3.zero;
        newCar.localRotation = Quaternion.identity;

        // 각 기차칸의 위치를 업데이트합니다.
        UpdateCarPositions();
    }

    private void UpdateCarPositions()
    {
        for (int i = 1; i < cars.Count; i++)
        {
            Transform currentCar = cars[i];
            Transform previousCar = cars[i - 1];
            Vector3 directionToPrevious = (previousCar.position - currentCar.position).normalized;
            Vector3 targetPosition = previousCar.position - directionToPrevious * distanceBetweenCars;
            currentCar.position = targetPosition;
            currentCar.rotation = Quaternion.LookRotation(previousCar.position - currentCar.position);
        }
    }
}
