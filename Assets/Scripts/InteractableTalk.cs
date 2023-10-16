using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTalk : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            // Player entered talk area 
            // Should lauch dialog, right now just skips to classroom scene

            LevelLoader.Load(LevelLoader.Scene.Classroom);

        }
    }
}
