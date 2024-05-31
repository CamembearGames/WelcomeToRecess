using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class QuestionPanel : MonoBehaviour
{
    [SerializeField] private Button []buttons;
    public GameManager gameManagerReference;

     
    public void ShowDialogBox()
    {
        GetComponent<AudioSource>().Play();
        gameManagerReference.canRotate = false;
        ActivateButtons();
        transform.DOLocalMoveY(110f, 0.75f).SetEase(Ease.InOutCubic);
    }   
    public void HideDialogBox()
    {
        GetComponent<AudioSource>().Play();
        EventSystem.current.SetSelectedGameObject(null);
        DeactivateButtons();
        transform.DOLocalMoveY(-720f, 0.75f).SetEase(Ease.InOutCubic);
    }

    public void DeactivateButtons(){
        foreach (Button item in buttons)
        {
            item.interactable = false;
        }
    }

    public void ActivateButtons(){
        foreach (Button item in buttons)
        {
            item.interactable = true;
        }
    }
}
