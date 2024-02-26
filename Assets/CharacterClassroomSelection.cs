using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class CharacterClassroomSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private ScriptableCharacter Character;
    [SerializeField] private Image CharacterSprite;
    [SerializeField] private TextMeshProUGUI nameTag;
    [SerializeField] private Image nameSprite;
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private TextMeshProUGUI questionTag;
    [SerializeField] private Image questionSprite;


    private void Start() 
    {
        CharacterSprite.sprite = Character.portraitOfCharacter;
        GetComponent<Image>().sprite = Character.outlineOfCharacter;
        nameTag.text = Character.nameOfCharacter;
    }

    public void OnPointerDown(PointerEventData eventData){
        if (!dialogManager.dialogIsPlaying)
        {
            questionTag.DOFade(0.0f,0.3f);
            questionSprite.DOFade(0.0f,0.3f);
            dialogManager.EnterDialogMode(Character.classroomTalk, Character, false, 0.5f);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!dialogManager.dialogIsPlaying)
        {
            nameTag.DOFade(1.0f,0.5f);
            nameSprite.DOFade(1.0f,0.5f);
            GetComponent<Image>().DOFade(1.0f,0.5f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        nameTag.DOFade(0.0f,0.5f);
        nameSprite.DOFade(0.0f,0.5f);
        GetComponent<Image>().DOFade(0.0f,0.5f);
    }

}
