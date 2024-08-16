using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Reflector : MonoBehaviour
{
    public bool placed;
    private float currentVelocity;

    private void Start()
    {
        placed = false;
    }

    void Update()
    {
        if (!placed)
        {
            LookAtMouse();
        }
    }

    void LookAtMouse()
    {
        Vector2 movement = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; ;
        float targetAngle = Mathf.Atan2(-movement.x, movement.y) * Mathf.Rad2Deg;
        float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetAngle, ref currentVelocity, 0.1f);
        transform.rotation = Quaternion.Euler(0, 0, smoothedAngle);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(123);
        if (other.tag == "Tornado")
        {
            Debug.Log(1234);
            other.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180 - other.transform.eulerAngles.z - transform.eulerAngles.z));
        }
    }
}
