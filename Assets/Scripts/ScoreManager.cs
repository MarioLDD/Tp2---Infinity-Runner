using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text bestScoreText;
    [SerializeField] private int avoidPoints = 100;
    [SerializeField] private int pointsPerSecond = 10;
    private float score;
    private int highScore;
    public int Score { get { return Mathf.FloorToInt(score); } }
    public int HighScore { get { return highScore; } }

    private void Start()
    {
        if (PlayerPrefs.HasKey("BestScore") == true)
        {
            highScore = PlayerPrefs.GetInt("BestScore");
            bestScoreText.text = $"HighScore: {highScore}";
        }
    }

    void Update()
    {
        score += pointsPerSecond * Time.deltaTime;

        scoreText.text = $"Score: {Score}";

        if (score > highScore && bestScoreText.IsActive())
        {
            bestScoreText.enabled = false;
            highScore = Score;
        }
    }
    private void ScoreUpdate()
    {
        score += avoidPoints;

        if (score > highScore)
        {
            bestScoreText.enabled = false;
            highScore = Score;
        }
    }
    private void OnEnable()
    {
        EnemyController.OnAvoidObstacleEvent += ScoreUpdate;
    }
    private void OnDisable()
    {
        EnemyController.OnAvoidObstacleEvent -= ScoreUpdate;
    }
}