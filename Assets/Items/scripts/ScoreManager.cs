using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    private Leaderboard leaderboard;  // Reference to the Leaderboard script

    public TMP_Text scoreText;
    public TMP_Text scoreTextDeath;
    private int score = 0;

    void Start()
    {
        leaderboard = FindObjectOfType<Leaderboard>();
        if (scoreText == null) // Check if scoreText is missing
        {
            Debug.LogError("ScoreText is not assigned in the Inspector!");
        }
        UpdateScoreUI(); // Set initial score display
    }

    public void AddScore(int points)
    {
        Debug.Log("Score add: " + score); // Debug log to check updates
        score += points;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            Debug.Log("update board"); // Debug log to check updates
            scoreText.text = "Score: " + score;
            scoreTextDeath.text = "Score: " + score;
            SaveScore();
        }
    }

    public void SaveScore()
    {
        leaderboard.SaveScore(score);
    }

}