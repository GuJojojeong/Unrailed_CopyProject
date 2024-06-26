using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public static Train Instance { get; private set; }

    [SerializeField]
    private List<Transform> cars = new List<Transform>();

    public IReadOnlyList<Transform> Cars
    {
        get { return cars.AsReadOnly(); }
    }

    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float speedRate = 0.001f;
    [SerializeField]
    private float distanceBetweenCars = 2.0f;
    [SerializeField]
    private float trainSpeed = 0;

    public float TrainSpeed
    {
        get { return trainSpeed; }
        set { trainSpeed = value; }
    }

    public float detectionRadius = 3.0f;
    public LayerMask railLayerMask;

    [SerializeField]
    private List<Transform> waypoints = new List<Transform>();
    private int currentWaypointIndex = 0;

    private bool isDead = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            DetectRails();
            MoveTrain();
        }
    }

    void OnDrawGizmos()
    {
        if (cars != null && cars.Count > 0)
        {
            Gizmos.DrawWireSphere(cars[0].position, detectionRadius);
        }
    }

    void DetectRails()
    {
        if (cars != null && cars.Count > 0)
        {
            Collider[] detectedRails = Physics.OverlapSphere(cars[0].position, detectionRadius, railLayerMask);
            foreach (Collider rail in detectedRails)
            {
                if (!waypoints.Contains(rail.transform) && Vector3.Dot(cars[0].forward, (rail.transform.position - cars[0].position).normalized) > 0)
                {
                    waypoints.Add(rail.transform);
                }
            }

            Vector3 currentPosition = cars[0].position;
            waypoints.RemoveAll(waypoint => Vector3.Dot(cars[0].forward, (waypoint.position - currentPosition).normalized) <= 0);
        }
    }

    void MoveTrain()
    {
        if (currentWaypointIndex < waypoints.Count)
        {
            Transform targetWaypoint = waypoints[currentWaypointIndex];

            Transform leadCar = cars[0];
            Vector3 directionToWaypoint = (targetWaypoint.position - leadCar.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(directionToWaypoint);

            trainSpeed = Mathf.Min(trainSpeed + speed * speedRate * Time.deltaTime, speed);

            leadCar.position = Vector3.MoveTowards(leadCar.position, targetWaypoint.position, trainSpeed * Time.deltaTime);

            // Y축 회전 값만 변경
            float targetYRotation = targetRotation.eulerAngles.y;
            leadCar.rotation = Quaternion.Euler(leadCar.rotation.eulerAngles.x, targetYRotation, leadCar.rotation.eulerAngles.z);

            if (Vector3.Distance(leadCar.position, targetWaypoint.position) < 0.1f)
            {
                currentWaypointIndex++;
            }

            for (int i = 1; i < cars.Count; i++)
            {
                Transform currentCar = cars[i];
                Transform previousCar = cars[i - 1];

                Vector3 directionToPrevious = (previousCar.position - currentCar.position).normalized;
                Vector3 targetPositionForCar = previousCar.position - directionToPrevious * distanceBetweenCars;

                currentCar.position = Vector3.Lerp(currentCar.position, targetPositionForCar, trainSpeed * Time.deltaTime);

                // Y축 회전 값만 변경
                float carTargetYRotation = previousCar.rotation.eulerAngles.y;
                currentCar.rotation = Quaternion.Euler(currentCar.rotation.eulerAngles.x, carTargetYRotation, currentCar.rotation.eulerAngles.z);
            }
        }
        else
        {
            Explode();
        }
    }

    void Explode()
    {
        Debug.Log("Train exploded!");
        isDead = true;
    }

    internal void AddCar(Transform car)
    {
        cars.Add(car);
    }

    public void AttachCarBehindLast(Transform car)
    {
        Transform lastCar = cars[cars.Count - 1];
        car.position = lastCar.position - lastCar.forward * distanceBetweenCars;
        car.rotation = lastCar.rotation;
        car.SetParent(transform);
        AddCar(car);
    }
}
