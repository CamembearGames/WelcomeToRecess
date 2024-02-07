using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{

    [SerializeField] bool isPlayerGoal;
    [SerializeField] PongGameManager gameManager;


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            if (!isPlayerGoal)
            {
                gameManager.PlayerScored();
            }
            else
            {
                gameManager.ComputerScored();
            }
        }

    }
}
