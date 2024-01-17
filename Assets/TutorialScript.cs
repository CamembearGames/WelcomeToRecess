using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{

    [SerializeField] private GameObject tutorialCharacter;
    private GameObject player;

    private void OnTriggerEnter(Collider other) {
        
        if(other.tag == "Player"){
            other.GetComponent<PlayerController>().StopMoving();
            player = other.gameObject;
            Invoke("StartTutorial", 0.5f);
        }
    }

    private void StartTutorial()
    {
        tutorialCharacter.GetComponent<NonPlayableCharacter>().MoveToPosition(player);
    }
}
