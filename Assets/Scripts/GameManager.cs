using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //range from 0.5 to 1.5
    public float scale;
    public bool increasing;
    public DialogueScript dlgMng;
    public Tornado tornado;

    void Update()
    {
        scale += increasing ? 0.05f : -0.05f;

        if (scale > 1.5)
        {
            increasing = false;
        }
        if (scale < 0.5)
        {
            increasing = true;
        }
    }
}
