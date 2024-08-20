using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public float finaltime;
    void Update()
    {
        finaltime += Time.deltaTime;
        GetComponent<TMP_Text>().text = (Mathf.Round(finaltime * 100) / 100).ToString();
    }
}
