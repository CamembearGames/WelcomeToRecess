using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Rendering;
public class AnswerButton  : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, ISelectHandler, IDeselectHandler
{

    public DialogManager dialogManager;
    public int answerNumber;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //dialogManager.GetComponent<DialogManager>().selectedAnswer = answerNumber;
        transform.DOScale(1.2f,0.3f);
        //EventSystem.current.SetSelectedGameObject(this.gameObject);
        //transform.DOLocalMoveY(194f, 0.5f).SetEase(Ease.InOutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1f,0.3f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //dialogManager.GetComponent<DialogManager>().MakeChoice(answerNumber);
    }

    public void OnHighlightEnter()
    {
        //transform.DOScale(1.2f,0.3f);
    }
    public void OnHighlightExit()
    {
        //transform.DOScale(1f,0.3f);
    }
    
    public void OnSelect (BaseEventData eventData) 
	{
        transform.DOScale(1.2f,0.3f);
		//Debug.Log (this.gameObject.name + " was selected");
	}
    public void OnDeselect (BaseEventData eventData) 
	{
        transform.DOScale(1f,0.3f);
		//Debug.Log (this.gameObject.name + " was deselected");
	}

}
