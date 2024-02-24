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
using UnityEngine.UI;
using Unity.VisualScripting;

public class NonPlayableCharacterClick : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] public ScriptableCharacter character;


    public PlayerInputActions playerControls;
    private InputAction talk;
    public GameObject playerPrefab;

    public bool showDialogueBubble = false;
    public GameObject chatBubble;
    public Transform chatBubbleTransform;
    public string startText;

    public Animator animator;
    public bool isInteractableChar;

    public CinemachineVirtualCamera vcam;
    public CinemachineVirtualCamera maincam;

    public Rigidbody rigidBody;
    [SerializeField] private float moveSpeed = 0f;
    //[SerializeField] private float rangeLimit = 0f;
    private float distanceFromTarget;
    private Vector3 directiontoTarget;
    private GameObject targetToMoveTo;
    private Vector2 moveInput; 

    [SerializeField] private GameObject Outline;

    private void Awake() {
        if (isInteractableChar)
        {        
            playerControls = new PlayerInputActions();
            visualCue.SetActive(true);
        }

    }

    void OnMouseDown(){
        DialogManager.GetInstance().EnterDialogMode(inkJSON, character, false, 1.0f);
    }
    void OnMouseEnter(){
        Outline.GetComponent<SpriteRenderer>().DOKill();
        Outline.GetComponent<SpriteRenderer>().DOFade(1.0f,0.5f);
    }

    void OnMouseExit(){
        Outline.GetComponent<SpriteRenderer>().DOKill();
        Outline.GetComponent<SpriteRenderer>().DOFade(0.0f,0.5f);
    }


    private void Update() {

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

        }

    }

    private void OnTriggerEnter(Collider other) {
        
        if (isInteractableChar)
        {
            if(other.tag == "Player"){

                DOTween.Clear();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        
        if (isInteractableChar)
        {
            if(other.tag == "Player"){
                DOTween.Clear();
                chatBubbleTransform.transform.DOScale(new Vector3 (1f,1f,1f), .5f).SetEase(Ease.InOutSine);
                // Player entered talk area 
                // Should lauch dialog, right now just skips to classroom scene
                //LevelLoader.Load(LevelLoader.Scene.Classroom);
            }
        }
    }


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
