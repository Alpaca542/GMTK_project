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
    public Animator anim;

    [Header("Debug")]
    public GameObject currentReflector;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();
        HandleWandToggle();

    }

    private void HandleMovement()
    {
        float dirX = Input.GetAxis("Horizontal");
        float dirY = Input.GetAxis("Vertical");

        float dirXRaw = Input.GetAxisRaw("Horizontal");
        float dirYRaw = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(dirXRaw) > 0 || Mathf.Abs(dirYRaw) > 0)
        {
            if (Mathf.Abs(dirYRaw) > 0)
            {
                if (dirYRaw > 0)
                {
                    anim.SetBool("GoRight", false);
                    anim.SetBool("GoLeft", false);
                    anim.SetBool("GoUp", true);
                    anim.SetBool("GoDown", false);
                }
                else
                {
                    anim.SetBool("GoRight", false);
                    anim.SetBool("GoLeft", false);
                    anim.SetBool("GoUp", false);
                    anim.SetBool("GoDown", true);
                }
            }
            else
            {
                if (dirXRaw > 0)
                {
                    anim.SetBool("GoRight", true);
                    anim.SetBool("GoLeft", false);
                    anim.SetBool("GoUp", false);
                    anim.SetBool("GoDown", false);
                }
                else
                {
                    anim.SetBool("GoRight", false);
                    anim.SetBool("GoLeft", true);
                    anim.SetBool("GoUp", false);
                    anim.SetBool("GoDown", false);
                }
            }
        }
        rb.velocity = new Vector2(dirX, dirY) * speed;
    }

    private void HandleWandToggle()
    {
        if (Input.GetMouseButton(0))
        {
            wand.SetActive(true);
        }
        else
        {
            wand.SetActive(false);
        }
    }
}
