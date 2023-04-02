using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : Singleton<GameManager>
{
    public GameObject loseUI;
    public int points = 0;
    public TextMeshProUGUI scoreText;

    private int totalPoints;
    public TMP_Text TotalScore;

    public void StartGame()
    {
        Time.timeScale = 1;
    }

    private void ShowLoseUI()
    {
        loseUI.SetActive(true);
    }

    public void RepeatGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }
    public void OnGameOver()
    {
        ShowLoseUI();
        totalPoints = PlayerPrefs.GetInt("TotalScore");
        if (points > totalPoints)
        {
            totalPoints = points;
            PlayerPrefs.SetInt("TotalScore", totalPoints);
        }
        TotalScore.text = "Your Total Points:" + totalPoints.ToString();
        Time.timeScale = 0;
    }

    public void UpdateScore()
    {
        points++;
        scoreText.text = points.ToString();
    }

}
