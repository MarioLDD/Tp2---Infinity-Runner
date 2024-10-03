using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int pointsPerSecond = 10;
    private float score = 0;
    private float elapsedTime = 0f;

    void Update()
    {
        elapsedTime += Time.deltaTime;
        score += pointsPerSecond * Time.deltaTime;

        scoreText.text = $"Score: {Mathf.FloorToInt(score).ToString()}";
    }
}