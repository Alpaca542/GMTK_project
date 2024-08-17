using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public Vector2 vel;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Random.insideUnitCircle * speed;
        vel = rb.velocity;
    }

    private void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.Reflect(vel, collision.GetContact(0).normal);
        vel = rb.velocity;
    }
}
