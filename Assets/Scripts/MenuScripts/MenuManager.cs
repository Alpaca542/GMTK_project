using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button[] buttonList;
    public GameObject levelButtonLay;

    public GameObject startAnim;

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
