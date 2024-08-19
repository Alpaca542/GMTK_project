using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class House : GoodThing
{
    [Header("Detection Settings")]
    [SerializeField] private float detectionRadius = 5f;

    [Header("People Settings")]
    [SerializeField] private GameObject personPrefab;
    [SerializeField] private int minPeople = 2;
    [SerializeField] private int maxPeople = 5;
    [SerializeField] private float spawnRadius = 2f;
    [SerializeField] private float runSpeed = 3f;
    [SerializeField] private float respawnCooldown = 10f;

    public bool Bad;
    public Tornado tornado;

    private float timeSinceLastSpawn;

    private void Start()
    {
        if (Bad)
        {
            GetComponent<SpriteRenderer>().material.SetColor("_Outlinecolor", Color.red);
            if (Random.Range(0, 2) == 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * 1.1f, transform.localScale.y * 0.7f, 1);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x * 0.8f, transform.localScale.y * 1.3f, 1);
            }
        }

        tornado = FindObjectOfType<Tornado>();

        if (tornado == null)
        {
            Debug.LogError("Tornado not found in the scene. Please ensure there is an active Tornado object."); // if we will have more then one we can make adjustments!
        }
        timeSinceLastSpawn = respawnCooldown;
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        Vector3 tornadoPosition = tornado.GetComponent<Rigidbody2D>().position;

        // Check if the tornado is within the detection radius and the cooldown
        if (Vector2.Distance(transform.position, tornadoPosition) < detectionRadius && timeSinceLastSpawn >= respawnCooldown)
        {
            SpawnPeople();
            timeSinceLastSpawn = 0f;
        }
    }

    private void SpawnPeople()
    {
        int numberOfPeople = Random.Range(minPeople, maxPeople);
        for (int i = 0; i < numberOfPeople; i++)
        {
            Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
            GameObject person = Instantiate(personPrefab, spawnPosition, Quaternion.identity);

            Vector2 directionAwayFromTornado = (spawnPosition - tornado.GetComponent<Rigidbody2D>().position).normalized;

            person.AddComponent<Person>().Initialize(directionAwayFromTornado, runSpeed);
        }
    }
}
