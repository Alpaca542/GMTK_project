using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManagePoints : MonoBehaviour
{
    public int housesDestroyedGoal;
    public int carsDestroyedGoal;

    public int housesDestroyed;
    public int carsDestroyed;

    public TMP_Text housesDestroyedGoalTxt;
    public TMP_Text carsDestroyedGoalTxt;

    public Gradient goodBarColors;

    public ManageGame gmMng;

    private void UpdateText()
    {
        housesDestroyedGoalTxt.text = housesDestroyed.ToString() + "/" + housesDestroyedGoal.ToString();
        carsDestroyedGoalTxt.text = carsDestroyed.ToString() + "/" + carsDestroyedGoal.ToString();

        if (housesDestroyed >= housesDestroyedGoal)
        {
            housesDestroyedGoalTxt.text = "<i><s>" + housesDestroyedGoalTxt.text + "</i></s>";
            housesDestroyedGoalTxt.color = Color.green;
        }
    }

    public void MinusBadHouse()
    {
        housesDestroyed++;
        gmMng.AmountOfGood++;
        gmMng.UpdateCounter();
        UpdateText();
    }

    public void MinusBadCar()
    {
        carsDestroyed++;
        gmMng.AmountOfGood++;
        gmMng.UpdateCounter();
        UpdateText();
    }
}
