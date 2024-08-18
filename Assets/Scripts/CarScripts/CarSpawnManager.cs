using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnManager : MonoBehaviour
{
    [Header("Car Settings")]
    [SerializeField] private GameObject carPrefab; // The car prefab to spawn
    [SerializeField] private Transform[] points; // The points for the car

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 5f; // Time between spawns

    private void Start()
    {
        // Start spawning cars at regular intervals
        InvokeRepeating("SpawnCar", 0f, spawnInterval);
    }

    private void SpawnCar()
    {
        //Instantiate a new car at the start point
        GameObject newCar = Instantiate(carPrefab, transform.position, Quaternion.identity);
        newCar.GetComponent<CarController>().points = points;
    }
}
