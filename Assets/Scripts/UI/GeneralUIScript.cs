using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using DG.Tweening;
//using Ink.UnityIntegration;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GeneralUIScript : MonoBehaviour
{
    public GameObject fadeInPanel;
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private PlayerController player;
    [SerializeField] private TextAsset inkTutorial;
    [SerializeField] private float fadeTime= 1f;

    [SerializeField] private TextMeshProUGUI yearNumber;
    [SerializeField] private TextMeshProUGUI recessNumber;
    [SerializeField] private TextMeshProUGUI recessText;
    [SerializeField] private TextMeshProUGUI flavorText;

    // Start is called before the first frame update
    void Start()
    {

        if (!GameData.Instance.hasDoneTutorial) flavorText.text = "The beginning";
        else flavorText.text = "";

        switch (GameData.Instance.currentSegment)
        {
            case GameData.Segments.Recess:
                recessText.text = "Recess";
                yearNumber.text = (GameData.Instance.currentYear+1).ToString();
                recessNumber.text = (GameData.Instance.currentRecess+1).ToString();
                break;
            case GameData.Segments.Classroom:
                recessText.text = "Class";
                yearNumber.text = (GameData.Instance.currentYear+1).ToString();
                recessNumber.text = (GameData.Instance.currentClass+1).ToString();
                flavorText.text = "";
                break;
            case GameData.Segments.PongScene:
                recessText.text = "";
                yearNumber.text = (GameData.Instance.currentYear+1).ToString();
                recessNumber.text = "";
                flavorText.text = "Pong Game !";
                break;
        }
        
        fadeInPanel.SetActive(true);
        Invoke("FadeinText", 0.2f);

    }

    void FadeinText()
    {
        //fadeInPanel.GetComponent<Image>().DOFade(0f,1f);
        foreach (TextMeshProUGUI elem in fadeInPanel.GetComponentsInChildren<TextMeshProUGUI>())
        {
            elem.DOFade(1f,1f); 
        }
        
        Invoke("ShowTextTimer", 1f);
        
    } 

    void ShowTextTimer()
    {
        Invoke("FadeOut", fadeTime);
    }

    void FadeOut()
    {
        foreach (TextMeshProUGUI elem in fadeInPanel.GetComponentsInChildren<TextMeshProUGUI>())
        {
            elem.DOFade(0f,1f);
        }
        fadeInPanel.GetComponent<Image>().DOFade(0f,1f).OnComplete(FadeOutFinished);;
    }

    void FadeOutFinished()
    {
        fadeInPanel.SetActive(false);
        if (!GameData.Instance.hasDoneTutorial)
        {
            if (inkTutorial) dialogManager.EnterDialogMode(inkTutorial, null, false, 1.0f);
            GameData.Instance.hasDoneTutorial = true;
            flavorText.text = "";
        }
        else
        {
            if (player!= null) player.OnEnable();
            flavorText.text = "";
        }
        
        
    }
}
