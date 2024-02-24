using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterInteractionPortrait : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    private Vector3 targetPosition = Vector3.zero;
    private bool startDrag = false;
    public bool isSelected = false;
    public bool canBeDragged = false;
    public bool hasBeenPlacedOnce = false;
    [SerializeField] public RectTransform graphImage;
    public YearBookManager ybManager;

    public void MakeAppear()
    {
        Invoke("Grow", 1f);
    }

    private void Grow()
    {
        this.transform.DOScale(1.0f, 0.2f).SetEase(Ease.InOutSine);
    }


    public void OnBeginDrag (PointerEventData eventData)
    {
        if(isSelected)
        {
            this.transform.DOScale(0.5f,0.2f).SetEase(Ease.InOutSine);
        }
        if (canBeDragged) startDrag = true;


    }

    public void OnDrag (PointerEventData eventData)
    {
        if (canBeDragged) targetPosition = (Vector3)eventData.position;
    }


    public void OnEndDrag (PointerEventData eventData)
    {
        if (canBeDragged)
        
        {
            startDrag = false;

            if(IsMouseOverGraph())
            {
                this.GetComponent<Image>().color = Color.green;
                if(hasBeenPlacedOnce == false)
                {
                    ybManager.LoadNextInteraction();
                    hasBeenPlacedOnce = true;
                }
                    
            }
            else
            {
                this.GetComponent<Image>().color = Color.white;
            }  
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
