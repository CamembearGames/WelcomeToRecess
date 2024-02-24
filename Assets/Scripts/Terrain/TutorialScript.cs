using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{

    [SerializeField] private GameObject tutorialCharacter;
    [SerializeField] private GameObject playerPosition;
    [SerializeField] private GameObject npcPosition;

    private GameObject player;

    private void OnTriggerEnter(Collider other) {
        
        if(other.tag == "Player"){
            player = other.gameObject;
            player.GetComponent<PlayerController>().OnDisable();
            Invoke("StartTutorial", 0.5f);
        }
    }

    private void StartTutorial()
    {
        tutorialCharacter.GetComponent<NonPlayableCharacter>().MoveToPosition(npcPosition);
        player.GetComponent<PlayerController>().MoveToPoint(playerPosition.transform.position);
    }

    public void Reached(TextAsset inkFile, ScriptableCharacter char1, ScriptableCharacter char2)
    {
        DialogManager.GetInstance().EnterDialogMode(inkFile, char1, true, 1.0f);
    }
}
