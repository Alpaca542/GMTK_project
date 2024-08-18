using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnManager : MonoBehaviour
{
    [Header("Car Settings")]
    [SerializeField] private GameObject[] carPrefab; // The car array prefab to spawn
    [SerializeField] private Transform[] points; // The points for the car
    [SerializeField] private float chanceOfBadCar;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 5f; // Time between spawns

    private void Start()
    {
        // Start spawning cars at regular intervals
        InvokeRepeating("SpawnCar", 0f, spawnInterval);
    }

    private void SpawnCar()
    {
        // random car is bad or good:
        bool isBadCar = Random.value < chanceOfBadCar;
        int randomIndex = Random.Range(0, carPrefab.Length);
        GameObject selectedCarPrefab = carPrefab[randomIndex];
        GameObject newCar = Instantiate(selectedCarPrefab, points[0].transform.position, Quaternion.identity);
        CarController carController = newCar.GetComponent<CarController>();
        carController.points = points;
        carController.isBadCar = isBadCar;
    }
}
