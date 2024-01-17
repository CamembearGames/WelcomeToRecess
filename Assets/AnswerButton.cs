using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 1
using DG.Tweening;
public class AnswerButton  : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public DialogManager dialogManager;
    public int answerNumber;

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.2f,0.3f);
        //transform.DOLocalMoveY(194f, 0.5f).SetEase(Ease.InOutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1f,0.3f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        dialogManager.GetComponent<DialogManager>().MakeChoice(answerNumber);
    }
}
