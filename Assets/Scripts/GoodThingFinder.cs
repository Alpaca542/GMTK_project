using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodThingFinder : MonoBehaviour
{
    private static List<GoodThing> goodThings;
    public static event Action OnAllGoodThingsDestroyed;

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
        if (goodThing.myValue > 0)
        {
            goodThings.Add(goodThing);
        }
    }

    public void UnregisterGoodThing(GoodThing goodThing)
    {
        goodThings.Remove(goodThing);
        Debug.Log("goodThing count: " + goodThings.Count);
        CheckForAllGoodThingsDestroyed();
    }

    private void CheckForAllGoodThingsDestroyed()
    {
        if (goodThings.Count == 0)
        {
            OnAllGoodThingsDestroyed?.Invoke();
            // gameManager.EndGameWin(); // Uncomment if you have a game manager handling the win condition
        }
    }

    public List<GoodThing> GetGoodThings()
    {
        return new List<GoodThing>(goodThings);
    }
}
