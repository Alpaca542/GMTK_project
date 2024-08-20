using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ManageGame : MonoBehaviour
{
    public string sceneName;
    private string winGameText = "You cleared the whole city!";
    private string loseGameText = "Your tornado died!";
    private ManagePoints mng;
    public GameObject pauseMenuPanel;
    public GameObject endGamePanel;
    public TextMeshProUGUI endGameText;
    public Button nextLevelButton;
    public Button restartLevelButton;
    public DialogueScript dlgMng;
    private void Start()
    {
        Time.timeScale = 1f;
        sceneName = SceneManager.GetActiveScene().name;
        mng = GameObject.FindGameObjectWithTag("mngPoints").GetComponent<ManagePoints>();
        mng.LoadScores();
        mng.UpdateBestScore(sceneName, 0);
        pauseMenuPanel.transform.localScale = Vector3.zero;
        endGamePanel.transform.localScale = Vector3.zero;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenuPanel.transform.DOScale(1.4f, 0.5f).SetUpdate(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenuPanel.transform.DOScale(0, 0.5f).SetUpdate(true);
    }
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        string levelNumber = sceneName.Replace("Level", "");
        int nextLevelNumber = int.Parse(levelNumber) + 1;
        if (nextLevelNumber < 6)
        {
            SceneManager.LoadScene("Level" + nextLevelNumber);
        }
        else
        {
            SceneManager.LoadScene("MenuScene");
        }

    }

    public void EndGameWin()
    {
        Debug.Log("EndGameWin");
        Time.timeScale = 0f;
        GetComponent<soundManager>().PlaySound(0, 0.8f, 1.2f, 0.5f);
        nextLevelButton.gameObject.SetActive(true);
        restartLevelButton.gameObject.SetActive(false);
        mng.UpdateBestScore(sceneName, mng.GetScore());
        mng.SaveCurrentLevelScore(sceneName);
        endGamePanel.transform.DOScale(1.4f, 0.5f).SetUpdate(true);
        endGameText.text = winGameText;
        UnlockNewLevel();
    }

    public void EndGameLose()
    {
        Debug.Log("EndGameLose");
        Time.timeScale = 0f;
        GetComponent<soundManager>().PlaySound(0, 0.8f, 1.2f, 0.5f);
        nextLevelButton.gameObject.SetActive(false);
        restartLevelButton.gameObject.SetActive(true);
        mng.UpdateBestScore(sceneName, mng.GetScore());
        mng.SaveCurrentLevelScore(sceneName);
        endGamePanel.transform.DOScale(1.4f, 0.5f).SetUpdate(true);
        endGameText.text = loseGameText;
        UnlockNewLevel();
    }

    private void UnlockNewLevel()
    {
        if (!sceneName.Contains("Level"))
        {
            Debug.Log("this is a testing only level!");
        }
        else
        {
            string levelNumber = sceneName.Replace("Level", "");
            int unlockedLevel = int.Parse(levelNumber) + 1;
            Debug.Log("UnlockedLevel" + unlockedLevel);
            PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel);
            PlayerPrefs.Save();
        }

    }

    private void OnEnable()
    {
        GoodThingFinder.OnAllGoodThingsDestroyed += EndGameWin;
        Tornado.OnTornadoDie += EndGameLose;
    }

    private void OnDisable()
    {
        GoodThingFinder.OnAllGoodThingsDestroyed -= EndGameWin;
        Tornado.OnTornadoDie -= EndGameLose;
    }

}
