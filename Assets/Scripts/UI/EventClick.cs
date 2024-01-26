using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler,IPointerEnterHandler, IPointerExitHandler
{

    private Material baseMaterial;
    public Material hoverMaterial;

    public DialogManager dialogManager;
    public int answerOrder;

    private void Awake() 
    {
        baseMaterial = GetComponent<MeshRenderer>().material;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        dialogManager.MakeChoice(answerOrder);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<MeshRenderer>().material = hoverMaterial;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<MeshRenderer>().material = baseMaterial;
    }
}
