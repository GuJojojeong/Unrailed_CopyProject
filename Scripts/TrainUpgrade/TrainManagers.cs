using System.Collections.Generic;
using UnityEngine;

public class TrainManagers : MonoBehaviour
{
    public List<TrainCar> availableTrainCars;
    public Transform trainParentTransform;
    private CargoCar cargoCar; // private으로 설정하고 Start에서 찾아 설정

    private void Start()
    {
        cargoCar = FindObjectOfType<CargoCar>(); // 씬에서 CargoCar 컴포넌트를 찾아 설정
    }

    public bool PurchaseTrainCar(TrainCar trainCar)
    {
        if (ScoreManager.Instance.SpendScore(trainCar.scoreCost))
        {
            trainCar.isPurchased = true;
            AddTrainCar(trainCar);
            Debug.Log($"{trainCar.carName} purchased and added to train.");
            return true;
        }
        else
        {
            Debug.Log("Not enough score!");
            return false;
        }
    }

    public void AddTrainCar(TrainCar trainCar)
    {
        GameObject newCar = Instantiate(trainCar.prefab);
        Train.Instance.AttachCarBehindLast(newCar.transform);

        if (trainCar.carName == "AutoCollector")
        {
            AutoCollectorCar autoCollector = newCar.GetComponent<AutoCollectorCar>();
            if (autoCollector != null)
            {
                autoCollector.cargoCar = cargoCar;
            }
        }
        else if (trainCar.carName == "RailBuilder")
        {
            RailBuilderCar railBuilder = newCar.GetComponent<RailBuilderCar>();
            if (railBuilder != null)
            {
                railBuilder.cargoCar = cargoCar;
            }
        }
        else if (trainCar.carName == "BrakeCar")
        {
            BrakeCar brakeCar = newCar.GetComponent<BrakeCar>();
            if (brakeCar != null)
            {
                brakeCar.train = Train.Instance;
            }
        }
    }
}
