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

    public void SetPlayerName()
    {
        Debug.Log("Setting player name");
        playerName = "Igor";
        currentPlayerText.text = "Current player: " + playerName;
    }

    public void addCharacter(string s)
    {
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