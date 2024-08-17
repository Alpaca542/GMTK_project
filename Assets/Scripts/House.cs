using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    private ManagePoints mngPoints;
    private void Start()
    {
        mngPoints = GameObject.FindGameObjectWithTag("mngPoints").GetComponent<ManagePoints>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Tornado"))
        {
            mngPoints.MinusHouse();
            Destroy(gameObject);
        }
    }
}
