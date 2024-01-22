using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class DialogAnimatedV2 : MonoBehaviour
{


    private TextMeshProUGUI textBox;
    [SerializeField] private DialogManager dialogManager;
    private string textToWrite;
    private float timePerChar;
    private float timer;
    private int charIndex;
    private bool invisibleCharacters;
    // Start is called before the first frame update
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

    public void ShowDialogBox()
    {
        transform.DOLocalMoveY(0f, 0.75f).SetEase(Ease.InOutCubic);
    }
    public void HideDialogBox()
    {
        transform.DOLocalMoveY(-278f, 0.75f).SetEase(Ease.InOutCubic);
    }
}
