using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class House : MonoBehaviour
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

    private int Bad;
    public int myValue;
    public Tornado tornado;
    private ManagePoints mngPoints;
    public Vector3 ScaleFactor;
    [SerializeField] public float changeSize = 0.2f;
    [SerializeField] public float changeMas = 0.2f;

    private float timeSinceLastSpawn;

    private void Start()
    {
        Bad = Random.Range(0, 2);

        if (Bad == 1)
        {
            transform.localScale = new Vector2(transform.localScale.x * Random.Range(0.8f, 1.2f), transform.localScale.y * Random.Range(0.8f, 1.2f));
        }

        mngPoints = GameObject.FindGameObjectWithTag("mngPoints").GetComponent<ManagePoints>();
        Debug.Log(mngPoints);
        ScaleFactor = new Vector3(changeSize, changeSize, 0);
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Tornado"))
        {
            if (Bad == 1)
            {
                Grow(collider.gameObject);
                //spawm particles
                GameObject.FindGameObjectWithTag("mngPoints").GetComponent<ManagePoints>().getPoints(myValue);
                Destroy(gameObject);
            }
            else
            {
                Grow(collider.gameObject);
                //spawm particles
                GameObject.FindGameObjectWithTag("mngPoints").GetComponent<ManagePoints>().loosePoints(myValue);
                Destroy(gameObject);
            }
        }
    }
    void Grow(GameObject gmb)
    {
        gmb.transform.DOScale(gmb.transform.localScale + ScaleFactor, 0.5f);
        gmb.GetComponent<Rigidbody2D>().mass += 0.2f;
    }
}
