using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 1
using DG.Tweening;


public class TabMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.2f,0.3f);
        //transform.DOLocalMoveY(194f, 0.5f).SetEase(Ease.InOutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1f,0.3f);
    }
}
