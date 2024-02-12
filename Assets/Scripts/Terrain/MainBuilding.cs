using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class MainBuilding : MonoBehaviour
{
    public PlayerController player;

    [SerializeField] private GameObject exaclamation;
    [SerializeField] private GameObject keyInput;
    [SerializeField] private GameManager gameManager;


    public PlayerInputActions playerControls;
    private InputAction talk;
    private bool isInRange = false;

    private void Awake() {
        playerControls = new PlayerInputActions();
        talk = playerControls.Player.Talk;
        talk.Enable();
    }


    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            //exaclamation.transform.localScale = new Vector3(5f,5f,5f);
            exaclamation.transform.DOScale(0f,0.2f).OnComplete(ExclamationTweenFinished); 
            isInRange = true;  
        }
    }

    private void ExclamationTweenFinished()
    {
        //keyInput.transform.localScale = new Vector3(0f,0f,0f);
        keyInput.transform.DOScale(2f,0.2f);  
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player"){
            //keyInput.transform.localScale = new Vector3(2f,2f,2f);
            keyInput.transform.DOScale(0f,0.2f).OnComplete(KeyTweenFinished);   
            isInRange = false;
        }
    }

    private void KeyTweenFinished()
    {
        //exaclamation.transform.localScale = new Vector3(0f,0f,0f);
        exaclamation.transform.DOScale(5f,0.2f);   
    }

    private void Update() {
        if (talk.IsPressed() & isInRange)
        {
            player.OnDisable();
            isInRange = false;
            gameManager.GoBackToClass();
            //LevelLoader.Load(LevelLoader.Scene.EndYearBook);
        }
    }
    
}
