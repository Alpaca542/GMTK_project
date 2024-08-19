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
    private List<GameObject> cars = new List<GameObject>();

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
            Vector3 offMapPosition = new Vector3(transform.position.x - 1000, transform.position.y-1000, -100f);

            GameObject newCar = Instantiate(selectedCarPrefab, offMapPosition, Quaternion.identity);
            CarController carController = newCar.GetComponent<CarController>();
            carController.points = points;
            carController.isBadCar = isBadCar;

            cars.Add(newCar);
            currentCarCount++;
        }
    }

    private IEnumerator ActivateCars()
    {
        foreach (GameObject car in cars)
        {
            yield return new WaitForSeconds(spawnInterval);
            MoveCarToStart(car);
        }
    }

    private void MoveCarToStart(GameObject car)
    {
        car.transform.position = points[0].position; // Move car to the first point in the route
        car.GetComponent<CarController>().StartCar(); // Start car movement
    }

    public void ResetCar(GameObject car)
    {
        StartCoroutine(ResetCarRoutine(car));
    }

    private IEnumerator ResetCarRoutine(GameObject car)
    {
        yield return new WaitForSeconds(spawnInterval);
        MoveCarToStart(car);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
