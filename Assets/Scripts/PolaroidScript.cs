using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using DG.Tweening;
using Cinemachine;
using System.Linq;
using UnityEngine.UI;
public class PolaroidScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI []Text;
    [SerializeField] private Image []Pola;


    [SerializeField] private String []Text_txt;

    [SerializeField] private CinemachineVirtualCamera[] storyCam;


    [SerializeField] private float timing_char;
    [SerializeField] private AudioSource writingSound;


    private TextMeshProUGUI textBox;
    private string textToWrite;
    private float timePerChar;
    private float timer;
    private int charIndex;
    private bool invisibleCharacters;

    private int storyIteration = 0;

    private void Start() {
    }

    public void StartText1()
    {
        
        TextFinishedLoading();
        
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
                    Invoke("TextFinishedLoading",1f);
                    return;
                }
            }
        }
    }

    void TextFinishedLoading()
    {
        if (storyIteration < Text.Count())
        {
            Pola[storyIteration].GetComponentInParent<AudioSource>().Play();
            if (storyIteration>0)storyCam[storyIteration].Priority = storyCam[storyIteration-1].Priority+1;
            else storyCam[storyIteration].Priority = 20;
            Invoke("LoadNextText",1.5f);
        }
    }

    void LoadNextText()
    {
        Pola[storyIteration].transform.DOScale(0.75f,0.6f).SetEase(Ease.InOutSine);
        AddWriter(Text[storyIteration], Text_txt[storyIteration], timing_char, true);
        storyIteration ++;
    }
}
