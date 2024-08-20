using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class musicManager : MonoBehaviour
{
    public bool started;
    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Music").Length == 1)
        {
            GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().DOFade(0.6f, 1f);
            DontDestroyOnLoad(gameObject);
        }
    }
}
