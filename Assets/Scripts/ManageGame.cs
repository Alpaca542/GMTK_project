using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageGame : MonoBehaviour
{
    public string sceneName;
    private ManagePoints mng;
    public GameObject pauseMenuPanel;

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        mng = GameObject.FindGameObjectWithTag("mngPoints").GetComponent<ManagePoints>();
        mng.LoadScores();
        mng.UpdateBestScore(sceneName, 0);
        pauseMenuPanel.SetActive(false);
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
