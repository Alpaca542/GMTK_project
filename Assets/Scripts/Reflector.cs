using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using DG.Tweening;

public class Reflector : MonoBehaviour
{
    public bool placed;
    public int myType;
    private float currentVelocity;
    private float JustHit;

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
        JustHit -= Time.deltaTime;
    }

    void LookAtMouse()
    {
        Vector2 movement = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; ;
        float targetAngle = Mathf.Atan2(-movement.x, movement.y) * Mathf.Rad2Deg + 180;
        float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetAngle, ref currentVelocity, 0.1f);
        transform.rotation = Quaternion.Euler(0, 0, smoothedAngle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Tornado" && JustHit <= 0)
        {
            JustHit = 0.5f;

            if (myType == 1)
            {
                Reflect(other.gameObject);
            }
            else if (myType == 2)
            {
                Shrink(other.gameObject);
            }
            else if (myType == 3)
            {
                Grow(other.gameObject);
            }
        }
    }

    void Reflect(GameObject obj)
    {
        obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 360 - obj.transform.eulerAngles.z - transform.eulerAngles.z));
    }

    IEnumerator Shrink(GameObject gmb)
    {
        gmb.GetComponent<Tornado>().ShouldMove = false;
        gmb.transform.DOScale(gmb.transform.localScale * 0.8f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        gmb.GetComponent<Tornado>().ShouldMove = true;
    }

    IEnumerator Grow(GameObject gmb)
    {
        gmb.GetComponent<Tornado>().ShouldMove = false;
        gmb.transform.DOScale(gmb.transform.localScale * 1.2f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        gmb.GetComponent<Tornado>().ShouldMove = true;
    }
}
