using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class MainBuildingClick : MonoBehaviour
{
    [SerializeField] private GameObject Outline;
    [SerializeField] private GameManager GM;


    void OnMouseDown(){
        if (!DialogManager.GetInstance().dialogIsPlaying && GM.canInteract)
        {
            GM.GoBackToClass();
        }
    }
    void OnMouseEnter(){
        if (!DialogManager.GetInstance().dialogIsPlaying && GM.canInteract)
        {
            Outline.GetComponent<SpriteRenderer>().DOFade(1.0f,0.5f);
        }

    }

    void OnMouseExit(){
        Outline.GetComponent<SpriteRenderer>().DOFade(0.0f,0.5f);
    }
    
}
