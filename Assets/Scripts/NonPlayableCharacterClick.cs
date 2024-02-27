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

    [SerializeField] private TextAsset inkJSON;
    [SerializeField] public ScriptableCharacter character;
    [SerializeField] private GameObject Outline;

    [SerializeField] private GameManager GM;

    public bool isInteractableChar;

    public CinemachineVirtualCamera vcam;
    public CinemachineVirtualCamera maincam;


    void OnMouseDown(){
        if (isInteractableChar && !DialogManager.GetInstance().dialogIsPlaying && GM.canInteract)
        {
            DialogManager.GetInstance().EnterDialogMode(inkJSON, character, false, 1.0f);
            DialogManager.GetInstance().currentNPC = this;
        }
    }
    void OnMouseEnter(){
        if (isInteractableChar && !DialogManager.GetInstance().dialogIsPlaying && GM.canInteract)
        {
            Outline.GetComponent<SpriteRenderer>().DOFade(1.0f,0.5f);
        }

    }

    void OnMouseExit(){
        if (isInteractableChar)
        {
            Outline.GetComponent<SpriteRenderer>().DOFade(0.0f,0.5f);
        }
    }


    private void Update() {


    }


}
