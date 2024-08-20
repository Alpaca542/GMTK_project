using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnManager : MonoBehaviour
{
    [Header("Car Settings")]
    [SerializeField] private GameObject[] carPrefab;
    [SerializeField] private Transform[] points;
    [SerializeField] private float chanceOfBadCar;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float detectionRadius = 3.5f;

    [Header("Car Limit Settings")]
    [SerializeField] private int maxCars = 4;
    private int currentCarCount = 0;
    [SerializeField] private List<GameObject> carList = new List<GameObject>();
    private List<GameObject> cars = new List<GameObject>();
    private readonly object listLock = new object();
    private readonly object carListLock = new object();

    private void Start()
    {
        SpawnCarsOffMap();
        StartCoroutine(ActivateCars());
    }

    private void SpawnCarsOffMap()
    {
        for (int i = 0; i < maxCars; i++)
        {
            bool isBadCar = Random.value < chanceOfBadCar;
            int randomIndex = Random.Range(0, carPrefab.Length);
            GameObject selectedCarPrefab = carPrefab[randomIndex];
            Vector3 offMapPosition = new Vector3(transform.position.x - 1000, transform.position.y - 1000, -100f);

            GameObject newCar = Instantiate(selectedCarPrefab, offMapPosition, Quaternion.identity);
            CarController carController = newCar.GetComponent<CarController>();
            carController.points = points;
            carController.isBadCar = isBadCar;
            if (isBadCar)
            {
                carController.myValue = +50;
            }
            else
            {
                carController.myValue = -40;
            }

            cars.Add(newCar);
            carList.Add(newCar);
            currentCarCount++;
        }
    }

    private IEnumerator ActivateCars()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            HandleCarSpawning(); // Call the function to handle car spawning
        }
    }

    private void HandleCarSpawning()
    {
        lock (carListLock)
        {
            if (carList.Count > 0)
            {
                GameObject car = carList[0];
                carList.RemoveAt(0);
                MoveCarToStart(car);
            }
        }
    }

    private void MoveCarToStart(GameObject car)
    {
        Collider2D obstacle = Physics2D.OverlapCircle(points[0].position, detectionRadius, obstacleLayer);

        if (obstacle == null)
        {
            // If there's no obstacle, move the car to the start point and start its movement
            car.transform.position = points[0].position;
            car.GetComponent<CarController>().StartCar();
        }
        else
        {
            StartCoroutine(ResetCarRoutine(car));
        }
    }

    public void AddCarToList(CarController car)
    {
        if (cars.Contains(car.gameObject))
        {
            lock (listLock)
            {
                carList.Add(car.gameObject);
                car.transform.position = new Vector3(transform.position.x - 1000, transform.position.y - 1000, -100f);
            }

        }
    }

    public void TriggerCarSpawn()
    {
        lock (carListLock)
        {
            if (carList.Count > 0)
            {
                GameObject car = carList[0];
                carList.RemoveAt(0);
                MoveCarToStart(car);
            }
        }
    }

    public void ResetCar(GameObject car)
    {
        StartCoroutine(ResetCarRoutine(car));
    }

    private IEnumerator ResetCarRoutine(GameObject car)
    {
        yield return new WaitForSeconds(1);

        AddCarToList(car.GetComponent<CarController>());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void OnEnable()
    {
        CarController.OnCarEnd += AddCarToList;
    }

    private void OnDisable()
    {
        CarController.OnCarEnd -= AddCarToList;
    }
}
