using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class NonPlayableCharacter : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    [SerializeField] private TextAsset inkJSON;

    public PlayerInputActions playerControls;
    private InputAction talk;

    private bool playerInRange;

    private void Awake() {
        playerControls = new PlayerInputActions();
        playerInRange = false;
        visualCue.SetActive(false);

        talk = playerControls.Player.Talk;
        talk.Enable();
    }

    private void Update() {

        if (playerInRange && !DialogManager.GetInstance().dialogIsPlaying)
        {
            visualCue.SetActive(true);
            if (talk.IsPressed()){
                DialogManager.GetInstance().EnterDialogMode(inkJSON);
            }
        }else{
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            playerInRange = true;
            // Player entered talk area 
            // Should lauch dialog, right now just skips to classroom scene
            //LevelLoader.Load(LevelLoader.Scene.Classroom);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player"){
            playerInRange = false;
            // Player entered talk area 
            // Should lauch dialog, right now just skips to classroom scene
            //LevelLoader.Load(LevelLoader.Scene.Classroom);
        }
    }
}
