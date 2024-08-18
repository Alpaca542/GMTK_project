using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using DG.Tweening;

public class Reflector : MonoBehaviour
{
    public int myType;
    private float JustHit;

    [SerializeField] public float changeSize = 0.2f;
    [SerializeField] public float changeMas = 0.2f;
    public Vector3 ScaleFactor;

    private void Start()
    {
        ScaleFactor = new Vector3(changeSize, changeSize, 0);
    }

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
        gmb.transform.DOScale(gmb.transform.localScale - ScaleFactor, 0.5f);
        gmb.GetComponent<Rigidbody2D>().mass -= changeMas;

        if (gmb.transform.localScale.x < 0.5f)
        {
            //no hole
        }
    }

    void Grow(GameObject gmb)
    {
        gmb.transform.DOScale(gmb.transform.localScale + ScaleFactor, 0.5f);
        gmb.GetComponent<Rigidbody2D>().mass += changeMas;
    }
}
