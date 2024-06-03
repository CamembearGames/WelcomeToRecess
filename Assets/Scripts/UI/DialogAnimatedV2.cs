using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class DialogAnimatedV2 : MonoBehaviour
{


    private TextMeshProUGUI textBox;
    [SerializeField] private DialogManager dialogManager;
    private string textToWrite;
    private float timePerChar;
    private float timer;
    private int charIndex;
    private bool invisibleCharacters;
    private bool isAddingRichTextTag = false;
    // Start is called before the first frame update

    void Awake(){
        if(dialogManager == null) dialogManager = DialogManager.instance;
    }

    void Start()
    {
        charIndex = 0;
    }

    public void AddWriter(TextMeshProUGUI textBox, String textToWrite, float timePerChar, bool invisibleCharacters)
    {
        this.textBox = textBox;
        this.textToWrite = textToWrite;
        this.timePerChar = timePerChar;
        this.invisibleCharacters = invisibleCharacters;
        charIndex = 0;
        timer += timePerChar;
    }

    // Update is called once per frame
    void Update()
    {
        if (textBox != null)
        {
            timer -= Time.deltaTime;
            while (timer < 0)
            {

                timer += timePerChar;
                charIndex++;

                if (textToWrite[charIndex-1] == '<' || isAddingRichTextTag)
                {
                    isAddingRichTextTag = true;
                    timer = 0;
                    if (textToWrite[charIndex-1] == '>')
                    {
                        isAddingRichTextTag = false;
                    }
                    break;
                }


                string text = textToWrite.Substring(0, charIndex);
                if (invisibleCharacters)
                {
                    text += "<color=#00000000>" + textToWrite.Substring(charIndex) + "</color>";
                }

                
                textBox.text = text;
                if (charIndex >= textToWrite.Length){
                    textBox = null;
                    dialogManager.TextFinishedLoading();
                    return;
                }
            }
        }
    }

    public void ShowAllText()
    {
        textBox.text = textToWrite;
        textBox = null;
        dialogManager.TextFinishedLoading();
    }

    public void ShowDialogBox()
    {
        transform.DOLocalMoveY(-20f, 0.5f).SetEase(Ease.InOutCubic);
        GetComponent<AudioSource>().Play();
    }   
    public void HideDialogBox()
    {
        transform.DOLocalMoveY(-720f, 0.5f).SetEase(Ease.InOutCubic);
        //GetComponent<AudioSource>().Play();
    }
}
