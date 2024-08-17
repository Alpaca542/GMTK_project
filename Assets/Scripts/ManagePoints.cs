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

    public void MinusHouse()
    {
        housesDestroyed++;
        UpdateText();
    }

    public void MinusCar()
    {
        carsDestroyed++;
        UpdateText();
    }
}
