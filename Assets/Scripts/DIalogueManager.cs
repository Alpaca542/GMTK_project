using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    [Header("Parametres")]
    public bool startImmediately;
    public bool StopTime;
    public float typingspeed = 0.02f;
    private float borderSize = 1;

    [Header("Content")]
    public string[] sentences;
    public Sprite[] faces;
    public int[] stopindexes = { 7 };

    [Header("Fields")]
    public TMP_Text Display;
    public Image Display2;
    private bool ShouldIStopAfterpb;
    private int IndexInMain;
    private string Stringpb;
    public GameObject btnContinue;
    public GameObject cnv;
    public GameObject cnvInGame;
    public GameObject cnvInGame2;
    public GameObject btnContinueFake;

    public GameObject animStart;
    public GameObject animEnd;

    IEnumerator coroutine;
    private void Start()
    {
        Time.timeScale = 1f;
        if (startImmediately)
        {
            coroutine = Type(sentences[IndexInMain], faces[IndexInMain], false);
            StartCoroutine(coroutine);
        }
        else
        {
            cnvInGame.SetActive(true);
            cnvInGame2.SetActive(true);
            btnContinue.SetActive(false);
            cnv.SetActive(false);
            IndexInMain = stopindexes[0];
        }
    }
    public void StartCrtnRemotely(string WhatToType, Sprite WhatToShow, bool ShouldIStopAfter)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = Type(WhatToType, WhatToShow, ShouldIStopAfter);
        StartCoroutine(coroutine);
    }
    public IEnumerator Type(string WhatToType, Sprite WhatToShow, bool ShouldIStopAfter)
    {
        // GetComponent<AudioSource>().loop = true;
        // GetComponent<AudioSource>().Play();
        if (StopTime)
        {
            Time.timeScale = 0f;
        }
        ShouldIStopAfterpb = ShouldIStopAfter;
        Stringpb = WhatToType;

        cnv.SetActive(true);
        cnvInGame.SetActive(false);
        cnvInGame2.SetActive(false);
        btnContinue.SetActive(false);
        btnContinueFake.SetActive(false);
        Display.text = "";
        Debug.Log(WhatToShow.rect.size.x);
        Display2.gameObject.GetComponent<RectTransform>().localScale = new Vector2(WhatToShow.rect.size.x, WhatToShow.rect.size.y) * (borderSize / Mathf.Max(WhatToShow.rect.size.x, WhatToShow.rect.size.y));

        Display2.sprite = WhatToShow;
        foreach (char letter1 in WhatToType.ToCharArray())
        {
            Display.text += letter1;
            if (letter1 == ".".ToCharArray()[0] || letter1 == "!".ToCharArray()[0] || letter1 == "?".ToCharArray()[0])
            {
                yield return new WaitForSecondsRealtime(0.1f);
            }
            else if (letter1 == " ".ToCharArray()[0])
            {
                yield return new WaitForSecondsRealtime(0.05f);
            }
            else
            {
                yield return new WaitForSecondsRealtime(typingspeed);
            }
        }
        //GetComponent<AudioSource>().loop = false;

        if (ShouldIStopAfter)
        {
            btnContinueFake.SetActive(true);
        }
        else
        {
            btnContinue.SetActive(true);
        }
    }
    public void StartMainLine()
    {
        coroutine = Type(sentences[IndexInMain], faces[IndexInMain], false);
        StartCoroutine(coroutine);
    }
    public void ContinueTyping()
    {
        if (cnv.activeSelf)
        {
            IndexInMain++;
            if (Array.IndexOf(stopindexes, IndexInMain) == -1)
            {
                coroutine = Type(sentences[IndexInMain], faces[IndexInMain], false);
                StartCoroutine(coroutine);
            }
            else
            {
                if (StopTime)
                {
                    Time.timeScale = 1f;
                }
                cnvInGame.SetActive(true);
                cnvInGame2.SetActive(true);
                btnContinue.SetActive(false);
                cnv.SetActive(false);

                if (IndexInMain == stopindexes[0])
                {
                    if (StopTime)
                    {

                    }
                    else
                    {
                        animStart.SetActive(true);
                        Invoke(nameof(StartTheGame), 1f);
                    }
                }
            }
        }
    }

    void StartTheGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void StopTyping()
    {
        if (StopTime)
        {
            Time.timeScale = 1f;
        }
        cnvInGame.SetActive(true);
        cnvInGame2.SetActive(true);
        btnContinue.SetActive(false);
        cnv.transform.DOMoveY(cnv.transform.position.y - 5, 1f);
        Camera.main.transform.parent.GetComponent<playerfollow>().enabled = true;
    }
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && cnv.activeSelf)
        {
            //gameObject.GetComponent<soundManager>().sound.loop = false;
            //GetComponent<AudioSource>().Stop();
            StopCoroutine(coroutine);
            //GetComponent<AudioSource>().loop = false;
            if (Display.text == Stringpb)
            {
                if (ShouldIStopAfterpb)
                {
                    StopTyping();
                }
                else
                {
                    ContinueTyping();
                }
            }
            else
            {
                if (ShouldIStopAfterpb)
                {
                    Display.text = Stringpb;
                    btnContinueFake.SetActive(true);
                }
                else
                {
                    Display.text = sentences[IndexInMain];
                    btnContinue.SetActive(true);
                }
            }
        }
    }
}