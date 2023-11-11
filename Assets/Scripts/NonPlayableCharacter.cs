using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class NonPlayableCharacter : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    [SerializeField] private TextAsset inkJSON;

    public PlayerInputActions playerControls;
    private InputAction talk;

    private bool playerInRange;

    public Transform chatBubbleTransform;
    public string startText;
    

    private void Awake() {
        playerControls = new PlayerInputActions();
        playerInRange = false;
        visualCue.SetActive(false);

        talk = playerControls.Player.Talk;
        talk.Enable();
    }

    private void Start() 
    {
        chatBubbleTransform.GetComponent<ChatBubble>().SetupText(startText);
    }
    private void Update() {

        if (playerInRange && !DialogManager.GetInstance().dialogIsPlaying)
        {
            visualCue.SetActive(true);
            if (talk.IsPressed()){
                DialogManager.GetInstance().EnterDialogMode(inkJSON, this);
            }
        }else{
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            playerInRange = true;

            DOTween.Clear();
            chatBubbleTransform.transform.DOScale(new Vector3 (1f,0f,1f), .5f).SetEase(Ease.InOutSine);

        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player"){
            playerInRange = false;
            DOTween.Clear();
            chatBubbleTransform.transform.DOScale(new Vector3 (1f,1f,1f), .5f).SetEase(Ease.InOutSine);
            // Player entered talk area 
            // Should lauch dialog, right now just skips to classroom scene
            //LevelLoader.Load(LevelLoader.Scene.Classroom);
        }
    }
}
