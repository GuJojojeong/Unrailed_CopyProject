using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class TrainController : MonoBehaviour
{
    
    public float speed = 5f;
    public float rotationSpeed = 2f; // 회전 속도
    public GameObject particle;
    private int currentRailIndex = 0;
    public Transform currentRail;
    public Transform nextRail;
    public bool isMoving = true;

    [SerializeField]
    private float currentTemperature = 20f;
    private float temperatureRate = 0.1f;
    [SerializeField]
    private bool isOnFire = false;

    void Start()
    {

        //railManager.RailAdded += OnRailAdded;

        if (RailManager.Instance.GetRailCount() > 0)
        {
            StartMoving();
        }
    }

    void Update()
    {
        if (isMoving)
        {
            if (isOnFire) return;
            MoveTrain();
            RotateTrain();
            //TrainTemperature();
        }
    }
    void OnRailAdded()
    {
        if (!isMoving && RailManager.Instance.GetRailCount() > 0)
        {
            StartMoving();
        }
    }

    void StartMoving()
    {
        currentRailIndex = 0;
        currentRail = RailManager.Instance.GetRailAt(currentRailIndex);
        nextRail = RailManager.Instance.GetRailAt(currentRailIndex + 1);
        isMoving = true;
    }

    void MoveTrain()
    {
        
        if (object.ReferenceEquals(nextRail, null))
        {
            Explode();
            isMoving = false;
            return;
        }

        Vector3 direction = (nextRail.position - currentRail.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, nextRail.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, nextRail.position) < 0.1f)
        {
            currentRailIndex++;
            currentRail = nextRail;
            nextRail = RailManager.Instance.GetRailAt(currentRailIndex + 1);
        }
    }

    void RotateTrain()
    {
        if (nextRail == null) return;

        Vector3 targetDirection = nextRail.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
    void TrainTemperature()
    {
        if (!isOnFire)
        {
            currentTemperature += currentTemperature * temperatureRate * Time.deltaTime;
            if (currentTemperature >= 80)
            {
                isOnFire = true;
            }
        }
    }
    public void WaterTank(float water)
    {

        if (isOnFire)
        {
            currentTemperature -= water;
            if (currentTemperature <= 20)
                isOnFire = false;
        }
    }
    void Explode()
    {
        Debug.Log("Train exploded!");
        GameObject go = Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(go, 4f);
        Destroy(gameObject);
    }

}
