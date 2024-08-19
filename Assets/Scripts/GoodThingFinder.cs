using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodThingFinder : MonoBehaviour
{
    private static List<GoodThing> goodThings = new List<GoodThing>();
    public ManageGame gameManager;

    private void Start()
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
        if (goodThing.myValue > 0) { 
            goodThings.Add(goodThing);
        }
    }

    public void UnregisterGoodThing(GoodThing goodThing)
    {
        goodThings.Remove(goodThing);
        CheckForAllGoodThingsDestroyed();
    }

    private void CheckForAllGoodThingsDestroyed()
    {
        if (goodThings.Count == 0)
        {
            gameManager.EndGameWin();
        }
    }

    public List<GoodThing> GetGoodThings()
    {
        return new List<GoodThing>(goodThings); 
    }
}
