using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreText : MonoBehaviour
{
    public static int scoreCount = 0;
    private Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    private void OnEnable()
    {
        EnemyController.OnEnemyKilled += IncreamentScoreCount;
    }

    private void OnDisable()
    {

        EnemyController.OnEnemyKilled -= IncreamentScoreCount;
    }

    public void IncreamentScoreCount()
    {
        scoreCount++;
        scoreText.text = $"Score: {scoreCount}";
        //cointText.SetText($"Coins: {coinCount}");
    }
}