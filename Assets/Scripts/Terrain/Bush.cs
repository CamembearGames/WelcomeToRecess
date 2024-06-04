using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using Cinemachine;

public class Bush : MonoBehaviour
{
    [SerializeField] private GameObject Outline;
    [SerializeField] private GameManager GM;
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private TextAsset inkNoMorTime;


    void OnMouseDown(){
        if (!DialogManager.GetInstance().dialogIsPlaying && GM.canInteract)
        {
            if (GameData.Instance.activitiesDone < GameData.Instance.activitiesMax)
            {
                GM.WaterBush(this.gameObject);
                GM.mainCamera.GetComponent<cameraMovement>().switchCamera(GetComponentInChildren<CinemachineVirtualCamera>());
            }
            else
            {
                dialogManager.EnterDialogMode(inkNoMorTime, null, false, 1.0f);
            }

            
        }
    }
    void OnMouseEnter(){
        Outline.GetComponent<SpriteRenderer>().DOFade(1.0f,0.5f);
    }

    void OnMouseExit(){
        Outline.GetComponent<SpriteRenderer>().DOFade(0.0f,0.5f);
    }
    
    public void Shake(){
        GetComponent<Animation>().Play();
        GetComponent<AudioSource>().Play();
    }
}
