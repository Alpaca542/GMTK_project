using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodThingFinder : MonoBehaviour
{
    private static List<GoodThing> goodThings;
    public static event Action OnAllGoodThingsDestroyed;
    //public ManageGame gameManager;

    private void Start()
    {
        goodThings = new List<GoodThing>();
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
        if (goodThing.myValue > 0) { 
            goodThings.Add(goodThing);
        }
    }

    public void UnregisterGoodThing(GoodThing goodThing)
    {
        goodThings.Remove(goodThing);
        Debug.Log("goodThing"+goodThings.Count);
        CheckForAllGoodThingsDestroyed();
    }

    private void CheckForAllGoodThingsDestroyed()
    {
        if (goodThings.Count == 0)
        {
            OnAllGoodThingsDestroyed?.Invoke();
           // gameManager.EndGameWin();
        }
    }

    public List<GoodThing> GetGoodThings()
    {
        return new List<GoodThing>(goodThings); 
    }
}
