using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
//using Ink.UnityIntegration;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GeneralUIScript : MonoBehaviour
{
    public GameObject fadeInPanel;
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private PlayerController player;
    [SerializeField] private TextAsset inkTutorial;
    [SerializeField] private float fadeTime= 1f;

    // Start is called before the first frame update
    void Start()
    {
        fadeInPanel.SetActive(true);
        Invoke("FadeinText", 0.5f);

    }

    void FadeinText()
    {
        //fadeInPanel.GetComponent<Image>().DOFade(0f,1f);
        fadeInPanel.GetComponentInChildren<TextMeshProUGUI>().DOFade(1f,1f).OnComplete(ShowTextTimer); 
    } 

    void ShowTextTimer()
    {
        Invoke("FadeOut", fadeTime);
    }

    void FadeOut()
    {
        fadeInPanel.GetComponent<Image>().DOFade(0f,1f);
        fadeInPanel.GetComponentInChildren<TextMeshProUGUI>().DOFade(0f,1f).OnComplete(FadeOutFinished);
    }

    void FadeOutFinished()
    {
        fadeInPanel.SetActive(false);
        if (inkTutorial) dialogManager.EnterDialogMode(inkTutorial, null, player.character, false);
    }
}
