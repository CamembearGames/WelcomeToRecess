using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBuilding : MonoBehaviour
{

    public GameObject questionPanel;
    public PlayerController player;

    private void OnTriggerEnter(Collider other) {
        
        if(other.tag == "Player"){
            questionPanel.SetActive(true);
            player.OnDisable();          
        }
    }

    public void YesAnswer(){
        LevelLoader.Load(LevelLoader.Scene.Classroom);
        player.OnEnable(); 
    }

    public void NoAnswer(){
        questionPanel.SetActive(false);
        player.OnEnable();
    }
    
}
