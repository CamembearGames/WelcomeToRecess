using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
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

    [SerializeField] private TextMeshProUGUI yearNumber;
    [SerializeField] private TextMeshProUGUI recessNumber;
    [SerializeField] private TextMeshProUGUI recessText;
    [SerializeField] private TextMeshProUGUI flavorText;




    // Start is called before the first frame update
    void Start()
    {
        if (player) recessText.text = "Recess";
        else recessText.text = "class";
        yearNumber.text = (GameData.Instance.currentYear+1).ToString();

        if (player) recessNumber.text = (GameData.Instance.currentRecess+1).ToString();
        else 
        {
            flavorText.text = "";
            recessNumber.text = (GameData.Instance.currentClass+1).ToString();
        }
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
        foreach (TextMeshProUGUI elem in fadeInPanel.GetComponentsInChildren<TextMeshProUGUI>())
        {
            elem.DOFade(0f,1f);
        }
        fadeInPanel.GetComponent<Image>().DOFade(0f,1f).OnComplete(FadeOutFinished);;
    }

    void FadeOutFinished()
    {
        fadeInPanel.SetActive(false);
        if (GameData.Instance.currentYear == 0 & GameData.Instance.currentRecess == 0)
        {
            if (inkTutorial) dialogManager.EnterDialogMode(inkTutorial, null, player.character, false, 1.0f);
            flavorText.text = "";
        }
        else
        {
            if (player!= null) player.OnEnable();
        }
        
        
    }
}
