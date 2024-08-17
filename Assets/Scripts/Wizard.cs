using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class wizard : MonoBehaviour
{
    [Header("Stats")]
    public float speed;

    [Header("Fields")]
    public Rigidbody2D rb;
    public GameObject wand;
    public GameObject reflector;
    public LineRenderer line;

    [Header("Debug")]
    public GameObject currentReflector;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float dirX = Input.GetAxis("Horizontal");
        float dirY = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            line.enabled = true;
            Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            currentReflector.GetComponent<Reflector>().chosen = true;
        }

        // if (Input.GetMouseButton(0))
        // {
        //     line.SetPosition(0, transform.position);
        //     line.SetPosition(1, currentReflector.transform.position);
        // }
        // else if (currentReflector != null)
        // {
        //     line.enabled = false;
        //     currentReflector = null;
        // }

        rb.velocity = new Vector2(dirX, dirY) * speed;
    }

    void Shoot(Vector2 where)
    {
        currentReflector = Instantiate(reflector, where, Quaternion.identity);
    }
}
