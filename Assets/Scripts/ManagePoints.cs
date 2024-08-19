using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManagePoints : MonoBehaviour
{
    [Header("Level Names:")]
    [SerializeField]public string[] levels = {"Level1", "Level2", "Level3"};
    private Dictionary<string, int> bestScores = new Dictionary<string, int>();

    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI bestScoreText;
    private int score;

    public void AddToScore(int points)
    {
        score += points;
        UpdateCurrentScoreText();
        Debug.Log("Score Updated: " + score);
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
            currentScoreText.text = "Score: " + score.ToString();
        }
    }

    private void UpdateBestScoreText(string levelName)
    {
        if (bestScoreText != null)
        {
            bestScoreText.text = "High Score" + levelName + ": " + GetBestScore(levelName).ToString();
        }
    }
}
