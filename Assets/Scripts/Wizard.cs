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
        if (Input.GetMouseButton(0))
        {
            wand.SetActive(true);
        }

        rb.velocity = new Vector2(dirX, dirY) * speed;
    }
}
