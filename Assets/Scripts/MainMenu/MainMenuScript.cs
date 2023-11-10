using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuScript : MonoBehaviour
{

    private String[] listOfLanguages = new String[] {"English", "Deutsch"};
    private int currentLanguage = 0;
    [SerializeField] public TextMeshProUGUI languageField;


    public void StartGame(){
        LevelLoader.Load(LevelLoader.Scene.IntroScene);
    }

    public void LeftArrow(){
        currentLanguage -= 1;
        if (currentLanguage == -1)
        {
            currentLanguage = 1;
        }
        languageField.text = listOfLanguages[currentLanguage];
        
    }

    public void RightArrow(){
        currentLanguage += 1;
        if (currentLanguage == 2)
        {
            currentLanguage = 0;
        }
        languageField.text = listOfLanguages[currentLanguage];

    }
}
