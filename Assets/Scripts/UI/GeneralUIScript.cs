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

    [SerializeField] private TutorialScriptNoPlayer tutorial;

    [SerializeField] private RectTransform bellUI;

    [SerializeField] private Vector3 bellUI_start_pos;
    [SerializeField] private Vector3 bellUI_start_scale;

    [SerializeField] private TextMeshProUGUI activityNumber;
    [SerializeField] private Animation activityAnimator;


    [SerializeField] private RectTransform clockUI;
    [SerializeField] private RectTransform handsUI;
    [SerializeField] private Image handsUIImage;

    [SerializeField] private float clockTimeStart;
    [SerializeField] private float bellTimeStart;
        [SerializeField] private int bellShake;

    [SerializeField] private float bellShaketimer;

    // Start is called before the first frame update
    void Start()
    {

        bellUI.transform.localPosition = bellUI_start_pos;
        bellUI.transform.localScale = bellUI_start_scale;

        if (!GameData.Instance.hasDoneTutorial) flavorText.text = "Der Anfang";
        else flavorText.text = "";

        switch (GameData.Instance.currentSegment)
        {
            case GameData.Segments.Recess:
                recessText.text = "Große Pause";
                yearNumber.text = (GameData.Instance.currentYear+1).ToString();
                recessNumber.text = (GameData.Instance.currentRecess+1).ToString();
                break;
            case GameData.Segments.Classroom:
                recessText.text = "Unterricht";
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
            if (tutorial) tutorial.EndCamera();
        }
        
        
    }

    public void UpdateActivity()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.SetEase(Ease.InOutSine);
        //mySequence.Append(clockUI.DOScale(new Vector3(1,1,1), 0.5f));
        mySequence.Append(clockUI.DOLocalMove(new Vector3(0,0,0), 0.5f).SetEase(Ease.InOutCubic));
        mySequence.Append(activityNumber.transform.DOScale(new Vector3(0,0,0), 0.5f).SetEase(Ease.InOutQuad));
        mySequence.AppendCallback(UpdateActivityText);
        
        mySequence.AppendInterval(2f);
        mySequence.AppendInterval(1.0f);
        //mySequence.Append(clockUI.DOScale(new Vector3(0,0,0), 0.5f));
        mySequence.Append(clockUI.DOLocalMove(new Vector3(0,305,0), 0.5f).SetEase(Ease.InOutCubic));

        mySequence.AppendCallback(UpdateBell);

        mySequence.Insert(0.0f,clockUI.DOScale(2.0f,0.5f));

        mySequence.Insert(0.8f,handsUIImage.DOFade(1.0f,0.1f));
        mySequence.Insert(0.8f,handsUI.DORotate(new Vector3(0,0,-360), 2f, RotateMode.WorldAxisAdd));
        mySequence.Insert(2f,handsUIImage.DOFade(0.0f,0.5f));
        mySequence.Insert(2f,activityNumber.transform.DOScale(new Vector3(1,1,1), 0.5f).SetEase(Ease.InOutQuad));
        mySequence.InsertCallback(clockTimeStart,PlaySound);
        //mySequence.Insert(1f, bellUI.DOScale(new Vector3(1f,1f,1f), 0.5f).SetEase(Ease.InOutCubic));
        mySequence.Insert(0.5f, clockUI.DOShakePosition(1f, 5));

        mySequence.Insert(4f,clockUI.DOScale(1.0f,0.5f));

        
        //mySequence.Insert(1.5f, bellUI.DOScale(bellUI_start_scale, 0.5f).SetEase(Ease.InOutCubic));
    }

            


    public void UpdateBell()
    {
        if (GameData.Instance.activitiesDone == GameData.Instance.activitiesMax && GameData.Instance.currentSegment == GameData.Segments.Recess)
        //if (true)
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.SetEase(Ease.InOutSine);
            mySequence.Append(bellUI.DOLocalMove(new Vector3(0,0,0), 0.5f));
            mySequence.Append(bellUI.DOPunchRotation(new Vector3(0f,0f,20f), bellShaketimer, bellShake));
            mySequence.Append(bellUI.DOLocalMove(bellUI_start_pos, 0.5f));
            mySequence.InsertCallback(bellTimeStart,PlayBell);
            mySequence.Insert(0f, bellUI.DOScale(new Vector3(2f,2f,2f), 0.6f).SetEase(Ease.InOutCubic));
            mySequence.Insert(3.4f, bellUI.DOScale(bellUI_start_scale, 0.6f).SetEase(Ease.InOutCubic));
        }
    }

    public void PlaySound()
    {
        clockUI.gameObject.GetComponent<AudioSource>().Play();
    } 

    public void PlayBell()
    {
        bellUI.gameObject.GetComponent<AudioSource>().Play();
    } 

    public void UpdateActivityText() 
    {
        activityNumber.text = (GameData.Instance.activitiesMax - GameData.Instance.activitiesDone).ToString();
    }

    public void PlayClick()
    {
        GetComponent<AudioSource>().Play();
    }

}
