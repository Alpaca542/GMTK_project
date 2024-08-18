using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnManager : MonoBehaviour
{
    [Header("Car Settings")]
    [SerializeField] private GameObject carPrefab; // The car prefab to spawn


    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 5f; // Time between spawns

    private void Start()
    {
        // Start spawning cars at regular intervals
        InvokeRepeating("SpawnCar", 0f, spawnInterval);
    }

    private void SpawnCar()
    {
        // Instantiate a new car at the start point
        //GameObject newCar = Instantiate(carPrefab, startPoint.position, Quaternion.identity);

        // Get the CarController component from the spawned car
        //CarController carController = newCar.GetComponent<CarController>();

        // Initialize the car with the start and end points
        //carController.Initialize(startPoint, endPoint);
    }
}
