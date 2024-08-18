using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public GameObject[] houses;
    void Start()
    {
        GameObject[] places = GameObject.FindGameObjectsWithTag("HousePoints");

        foreach (GameObject gmb in places)
        {
            Instantiate(houses[Random.Range(0, houses.Length)], gmb.transform.position, gmb.transform.rotation);
        }
    }
}
