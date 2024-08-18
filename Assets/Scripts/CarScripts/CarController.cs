using UnityEngine;

public class CarController : GoodThing
{
    [Header("Car Settings")]
    [SerializeField] private float speed = 7f;

    private float currentVelocity;

    public Transform[] points;
    public bool isBadCar;
    private int currentPoint;

    private void Update()
    {
        Vector2 direction = points[currentPoint].position - transform.position;
        direction.Normalize();
        // Move the car towards the end point
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        LookAt(points[currentPoint].position);
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

    void LookAt(Vector3 what)
    {
        Vector2 movement = what - transform.position;
        float targetAngle = Mathf.Atan2(-movement.x, movement.y) * Mathf.Rad2Deg + 90;
        float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetAngle, ref currentVelocity, 0.1f);
        transform.rotation = Quaternion.Euler(0, 0, smoothedAngle);
    }
}