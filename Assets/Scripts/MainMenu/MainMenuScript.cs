using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuScript : MonoBehaviour
{

    private String[] listOfLanguages = new String[] {"English", "German"};
    private String[] listOfLanguagesText = new String[] {"English", "Deutsch"};
    private int currentLanguage = 0;
    [SerializeField] public TextMeshProUGUI languageField;


    public void StartGame(){
        LevelLoader.Load(LevelLoader.Scene.Recess);
    }

    public void LeftArrow(){
        currentLanguage -= 1;
        if (currentLanguage == -1)
        {
            currentLanguage = 1;
        }
        languageField.text = listOfLanguagesText[currentLanguage];
        Lean.Localization.LeanLocalization.SetCurrentLanguageAll(listOfLanguages[currentLanguage]);
        
    }

    public void RightArrow(){
        currentLanguage += 1;
        if (currentLanguage == 2)
        {
            currentLanguage = 0;
        }
        languageField.text = listOfLanguages[currentLanguage];
        Lean.Localization.LeanLocalization.SetCurrentLanguageAll(listOfLanguages[currentLanguage]);

    }
}
