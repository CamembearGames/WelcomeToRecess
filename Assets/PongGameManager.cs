using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PongGameManager : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject playerPaddle;
    [SerializeField] private GameObject playerGoal;
    [SerializeField] private GameObject computerPaddle;
    [SerializeField] private GameObject computerGoal;
    [SerializeField] private GameObject playerScoreText;
    [SerializeField] private GameObject computerScoreText;

    private int playerScore = 0;
    private int computerScore = 0;

    public void PlayerScored()
    {
        playerScore++;
        playerScoreText.GetComponent<TextMeshProUGUI>().text = playerScore.ToString();
        ResetPosition();
    }


    public void ComputerScored()
    {
        computerScore++;
        computerScoreText.GetComponent<TextMeshProUGUI>().text = computerScore.ToString();
        ResetPosition();
    }

    public void ResetPosition()
    {
        ball.GetComponent<BallScript>().Reset();
        playerPaddle.GetComponent<PlayerPaddle>().Reset();

    }
}
