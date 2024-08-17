using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wans : MonoBehaviour
{
    private float currentVelocity;
    public float strength;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Tornado")
        {
            Debug.Log(123);
            other.GetComponent<Rigidbody2D>().AddForce((Vector2)transform.right * strength * Time.deltaTime * 1 / Vector2.Distance(transform.position, other.transform.position));
        }
    }
    private void Update()
    {
        LookAtMouse();
    }
    void LookAtMouse()
    {
        Vector2 movement = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float targetAngle = Mathf.Atan2(-movement.x, movement.y) * Mathf.Rad2Deg + 90;
        float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetAngle, ref currentVelocity, 0.1f);
        transform.rotation = Quaternion.Euler(0, 0, smoothedAngle);
    }
}
