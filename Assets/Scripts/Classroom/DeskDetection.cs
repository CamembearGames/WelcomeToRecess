using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskDetection : MonoBehaviour
{
    public GameObject questionPanel;
    public PlayerController player;

    private void OnTriggerEnter(Collider other) {
        
        if(other.tag == "Player"){
            questionPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        
        if(other.tag == "Player"){
            questionPanel.SetActive(false);
        }
    }
}
