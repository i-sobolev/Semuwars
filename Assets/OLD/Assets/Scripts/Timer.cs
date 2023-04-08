using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float seconds, maxGameTime;
    public bool isGameRunning = true;

    public Text Text;
    public GameScore gameScore;
    public GameObject buttons;

    private void Awake()
    {
        seconds = SettingsScript.maxGameTime;
    }
    void Start()
    {
        maxGameTime = SettingsScript.maxGameTime;
        Text = GetComponent<Text>();
    }
    void FixedUpdate()
    {
        if (isGameRunning == true)
        {
            seconds = seconds - Time.deltaTime;

            Text.text = seconds.ToString("F");
        }
        
        if (seconds <= 0 && gameScore.player1Score != gameScore.player2Score)
        {
            isGameRunning = false;

            if (gameScore.player1Score > gameScore.player2Score)
                Text.text = "PLAYER 1 WIN";
            
            if (gameScore.player1Score < gameScore.player2Score)
                Text.text = "PLAYER 2 WIN";
        }

        if (seconds <= 0 && gameScore.player1Score == gameScore.player2Score)
        {
            if (gameScore.player1Score == gameScore.player2Score)
                Text.text = "OVERTIME";

            if (gameScore.player1Score > gameScore.player2Score)
            {
                Text.text = "PLAYER 1 WIN";
                isGameRunning = false;
            }

            if (gameScore.player1Score < gameScore.player2Score)
            {
                Text.text = "PLAYER 2 WIN";
                isGameRunning = false;
            }
        }

        if (isGameRunning == false)
            buttons.SetActive(true);
    }
}
