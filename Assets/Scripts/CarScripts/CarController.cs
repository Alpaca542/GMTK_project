using UnityEngine;
using System.Collections;
using System;

public class CarController : GoodThing
{
    [Header("Car Settings")]
    [SerializeField] private float speed = 7f;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private LayerMask obstacleLayer;

    private float currentVelocity;
    private Collider2D carCollider;
    private bool isStopped;

    public Transform[] points;
    public bool isBadCar;
    private int currentPoint;
    private Rigidbody2D rb;
    private CarSpawnManager carSpawnManager;
    public static event Action<CarController> OnCarEnd;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        carCollider = GetComponent<Collider2D>();
        carSpawnManager = GetComponentInParent<CarSpawnManager>();

        if (isBadCar)
        {
            GetComponent<SpriteRenderer>().material.SetColor("_Outlinecolor", Color.red);
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * 1.1f, transform.localScale.y * 0.7f, 1);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x * 0.8f, transform.localScale.y * 1.3f, 1);
            }
        }
    }

    public void StartCar()
    {
        currentPoint = 0;
        isStopped = false;
    }

    private void Update()
    {
        if (!isStopped)
        {
            bool obstacleDetected = DetectObstacle();

            if (!obstacleDetected)
            {
                MoveTowardsNextPoint();
            }
            else
            {
                rb.velocity = Vector2.zero;
                StartCoroutine(StopForSeconds(5));
            }

            // Check if the car has reached the end point
            if (Vector2.Distance(transform.position, points[currentPoint].position) < 0.1f)
            {
                currentPoint++;
                if (currentPoint >= points.Length)
                {
                    currentPoint = 0;
                    isStopped = true;
                    OnCarEnd(this);
                }
            }
        }
    }

    private void MoveTowardsNextPoint()
    {
        Vector2 direction = points[currentPoint].position - transform.position;
        direction.Normalize();
        rb.velocity = direction * speed;
        LookAt(points[currentPoint].position);
    }

    private IEnumerator StopForSeconds(float seconds)
    {
        isStopped = true;
        yield return new WaitForSeconds(seconds);
        isStopped = false;
    }

    private bool DetectObstacle()
    {
        Vector2 forwardDirection = -transform.up;
        Vector2 leftDirection = Quaternion.Euler(0, 0, 30) * forwardDirection;
        Vector2 rightDirection = Quaternion.Euler(0, 0, -30) * forwardDirection;

        RaycastHit2D[] hitsFront = Physics2D.RaycastAll(transform.position, forwardDirection, detectionRange, obstacleLayer);
        RaycastHit2D[] hitsLeft = Physics2D.RaycastAll(transform.position, leftDirection, detectionRange, obstacleLayer);
        RaycastHit2D[] hitsRight = Physics2D.RaycastAll(transform.position, rightDirection, detectionRange, obstacleLayer);

        Debug.DrawRay(transform.position, forwardDirection * detectionRange, Color.red);
        Debug.DrawRay(transform.position, leftDirection * detectionRange, Color.red);
        Debug.DrawRay(transform.position, rightDirection * detectionRange, Color.red);

        // Check for obstacles
        if (IsObstacleDetected(hitsFront) || IsObstacleDetected(hitsLeft) || IsObstacleDetected(hitsRight))
        {
            return true;
        }

        return false;
    }

    private bool IsObstacleDetected(RaycastHit2D[] hits)
    {
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider != carCollider)
            {
                return true;
            }
        }
        return false;
    }

    void LookAt(Vector3 what)
    {
        Vector2 movement = what - transform.position;
        float targetAngle = Mathf.Atan2(-movement.x, movement.y) * Mathf.Rad2Deg + 180;
        float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetAngle, ref currentVelocity, 0.1f);
        transform.rotation = Quaternion.Euler(0, 0, smoothedAngle);
    }
}
