using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ManageGame : MonoBehaviour
{
    public Slider destrBar;
    public int AmountOfGood;
    public int AmountOfBad;

    private void Start()
    {
        destrBar.maxValue = AmountOfBad + AmountOfGood;
        destrBar.value = AmountOfGood;
    }
    public void UpdateCounter()
    {
        destrBar.DOValue(AmountOfGood, (AmountOfGood - destrBar.value) / 5);
    }
}
