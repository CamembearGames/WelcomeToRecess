using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using Cinemachine;

public class MainBuildingClick : MonoBehaviour
{
    [SerializeField] private GameObject Outline;
    [SerializeField] private GameManager GM;
    [SerializeField] private CinemachineVirtualCamera vcam;


    void OnMouseDown(){
        if (!DialogManager.GetInstance().dialogIsPlaying && GM.canInteract)
        {
            GM.canInteract = false;
            GM.mainCamera.GetComponent<cameraMovement>().switchCamera(vcam);
            Invoke("EndCameraMovement", 0.5f);
            
        }
    }

    void EndCameraMovement()
    {
        GM.GoBackToClass();
    }
    void OnMouseEnter(){

            Outline.GetComponent<SpriteRenderer>().DOFade(1.0f,0.5f);
    }

    void OnMouseExit(){
        Outline.GetComponent<SpriteRenderer>().DOFade(0.0f,0.5f);
    }
    
}
