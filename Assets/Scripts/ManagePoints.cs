using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManagePoints : MonoBehaviour
{
    public string[] levels = { "CutScene", "Level1", "Level2", "Level3", "Level4", "Level5" };
    private Dictionary<string, int> bestScores = new Dictionary<string, int>();
    [SerializeField] private GoodThingFinder goodThingFinder;

    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI badThingsLeft;
    private int score;

    private void Start()
    {
        ResetScore();
    }

    public void AddToScore(int points)
    {
        score += points;
        UpdateCurrentScoreText();
        UpdateBadThingsLeft();
    }
    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
        UpdateCurrentScoreText();
    }

    // call whenever a level is completed to update the best score
    public void UpdateBestScore(string levelName, int score)
    {
        if (!bestScores.ContainsKey(levelName))
        {
            bestScores[levelName] = score;
        }
        else if (score > bestScores[levelName])
        {
            bestScores[levelName] = score;
        }
        UpdateBestScoreText(levelName);

        // Save the best score for the current level
        SaveCurrentLevelScore(levelName);
    }

    //call to display the best score for a level.
    public int GetBestScore(string levelName)
    {
        if (bestScores.ContainsKey(levelName))
        {
            return bestScores[levelName];
        }
        return 0;
    }

    // at Game end 
    public void SaveScores()
    {
        foreach (var score in bestScores)
        {
            PlayerPrefs.SetInt(score.Key, score.Value);
        }
        PlayerPrefs.Save();
    }

    // at Game start
    public void LoadScores()
    {
        foreach (var level in levels)
        {
            if (PlayerPrefs.HasKey(level))
            {
                bestScores[level] = PlayerPrefs.GetInt(level);
            }
        }
    }

    private void UpdateCurrentScoreText()
    {
        if (currentScoreText != null)
        {
            currentScoreText.text = "Money: " + score.ToString();
        }
        //UpdateBadThingsLeft();
    }

    private void UpdateBestScoreText(string levelName)
    {
        if (bestScoreText != null)
        {
            bestScoreText.text = "High score:" + GetBestScore(levelName).ToString();
        }
    }
    public void SaveCurrentLevelScore(string levelName)
    {
        if (bestScores.ContainsKey(levelName))
        {
            PlayerPrefs.SetInt(levelName, bestScores[levelName]);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogWarning("No best score found for level: " + levelName);
        }
    }
    private void OnEnable()
    {
        GoodThingFinder.OnGoodThingsCreated += UpdateBadThingsLeft;
    }

    private void OnDisable()
    {
        GoodThingFinder.OnGoodThingsCreated -= UpdateBadThingsLeft;
    }

    private void UpdateBadThingsLeft()
    {
        badThingsLeft.text = "Targets Left: " + goodThingFinder.GetBadThingsLeft().ToString();
    }

}
