using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using DG.Tweening;
using System.Runtime.InteropServices.WindowsRuntime;

public class GameManager : MonoBehaviour
{
    [Header("Only need in recess")]
    [SerializeField] private  CinemachineVirtualCamera vCamera;
    [SerializeField] private GameObject player;

    [Header("Only used to set up tutorial")]

    [SerializeField] private GameObject tutorialArea;
    [SerializeField] private ScriptableCharacter tutorialChar;
    [SerializeField] private TextAsset explanation1;
    [SerializeField] private TextAsset explanation2;
    [SerializeField] private TextAsset explanation3;

    [Header("UI")]
    [SerializeField] private GameObject fadeInPanel;
    [SerializeField] private GameObject slider;
    [SerializeField] private DialogManager diagManager;
    [SerializeField] private TextMeshProUGUI recessNumber;
    [SerializeField] private TextMeshProUGUI yearNumber;
    [SerializeField] private GameObject[] activityUIChecks;


    [Header("Only needed in pong game")]
    [SerializeField] private PongGameManager pongGM;

    private LevelLoader.Scene sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        if (GameData.Instance.currentSegment == GameData.Segments.Recess)
        {        
            ResetActivityChecks();
            recessNumber.text = GameData.Instance.currentRecess.ToString();
            yearNumber.text = GameData.Instance.currentYear.ToString();
            //componentBase = vCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);

            player.transform.position = GameData.Instance.lastPlayerPosition;

            GameData.Instance.resetTalkedTo();
        }
    }

    // Reset activity checks
    //---------------------------------------------------------------------------------------------------------------------

    private void ResetActivityChecks()
    {
        for (int i = 0; i < GameData.Instance.activitiesDone; i++)
        {
            activityUIChecks[i].SetActive(true);
        }
    }

    /*public void PeformActivity()
    {
        if (isRecess)
        {
            Debug.Log("Activity performed, does checkmark appear ?");
            activityUIChecks[activitiesPerformed].SetActive(true);
            activitiesPerformed++;

            if (activitiesPerformed == maxNumberOfActivities)
            {
                GameData.Instance.currentRecess ++;
                if(GameData.Instance.CheckIsLastRecess())
                {
                    GameData.Instance.currentYear ++;
                    GameData.Instance.currentRecess = 0;
                    LevelLoader.Load(LevelLoader.Scene.EndOfYearBook);

                }
                else
                {
                    LevelLoader.Load(LevelLoader.Scene.Classroom);
                }
            }
        }
    }*/

    // Functions that are bound with Ink files.
    //---------------------------------------------------------------------------------------------------------------------

    public void ChangeRelationship(String character, int value)
    {
        GameData.Instance.relationshipDatabase[character] += value;
    }
    public void UpdateRelashionship(String character, int value)
    {
        GameData.Instance.relationshipDatabase[character] = value;
        slider.GetComponent<SliderController>().AnimateProgress(value);
        //Debug.Log("Relationship Updated");
        //Debug.Log(value);
    }
    public void UpdateTalkAlready(String character, bool value)
    {
        if (GameData.Instance) GameData.Instance.talkAlreadyDatabase[character] = value;
    }
    public void GoBackToClass()
    {
        GameData.Instance.lastPlayerPosition = player.transform.position;
        sceneToLoad = LevelLoader.Scene.Classroom;
        GameData.Instance.currentSegment = GameData.Segments.Classroom;
        if (GameData.Instance)GameData.Instance.currentRecess += 1;
        Fadein();
    }
    public void GoBackToRecess()
    {
        sceneToLoad = LevelLoader.Scene.Recess;
        GameData.Instance.currentSegment = GameData.Segments.Recess;
        if (GameData.Instance)GameData.Instance.currentClass += 1;
        Fadein();
    }
    public void UseTimeSlot(int numberOfTimeSlots)
    {
        if (GameData.Instance.currentSegment == GameData.Segments.Recess)
        {
            GameData.Instance.activitiesDone += numberOfTimeSlots;
            ResetActivityChecks();
        }
    }
    public void StartMiniGame(int miniGameNumber)
    {
        GameData.Instance.lastPlayerPosition = player.transform.position;
        if (miniGameNumber == 0)
        {
            sceneToLoad = LevelLoader.Scene.PongScene;
            GameData.Instance.currentSegment = GameData.Segments.PongScene;
            Fadein();
        }
    }
    // Tutorial code, only used for the first recess if the player chooses to do the tutorial.
    //---------------------------------------------------------------------------------------------------------------------

    public void ContinueTutorial()
    {
        Debug.Log("Tutorial Continue");
        tutorialArea.GetComponent<BoxCollider>().enabled = false;
        tutorialArea.GetComponent<PlayableDirector>().Play();
    }

    public void CancelTutorial()
    {
        tutorialArea.SetActive(false);
        player.GetComponent<PlayerController>().OnEnable();
    }

    public void EnterExplanation1()
    {
        tutorialArea.GetComponent<PlayableDirector>().Pause();
        diagManager.EnterDialogMode(explanation1, tutorialChar, tutorialChar, true, 1.0f);
    }

    public void EnterExplanation2()
    {
        tutorialArea.GetComponent<PlayableDirector>().Pause();
        diagManager.EnterDialogMode(explanation2, tutorialChar, tutorialChar, true, 1.0f);
    }

    public void EnterExplanation3()
    {
        tutorialArea.GetComponent<PlayableDirector>().Pause();
        diagManager.EnterDialogMode(explanation3, tutorialChar, tutorialChar, true, 1.0f);
    }

    //Only used in Pong game
    //---------------------------------------------------------------------------------------------------------------------

    public void StartPongMatch()
    {
        if (pongGM) Invoke("SendSignalToPongGM", 1.0f);
    }

    public void SendSignalToPongGM()
    {
        pongGM.StartGame();
    }

    // Code for changing scene
    //---------------------------------------------------------------------------------------------------------------------

    void Fadein()
    {
        diagManager.RemoveBindings();
        fadeInPanel.SetActive(true);
        fadeInPanel.GetComponent<Image>().DOFade(1f,1f).OnComplete(FadeinFinished);;
        //fadeInPanel.GetComponentInChildren<TextMeshProUGUI>().DOFade(1f,1f).OnComplete(FadeinFinished);
    }

    void FadeinFinished()
    {
        LevelLoader.Load(sceneToLoad);
    }

}