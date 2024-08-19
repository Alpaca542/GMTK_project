using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieInTIme : MonoBehaviour
{
    public float seconds;
    void Start()
    {
        Invoke(nameof(Die), seconds);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
