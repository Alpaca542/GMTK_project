using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car Settings")]
    [SerializeField] private float speed = 7f;

    public Transform[] points;

    private int currentPoint;

    private void Update()
    {
        Vector2 direction = transform.position - points[currentPoint].position;
        // Move the car towards the end point
        transform.Translate(direction * speed * Time.deltaTime);

        // Check if the car has reached the end point
        if (Vector2.Distance(transform.position, points[currentPoint].position) < 0.1f)
        {
            currentPoint++;
            if (currentPoint >= points.Length)
            {
                Destroy(gameObject);
            }
        }
    }
}