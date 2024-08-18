using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car Settings")]
    [SerializeField] private float speed = 7f;

    private Transform startPoint;
    private Transform endPoint;
    private Vector2 direction;

    public void Initialize(Transform start, Transform end)
    {
        startPoint = start;
        endPoint = end;

        // Set the car's initial position to the start point
        transform.position = startPoint.position;

        // Determine the direction to the end point
        direction = (endPoint.position - startPoint.position).normalized;
    }

    private void Update()
    {
        // Move the car towards the end point
        transform.Translate(direction * speed * Time.deltaTime);

        // Check if the car has reached the end point
        if (Vector2.Distance(transform.position, endPoint.position) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Tornado")
        {
            // Destroy the car if it collides with the Tornado
            Destroy(gameObject);
        }
    }
}