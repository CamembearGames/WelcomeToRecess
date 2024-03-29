using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Cinemachine;

public class MainMenuScript : MonoBehaviour
{

    private String[] listOfLanguages = new String[] {"English", "German"};
    private String[] listOfLanguagesText = new String[] {"English", "Deutsch"};
    private int currentLanguage = 1;
    [SerializeField] public TextMeshProUGUI languageField;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject logo;
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private CinemachineVirtualCamera cameraZoomedOut;
    [SerializeField] private CinemachineVirtualCamera cameraStory;

    [SerializeField] private PolaroidScript Polaroids;
    [SerializeField] private AudioSource MainMusic;
    [SerializeField] private GameObject Warningtext;
    [SerializeField] private GameObject Warningbutton;


    private Vector3 startPos = new Vector3(652,512,-50);
    private Vector3 targetPos = new Vector3(404,332,0);

    private Vector3 startPosText = new Vector3(22f,-300,0);
    private Vector3 targetPosText = new Vector3(22f,-96,0);

    private void Start() {
        
        
        Lean.Localization.LeanLocalization.SetCurrentLanguageAll(listOfLanguages[currentLanguage]);
        logo.transform.position = startPos;
        buttonContainer.GetComponent<RectTransform>().localPosition = startPosText;
        
        //languageField.text = listOfLanguagesText[currentLanguage];
    }

    private void FadeIn()
    {
        panel.GetComponent<Image>().DOFade(0.0f,0.5f).OnComplete(DesactivatePanel);
    }

    private void DesactivatePanel()
    {
        panel.gameObject.SetActive(false);
    }

    private void FadeOut()
    {
        panel.gameObject.SetActive(true);
        panel.GetComponent<Image>().DOFade(1.0f,0.5f).OnComplete(LoadLevel);
    }
    
    public void LoadLevel()
    {
        LevelLoader.Load(LevelLoader.Scene.Schoolyard);
    }
    public void StartGame(){
        FadeOut();
    }

    public void LeftArrow(){
        currentLanguage -= 1;
        if (currentLanguage == -1)
        {
            currentLanguage = 1;
        }
        Lean.Localization.LeanLocalization.SetCurrentLanguageAll(listOfLanguages[currentLanguage]);
        //languageField.text = listOfLanguagesText[currentLanguage];
        
    }

    public void RightArrow(){
        currentLanguage += 1;
        if (currentLanguage == 2)
        {
            currentLanguage = 0;
        }
        Lean.Localization.LeanLocalization.SetCurrentLanguageAll(listOfLanguages[currentLanguage]);
        //languageField.text = listOfLanguages[currentLanguage];

    }

    private void LogoIn()
    {
        logo.transform.DOJump(targetPos,10f,2, 1f).SetEase(Ease.OutQuad).OnComplete(ShowText);
        Invoke("PlaySound", 0.5f);
        
    }

    private void PlaySound()
    {
        cameraZoomedOut.Priority = 15;
        logo.GetComponent<AudioSource>().Play();
    }

    private void ShowText()
    {
        buttonContainer.GetComponent<RectTransform>().DOLocalMove(targetPosText, 1f).SetEase(Ease.OutCubic);
        buttonContainer.GetComponent<AudioSource>().Play();
    }

    public void StartPlay()
    {
        StartSory();
        //Invoke("StartSory", 1f);
    }

    private void StartSory()
    {
        MainMusic.Stop();
        Polaroids.StartText1();
    }

    
    public void HideWarning()
    {
        Invoke("FadeIn",0.5f);
        LogoIn();
        MainMusic.Play();
        Warningtext.SetActive(false);
        Warningbutton.SetActive(false);
    }
}
