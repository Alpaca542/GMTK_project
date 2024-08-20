using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public GameObject[] houses;
    public GameObject[] BigHouses;

    public float chanceOfBadHouse;

    void Start()
    {
        StartCoroutine(GenerateMapAndRegisterGoodThings());
    }

    private IEnumerator GenerateMapAndRegisterGoodThings()
    {
        GameObject[] places = GameObject.FindGameObjectsWithTag("HousePoints");
        GameObject[] places2 = GameObject.FindGameObjectsWithTag("BigHousePoints");

        foreach (GameObject gmb in places)
        {
            bool isBadHouse = Random.value < chanceOfBadHouse;

            int myValue = isBadHouse ? 100 : -100;

            House newHouse = House.InstantiateHouse(houses[Random.Range(0, houses.Length)], gmb.transform.position, gmb.transform.rotation, myValue);
            newHouse.Bad = isBadHouse;
        }

        foreach (GameObject gmb in places2)
        {
            bool isBadHouse = Random.value < chanceOfBadHouse;

            int myValue = isBadHouse ? 100 : -100;

            House newHouse = House.InstantiateHouse(BigHouses[Random.Range(0, BigHouses.Length)], gmb.transform.position, gmb.transform.rotation, myValue);
            newHouse.Bad = isBadHouse;
        }

        yield return new WaitForEndOfFrame();


        GoodThingFinder goodThingFinder = FindObjectOfType<GoodThingFinder>();
        if (goodThingFinder != null)
        {
            goodThingFinder.RegisterAllGoodThingsDelayed();
        }
        else
        {
            Debug.LogError("GoodThingFinder not found in the scene.");
        }
    }
}
