using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClassroomStudent : MonoBehaviour
{

    [SerializeField] private ScriptableCharacter character;
    public GameObject DialogPortrait;

    private void Start() {
        name = character.nameOfCharacter;
        //GetComponentInChildren<TextMeshPro>().text = "Sit with "+character.nameOfCharacter;
        GetComponent<SpriteRenderer>().sprite = character.portraitOfCharacter;
    }

    public void updatePortrait()
    {
        DialogPortrait.GetComponent<Image>().sprite = character.portraitOfCharacter;
    }

    public void updateDialog()
    {
        //DialogManager.GetInstance().EnterDialogMode(character.classroomTalk);
    }
    

}
