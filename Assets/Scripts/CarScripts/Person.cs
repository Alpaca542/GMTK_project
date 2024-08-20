using UnityEngine;

public class Person : MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    private float zigZagFrequency = 2f;
    private float zigZagAmplitude = 1f;
    private float circleRadius = 2f;
    private float switchTime = 3f;
    private float switchTimer;
    private MovementType currentMovementType;
    public GameObject myParticles;

    private enum MovementType
    {
        Straight,
        ZigZag,
        Circular
    }

    private void Start()
    {
        // Randomize the zig-zag pattern slightly for each person
        zigZagFrequency += Random.Range(-0.5f, 0.5f);
        zigZagAmplitude += Random.Range(-0.5f, 0.5f);

        // Start with a random movement type
        currentMovementType = (MovementType)Random.Range(0, System.Enum.GetValues(typeof(MovementType)).Length);
        switchTimer = switchTime;
    }

    public void Initialize(Vector2 moveDirection, float moveSpeed)
    {
        direction = moveDirection;
        speed = moveSpeed;
    }

    private void Update()
    {
        switchTimer -= Time.deltaTime;

        if (switchTimer <= 0)
        {
            SwitchMovementType();
            switchTimer = switchTime;
        }

        switch (currentMovementType)
        {
            case MovementType.Straight:
                MoveStraight();
                break;
            case MovementType.ZigZag:
                MoveZigZag();
                break;
            case MovementType.Circular:
                MoveCircular();
                break;
        }
    }

    private void MoveStraight()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void MoveZigZag()
    {
        Vector2 zigZagOffset = Vector2.Perpendicular(direction) * Mathf.Sin(Time.time * zigZagFrequency) * zigZagAmplitude;
        Vector2 finalDirection = (direction + zigZagOffset).normalized;
        transform.Translate(finalDirection * speed * Time.deltaTime, Space.World);
    }

    private void MoveCircular()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) + (Mathf.PI / circleRadius * Time.deltaTime);
        direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void SwitchMovementType()
    {
        currentMovementType = (MovementType)Random.Range(0, System.Enum.GetValues(typeof(MovementType)).Length);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy the person if they hit anything that isn't the player
        if (!other.CompareTag("Player"))
        {
            Instantiate(myParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}