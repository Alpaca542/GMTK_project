using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using DG.Tweening;

public class Reflector : MonoBehaviour
{
    public int myType;
    private float JustHit;

    private void Update()
    {
        JustHit -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Tornado" && JustHit <= 0)
        {
            JustHit = 0.5f;

            if (myType == 1)
            {
                Shrink(other.gameObject);
            }
            else if (myType == 2)
            {
                Grow(other.gameObject);
            }
        }
    }

    void Shrink(GameObject gmb)
    {
        gmb.transform.DOScale(gmb.transform.localScale * 0.8f, 0.5f);
        gmb.GetComponent<Tornado>().Go();

        if (gmb.transform.localScale.x < 0.5f)
        {
            //no hole
        }
    }

    void Grow(GameObject gmb)
    {
        gmb.transform.DOScale(gmb.transform.localScale * 1.2f, 0.5f);
        gmb.GetComponent<Tornado>().Go();
    }
}
