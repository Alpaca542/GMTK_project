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

    private void Start()
    {
        SpawnCar();
    }

    private void SpawnCar()
    {
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, obstacleLayer);
        if (colliders.Length == 0)
        {
            bool isBadCar = Random.value < chanceOfBadCar;
            int randomIndex = Random.Range(0, carPrefab.Length);
            GameObject selectedCarPrefab = carPrefab[randomIndex];
            GameObject newCar = Instantiate(selectedCarPrefab, transform.position, Quaternion.identity);
            CarController carController = newCar.GetComponent<CarController>();
            carController.points = points;
            carController.isBadCar = isBadCar;
        }
        
        Invoke(nameof(SpawnCar), spawnInterval + Random.Range(-0.3f, 0.3f));
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
