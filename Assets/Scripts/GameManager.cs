using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.PlayerLoop.EarlyUpdate;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject gameOver_Panel;

    void Start()
    {
        gameOver_Panel.SetActive(false);
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        gameOver_Panel.SetActive(true);
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