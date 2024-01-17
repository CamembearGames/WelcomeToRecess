using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems; // 1

public class YearInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOLocalMoveY(194f, 0.5f).SetEase(Ease.InOutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOLocalMoveY(271f, 0.5f).SetEase(Ease.InOutBack);;
    }
}
