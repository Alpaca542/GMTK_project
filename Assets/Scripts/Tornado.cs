using System;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    Rigidbody2D rb;
    public Grid destructableGrid;
    public float speed;
    public Vector2 vel;

    public static event Action OnTornadoDie;

    [Header("Growth Settings")]
    [SerializeField] private Vector3 growthAmount = new Vector3(0.5f, 0.5f, 0f);

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = UnityEngine.Random.insideUnitCircle.normalized * speed;
        vel = rb.velocity;
    }

    public void Stop()
    {
        rb.velocity = Vector2.zero;
    }

    public void Go()
    {
        rb.velocity = vel;
    }
    void FixedUpdate()
    {
        transform.localScale -= new Vector3(0.1f, 0.1f, 0) * Time.fixedDeltaTime;
        if (transform.localScale.x <= 0.3f)
        {
            gameObject.SetActive(false);
            OnTornadoDie?.Invoke();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bounceOf")
        {
            rb.velocity = Vector2.Reflect(vel, collision.GetContact(0).normal).normalized * speed;
            vel = rb.velocity;
        }
    }

    public void Grow()
    {
        transform.localScale += growthAmount;
    }
}
