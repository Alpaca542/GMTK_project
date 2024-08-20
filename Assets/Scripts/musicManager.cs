using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class musicManager : MonoBehaviour
{
    private void Start()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().DOFade(0.6f, 1f);
            DontDestroyOnLoad(gameObject);
        }
    }
}
