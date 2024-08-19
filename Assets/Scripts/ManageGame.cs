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
    public string winGameText = "Win the Game !";
    public string loseGameText = "U lose !";
    private ManagePoints mng;
    public GameObject pauseMenuPanel;
    public GameObject endGamePanel;
    public TextMeshProUGUI endGameText;
    public Button nextLevelButton;
    public Button restartLevelButton;

    private void Start()
    {
        Time.timeScale = 1f;
        sceneName = SceneManager.GetActiveScene().name;
        mng = GameObject.FindGameObjectWithTag("mngPoints").GetComponent<ManagePoints>();
        mng.LoadScores();
        mng.UpdateBestScore(sceneName, 0);
        pauseMenuPanel.SetActive(false);
        endGamePanel.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenuPanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenuPanel.SetActive(false);
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
        //SceneManager.LoadScene("MenuScene"); //TODO Remove this line and activate the following lines :

        string levelNumber = sceneName.Replace("Level", "");
        int nextLevelNumber = int.Parse(levelNumber) + 1;
        SceneManager.LoadScene("Level" + nextLevelNumber);

    }

    public void EndGameWin()
    {
        Time.timeScale = 0f;
        nextLevelButton.gameObject.SetActive(true);
        restartLevelButton.gameObject.SetActive(false);
        mng.UpdateBestScore(sceneName, mng.GetScore());
        mng.SaveCurrentLevelScore(sceneName);
        endGamePanel.SetActive(true);
        endGameText.text = winGameText;
        UnlockNewLevel();
    }

    public void EndGameLose()
    {
        Time.timeScale = 0f;
        endGamePanel.SetActive(true);
        nextLevelButton.gameObject.SetActive(false);
        restartLevelButton.gameObject.SetActive(true);
        endGameText.text = loseGameText;
    }

    private void UnlockNewLevel() {
        if (!sceneName.Contains("Level"))
        {
            Debug.Log("this is a testing only level!");
        }
        else { 
            string levelNumber = sceneName.Replace("Level", "");
            int unlockedLevel = int.Parse(levelNumber);
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

    /* public Slider destrBar;
     public int AmountOfGood;
     public int AmountOfBad;

     private void Start()
     {
         destrBar.maxValue = AmountOfBad + AmountOfGood;
         destrBar.value = AmountOfGood;
     }
     public void UpdateCounter()
     {
         destrBar.DOValue(AmountOfGood, (AmountOfGood - destrBar.value) / 5);
     }*/
}
