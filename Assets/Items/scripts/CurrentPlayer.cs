using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentPlayer : MonoBehaviour
{
    public TMP_Text currentPlayerText;
    public TMP_Text newPlayerText;
    private string playerName = "Player 1";
    private string tempName = "";

    public void Strat()
    {
        Debug.Log("Initializing player name");
        currentPlayerText.text = "Current player: " + playerName;
    }

    public void Update()
    {

    }

    public void addCharacter(string s)
    {
        if (tempName.Length == 0 || tempName.EndsWith(" "))
        {
            s = s.ToUpper();
        }
        tempName += s;
        newPlayerText.text = tempName;
    }

    public void savePlayerName()
    {
        playerName = tempName;
        currentPlayerText.text = "Current player: " + playerName;
        tempName = "";
        newPlayerText.text = tempName;
    }
}