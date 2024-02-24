using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class CharacterInteractionYB : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{

    private Vector3 targetPosition = Vector3.zero;
    private bool startDrag = false;

    [SerializeField] private RectTransform graphImage;

    #region IDragHandler implementation

    public void OnDrag (PointerEventData eventData)
    {
        targetPosition = (Vector3)eventData.position;
    }

    #endregion
    public void OnBeginDrag (PointerEventData eventData)
    {
        this.transform.DOScale(0.5f,0.2f).SetEase(Ease.InOutSine);
        startDrag = true;
    }

    public void OnEndDrag (PointerEventData eventData)
    {
        startDrag = false;

        if(IsMouseOverGraph())
        {
            this.GetComponent<Image>().color = Color.green;
        }
        else
        {
            this.GetComponent<Image>().color = Color.white;
        }

    }

    private bool IsMouseOverGraph()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);
        for (int i= 0; i< raycastResultList.Count;i++)
        {
            if (raycastResultList[i].gameObject.CompareTag("Graph"))
            {
                return true;
            }
        }
        return false;
    }

    private void Update() 
    {
        if (startDrag) this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime * 10.0f) ;
    }


    
}
