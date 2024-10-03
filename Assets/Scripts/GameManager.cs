using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.PlayerLoop.EarlyUpdate;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOver_Panel;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text bestScoreText;
    [SerializeField] private ScoreManager scoreManager;

    void Start()
    {
        scoreText.enabled = false;
        gameOver_Panel.SetActive(false);
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        int score = scoreManager.Score;
        int highScore = scoreManager.HighScore;
        gameOver_Panel.SetActive(true);
        bestScoreText.text = $"HighScore: {highScore}";
        if (score < highScore)
        {
            scoreText.enabled = true;
            scoreText.text = $"Current Score: {score}";
        }
        PlayerPrefs.SetInt("BestScore", highScore);
        PlayerPrefs.Save();
    }

    public void ResetGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        EnemyController.OnGameOverEvent += GameOver;
    }
    private void OnDisable()
    {
        EnemyController.OnGameOverEvent -= GameOver;
    }
}