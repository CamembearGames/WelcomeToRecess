using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.InputSystem.LowLevel;

public class QuestionButton  : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private Color selectedColor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //dialogManager.GetComponent<DialogManager>().selectedAnswer = answerNumber;
        //transform.DOScale(1.2f,0.3f);
        EventSystem.current.SetSelectedGameObject(this.gameObject);
        //transform.DOLocalMoveY(194f, 0.5f).SetEase(Ease.InOutBack);
    }
 
    public void OnSelect (BaseEventData eventData) 
	{
        transform.DOScale(1.1f,0.3f);
        GetComponent<Image>().DOColor(selectedColor, 0.3f);
		//Debug.Log (this.gameObject.name + " was selected");
	}
    public void OnDeselect (BaseEventData eventData) 
	{
        //if (eventData == UnityEngine.EventSystems.BaseEventData)

            transform.DOScale(1f,0.3f);
            GetComponent<Image>().DOColor(Color.white, 0.3f);


        //.GetType() == UnityEngine.InputSystem.UI.ExtendedAxisEventData);
		//Debug.Log (this.gameObject.name + " was deselected");
	}

}
