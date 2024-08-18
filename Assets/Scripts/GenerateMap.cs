using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public GameObject[] houses;
    public GameObject[] BigHouses;
    void Start()
    {
        GameObject[] places = GameObject.FindGameObjectsWithTag("HousePoints");
        GameObject[] places2 = GameObject.FindGameObjectsWithTag("BigHousePoints");

        foreach (GameObject gmb in places)
        {
            Instantiate(houses[Random.Range(0, houses.Length)], gmb.transform.position, gmb.transform.rotation);
        }
        foreach (GameObject gmb in places2)
        {
            Instantiate(BigHouses[Random.Range(0, BigHouses.Length)], gmb.transform.position, gmb.transform.rotation);
        }
    }
}
