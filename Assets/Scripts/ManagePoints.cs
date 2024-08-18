using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManagePoints : MonoBehaviour
{
    public int points;

    public int housesDestroyedGoal;
    public int carsDestroyedGoal;

    public int badHouses;
    public int badCars;

    public Gradient goodBarColors;

    public ManageGame gmMng;

    public void loosePoints(int howMany)
    {
        points += howMany;
    }

    public void getPoints(int howMany)
    {
        points -= howMany;
    }
}
