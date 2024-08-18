using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    Rigidbody2D rb;
    public Grid destructableGrid;
    public float speed;
    public Vector2 vel;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Random.insideUnitCircle.normalized * speed;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bounceOf")
        {
            rb.velocity = Vector2.Reflect(vel, collision.GetContact(0).normal).normalized * speed;
            vel = rb.velocity;
        }

    }
    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.tag == "bounceOf")
    //     {
    //         rb.velocity = Vector2.Reflect(vel, collision.GetContact(0).normal).normalized * speed;
    //         vel = rb.velocity;
    //     }
    // }
}
