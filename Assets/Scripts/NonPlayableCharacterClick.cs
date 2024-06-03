using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using Cinemachine;
//using UnityEditor.UI;
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

    private void Start() {
        //if (character) character.ResetDialogs();
    }

    void OnMouseDown(){
        if (isInteractableChar && !DialogManager.GetInstance().dialogIsPlaying && GM.canInteract)
        {
            TextAsset chosen_dialog;
            if (character.PotentialDialogs.Count>0)
            {
                if (character.PriorityDialogs.Count>0)
                {
                    int random_index = 0;
                    chosen_dialog = character.PriorityDialogs[random_index];
                    character.PriorityDialogs.RemoveAt(random_index);
                }
                else
                {
                    int random_index = Random.Range (0, character.PotentialDialogs.Count);
                    chosen_dialog = character.PotentialDialogs[random_index];
                    character.PotentialDialogs.RemoveAt(random_index);
                }
            }
            else
            {
                chosen_dialog = character.DefaultDialog;
            }

            DialogManager.GetInstance().EnterDialogMode(chosen_dialog, character, false, 0.2f);
            DialogManager.GetInstance().currentNPC = this;

            GM.mainCamera.GetComponent<cameraMovement>().switchCamera(vcam);

            //LockedRotation(this.transform);

            //transform.DOLookAt(this.transform.position, 1.0f);
        }
    }
    void OnMouseEnter(){
        if (isInteractableChar)
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



}
