using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button[] buttonList;
    public GameObject levelButtonLay;

    public GameObject startAnim;
    public TMP_Text[] highScoreTexts;

    private void Awake()
    {
        ButtonsToArray();
        int unlookedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        Debug.Log(unlookedLevel);
        for (int i = 0; i < buttonList.Length; i++)
        {
            if (i < unlookedLevel)
            {
                buttonList[i].interactable = true;
            }
            else
            {
                buttonList[i].interactable = false;
            }
            string levelName = "Level" + (i + 1);
            int highScore = PlayerPrefs.GetInt(levelName, 0);
            if (highScore > 0)
            {
                highScoreTexts[i].text = "High Score: " + highScore.ToString();
            }
            else
            {
                highScoreTexts[i].text = "";
            }

        }
    }

    public void PlayGame()
    {
        Invoke(nameof(LoadGameScene), 1f);
    }

    public void LoadGameScene()
    {
        StartCoroutine(LoadLevelScene(PlayerPrefs.GetInt("UnlockedLevel", 1)));
    }

    public void OpenLevel(int level)
    {
        StartCoroutine(LoadLevelScene(level));
    }

    public IEnumerator LoadLevelScene(int level)
    {
        startAnim.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        if (level == 1)
        {
            SceneManager.LoadScene("CutScene");
        }
        else
        {
            SceneManager.LoadScene("Level" + level);
        }
    }

    private void ButtonsToArray()
    {
        int childCount = levelButtonLay.transform.childCount;
        buttonList = new Button[childCount];
        for (int i = 0; i < childCount; i++)
        {
            buttonList[i] = levelButtonLay.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }

}
