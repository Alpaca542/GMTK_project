using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button[] buttonList;
    public GameObject levelButtonLay;

    private void Awake()
    {
        ButtonsToArray();
        int unlookedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
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
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("UnlockedLevel", 1));
    }

    public void OpenLevel(int level)
    {
        SceneManager.LoadScene("Level" + level);
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
