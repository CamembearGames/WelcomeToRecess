using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using Cinemachine;
//using UnityEditor.UI;
using System;
using TMPro;

public class NonPlayableCharacter : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private ScriptableCharacter character;


    public PlayerInputActions playerControls;
    private InputAction talk;
    public GameObject playerPrefab;

    private bool playerInRange;

    public bool showDialogueBubble = false;
    public GameObject chatBubble;
    public Transform chatBubbleTransform;
    public string startText;

    public Animator animator;
    public bool isInteractableChar;

    public CinemachineVirtualCamera vcam;
    public CinemachineVirtualCamera maincam;

    private bool isInDialog = false;

    public Rigidbody rigidBody;
    [SerializeField] private float moveSpeed = 0f;
    //[SerializeField] private float rangeLimit = 0f;
    private float distanceFromTarget;
    private Vector3 directiontoTarget;
    private GameObject targetToMoveTo;
    private Vector2 moveInput; 


    private void Awake() {
        if (isInteractableChar)
        {        
            playerControls = new PlayerInputActions();
            playerInRange = false;
            visualCue.SetActive(true);

            talk = playerControls.Player.Talk;
            talk.Enable();
            //vcam.name = this.name;
        }

    }


    private void Update() {

        if (isInteractableChar)
        {
            if (playerInRange && !DialogManager.GetInstance().dialogIsPlaying)
            {
                if (talk.IsPressed() & !isInDialog){
                    //ShowDialogBox();
                    DialogManager.GetInstance().EnterDialogMode(inkJSON, character, character, false, 1.0f);
                    DialogManager.GetInstance().currentNPC = this;
                }
            }else{
            }

        }

        if (targetToMoveTo != null)

        {
            Vector3 positionToMoveTo = targetToMoveTo.transform.position;
            positionToMoveTo.y = this.transform.position.y;
            if ((positionToMoveTo-this.transform.position).magnitude>0.1)
            {
                moveInput = new Vector2((positionToMoveTo-this.transform.position).x,(positionToMoveTo-this.transform.position).z).normalized;
            }
            else
            {   
                moveInput = Vector2.zero;
                this.transform.position = positionToMoveTo;
                targetToMoveTo.GetComponentInParent<TutorialScript>().Reached(inkJSON, character, playerPrefab.GetComponent<PlayerController>().character);
                targetToMoveTo = null;
            }


            rigidBody.velocity = new Vector3(moveInput.x * moveSpeed, rigidBody.velocity.y, moveInput.y * moveSpeed);
            animator.SetFloat("speed", (new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z)).magnitude);


            /*if ((targetToMoveTo.transform.position-transform.position).magnitude > rangeLimit)
            {
                rigidBody.velocity = new Vector3(directiontoTarget.x * moveSpeed, rigidBody.velocity.y, directiontoTarget.z * moveSpeed);
                distanceFromTarget = (targetToMoveTo.transform.position-transform.position).magnitude;
                animator.SetFloat("speed", rigidBody.velocity.magnitude);
            }
            else
            {
                rigidBody.velocity = Vector3.zero;
                this.transform.position = targetToMoveTo.transform.position;
                targetToMoveTo = null;
                DialogManager.GetInstance().EnterDialogMode(inkJSON, character, character);
                animator.SetFloat("speed", 0f);
            }*/

        }

    }

    private void OnTriggerEnter(Collider other) {
        
        if (isInteractableChar)
        {
            if(other.tag == "Player"){
                playerInRange = true;

                DOTween.Clear();
                //chatBubbleTransform.transform.DOScale(new Vector3 (1f,0f,1f), .5f).SetEase(Ease.InOutSine)
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

    /*public void ShowDialogBox()
    {
        isInDialog = true;
        playerPrefab.GetComponent<PlayerController>().MoveToPoint(playerPos.transform.position);
        maincam.Priority = 10;
        vcam.Priority = 11;
        //playerPrefeb.GetComponent<PlayerController>().ShowDialogBox();

        dialogBoxHolder.SetActive(true);
        dialogBoxHolder.GetComponent<Animation>().Play("BubleAnim");

    }*/

    /*public void HideDialogBox()
    {
        dialogBoxHolder.SetActive(true);
        dialogBoxHolder.GetComponent<Animation>().AddClip(shrinkAnim, "Schrink");
        dialogBoxHolder.GetComponent<Animation>().Play("Schrink");
        isInDialog = false;
        maincam.Priority = 11;
        vcam.Priority = 10;
    }*/

    /*public void UpdateDialogBox(String textToPut)
    {
        dialogBoxHolder.GetComponentInChildren<TextMeshPro>().text = textToPut;
    }*/

    public void MoveToPosition(GameObject target)
    {
        
        Vector3 heading = (target.transform.position-transform.position);
        heading.y = 0f;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance; 

        distanceFromTarget = distance;
        directiontoTarget = direction;
        targetToMoveTo = target;

    }

}
