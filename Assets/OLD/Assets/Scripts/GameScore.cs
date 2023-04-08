using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    public int player1Score = 0, player2Score = 0;
    public Text text;
    public Timer timer;
    void Start()
    {
        text = GetComponent<Text>();
    }

    void FixedUpdate()
    {
        if (timer.isGameRunning == true)
            text.text = player1Score.ToString() + "                 " + player2Score.ToString();
    }
}
