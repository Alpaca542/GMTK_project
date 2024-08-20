using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodThingFinder : MonoBehaviour
{
    [SerializeField] private static List<GoodThing> goodThings;
    public static event Action OnAllGoodThingsDestroyed;
    public static event Action OnGoodThingsCreated;

    private void Start()
    {
        goodThings = new List<GoodThing>();
        // Comment out automatic registration, we'll call this manually after map generation
        // RegisterAllGoodThings();
    }

    // Method to manually trigger registration after map generation
    public void RegisterAllGoodThingsDelayed()
    {
        RegisterAllGoodThings();
    }

    private void RegisterAllGoodThings()
    {
        GoodThing[] foundGoodThings = FindObjectsOfType<GoodThing>();
        foreach (GoodThing goodThing in foundGoodThings)
        {
            RegisterGoodThing(goodThing);
        }
    }

    public void RegisterGoodThing(GoodThing goodThing)
    {
        if (goodThing.myValue > 0 && !goodThings.Contains(goodThing))
        {
            goodThings.Add(goodThing);
            OnGoodThingsCreated?.Invoke();
        }
    }

    public void UnregisterGoodThing(GoodThing goodThing)
    {
        goodThings.Remove(goodThing);
        RegisterAllGoodThings(); // if somthin wasent added before do to timing
        Debug.Log("goodThing count: " + goodThings.Count);
        OnGoodThingsCreated?.Invoke();
        CheckForAllGoodThingsDestroyed();
    }

    private void CheckForAllGoodThingsDestroyed()
    {
        if (goodThings.Count == 0)
        {
            OnAllGoodThingsDestroyed?.Invoke();
        }
    }

    public List<GoodThing> GetGoodThings()
    {
        return new List<GoodThing>(goodThings);
    }

    public int GetBadThingsLeft() {
        return goodThings.Count;
    }
}
