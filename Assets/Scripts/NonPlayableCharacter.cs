using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEditor.UI;

public class NonPlayableCharacter : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    [SerializeField] private TextAsset inkJSON;

    public PlayerInputActions playerControls;
    private InputAction talk;

    private bool playerInRange;

    public GameObject chatBubble;

    public Transform chatBubbleTransform;
    public string startText;

    public float speed;

    public Animator animator;

    public bool isInteractableChar;

    public bool showDialogueBubble = false;

    private void Awake() {
        if (isInteractableChar)
        {        
            playerControls = new PlayerInputActions();
            playerInRange = false;
            visualCue.SetActive(true);

            talk = playerControls.Player.Talk;
            talk.Enable();
        }

    }

    private void Start() 
    {
        if (showDialogueBubble)
        {
            chatBubble.SetActive(true);
            //chatBubbleTransform.GetComponent<ChatBubble>().SetupText(startText);
        }
        
    }


    private void Update() {

        animator.SetFloat("speed", speed);


        if (isInteractableChar)
        {
            if (playerInRange && !DialogManager.GetInstance().dialogIsPlaying)
            {
                if (talk.IsPressed()){
                    DialogManager.GetInstance().EnterDialogMode(inkJSON);
                    DialogManager.GetInstance().currentNPC = this;
                }
            }else{
            }

        }

    }

    private void OnTriggerEnter(Collider other) {
        
        if (isInteractableChar)
        {
            if(other.tag == "Player"){
                playerInRange = true;

                DOTween.Clear();
                chatBubbleTransform.transform.DOScale(new Vector3 (1f,0f,1f), .5f).SetEase(Ease.InOutSine);

            }
        }
    }

    private void OnTriggerExit(Collider other) {
        
        if (isInteractableChar)
        {
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
}
