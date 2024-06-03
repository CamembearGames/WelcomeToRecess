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

            TextAsset chosen_dialog;

            if (Character.PotentialClassDialogs.Count>0)
            {
                if (Character.PriorityClassDialogs.Count>0)
                {
                    int random_index = 0;
                    chosen_dialog = Character.PriorityClassDialogs[random_index];
                    Character.PriorityClassDialogs.RemoveAt(random_index);
                }
                else
                {
                    int random_index = Random.Range (0, Character.PotentialClassDialogs.Count);
                    chosen_dialog = Character.PotentialClassDialogs[random_index];
                    Character.PotentialClassDialogs.RemoveAt(random_index);
                }
            }
            else
            {
                chosen_dialog = Character.DefaultDialog;
            }

            dialogManager.EnterDialogMode(chosen_dialog, Character, false, 0.5f);
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
