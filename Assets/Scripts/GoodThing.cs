using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodThing : MonoBehaviour
{
    public int myValue;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Tornado")
        {
            GameObject.FindGameObjectWithTag("MngPoints").GetComponent<ManagePoints>().loosePoints(myValue);
            Destroy(gameObject);
        }
    }
}
