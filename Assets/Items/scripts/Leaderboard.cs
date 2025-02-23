using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Leaderboard : MonoBehaviour
{

    private CurrentPlayer currentPlayer;  // Reference to the CurrentPlayer script
    
    private Dictionary<string, int> scores = new Dictionary<string, int>();
    public TMP_Text LeaderboardText;
    public TMP_Text highScore;

    // Start is called before the first frame update
    void Start()
    {
        // scores.Add("Player 1", 10);
        // scores.Add("Player 2", 0);
        // scores.Add("Player 3", 50);
        // scores.Add("Player 4", 100);
        // scores.Add("Player 5", 20);
        // scores.Add("Player 6", 50);
        currentPlayer = FindObjectOfType<CurrentPlayer>();
        UpdateLeaderboard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveScore(int score)
    {
        string currentPlayerName = currentPlayer.getPlayerName();
        if (scores.ContainsKey(currentPlayerName))
        {
            if (score > scores[currentPlayerName])
            {
                scores[currentPlayerName] = score;
                highScore.text = "Highscore: " + score;
            }
        }
        else
        {
            scores.Add(currentPlayerName, score);
        }
        UpdateLeaderboard();
    }

    private void UpdateLeaderboard()
    {
        LeaderboardText.text = "Leaderboard\n";

        // Sort the scores by value
        SortedDictionary<int, string[]> scoresByValue = new SortedDictionary<int, string[]>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
        foreach (KeyValuePair<string, int> entry in scores)
        {
            if (scoresByValue.ContainsKey(entry.Value))
            {
                List<string> names = new List<string>(scoresByValue[entry.Value]);
                names.Add(entry.Key);
                scoresByValue[entry.Value] = names.ToArray();
            }
            else
            {
                scoresByValue.Add(entry.Value, new string[] { entry.Key });
            }
        }

        // Display the leaderboard
        int rank = 1;
        foreach (KeyValuePair<int, string[]> entry in scoresByValue)
        {
            foreach (string name in entry.Value)
            {
                LeaderboardText.text += rank + ". " + name + ": " + entry.Key + "\n";
            }
            rank += entry.Value.Length;
        }
    }


}
