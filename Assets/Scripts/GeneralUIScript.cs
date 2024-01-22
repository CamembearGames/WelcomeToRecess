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
    public DialogAnimatedV2 dialogPanel;
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private PlayerController player;
    [SerializeField] private TextAsset inkTutorial;


    // Start is called before the first frame update
    void Start()
    {
        fadeInPanel.SetActive(true);
        Invoke("FadeOut", 1);

    }

    void FadeOut()
    {
        fadeInPanel.GetComponent<Image>().DOFade(0f,1f);
        fadeInPanel.GetComponentInChildren<TextMeshProUGUI>().DOFade(0f,1f).OnComplete(FadeOutFinished);
    }

    void FadeOutFinished()
    {
        fadeInPanel.SetActive(false);
        dialogManager.EnterDialogMode(inkTutorial, null, player.character, false);
    }
}
