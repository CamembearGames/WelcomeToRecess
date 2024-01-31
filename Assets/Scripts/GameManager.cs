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

    [SerializeField] private DialogManager diagManager;
    public GameObject[] activityUIChecks;

    private int activitiesPerformed = 0;
    public int maxNumberOfActivities = 2;

    public TextMeshProUGUI recessNumber;
    public TextMeshProUGUI yearNumber;

    public CinemachineVirtualCamera vCamera;
    CinemachineComponentBase componentBase;
    public GameObject player;
    public GameObject cardGameBoards;
    public GameObject quitCardGameButton;

    public bool isRecess = false;

    [SerializeField] private GameObject tutorialArea;
    [SerializeField] private ScriptableCharacter tutorialChar;
    [SerializeField] private TextAsset explanation1;
    [SerializeField] private TextAsset explanation2;
    [SerializeField] private TextAsset explanation3;

    [SerializeField] private GameObject fadeInPanel;

    [SerializeField] private GameObject slider;

    [SerializeField] private PongGameManager pongGM;


    private LevelLoader.Scene sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        if (isRecess)
        {        
            ResetActivityChecks();
            recessNumber.text = GameData.Instance.currentRecess.ToString();
            yearNumber.text = GameData.Instance.currentYear.ToString();
            componentBase = vCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);

            GameData.Instance.resetTalkedTo();
        }

    }

    private void ResetActivityChecks()
    {
        if (isRecess)
        {
            foreach(GameObject check in activityUIChecks)
            {
                check.SetActive(false);
            }
        }
    }

    public void PeformActivity()
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
    }

    public void PlayCards()
    {
        cardGameBoards.SetActive(true);
        vCamera.Follow = cardGameBoards.transform;
        (componentBase as CinemachineFramingTransposer).m_CameraDistance = 6f;
        quitCardGameButton.SetActive(true);
    }

    public void StopPlayCards()
    {
        cardGameBoards.SetActive(false);
        vCamera.Follow = player.transform;
        (componentBase as CinemachineFramingTransposer).m_CameraDistance = 13f;
        quitCardGameButton.SetActive(false);
    }

    public void ChangeRelationship(String character, int value)
    {
        GameData.Instance.relationshipDatabase[character] += value;
        //Debug.Log(character);
        //Debug.Log(GameData.Instance.relationshipDatabase[character]);
    }

    public void UpdateRelashionship(String character, int value)
    {
        GameData.Instance.relationshipDatabase[character] = value;
        slider.GetComponent<SliderController>().AnimateProgress(value);
        //Debug.Log(character);
        //Debug.Log(GameData.Instance.relationshipDatabase[character]);
    }

    public void UpdateTalkAlready(String character, bool value)
    {
        if (GameData.Instance) GameData.Instance.talkAlreadyDatabase[character] = value;
        //Debug.Log(character);
        //Debug.Log(GameData.Instance.talkAlreadyDatabase[character]);
    }

    public void StartPongMatch()
    {
        if (pongGM) Invoke("SendSignalToPongGM", 1.0f);
    }

    public void SendSignalToPongGM()
    {
        pongGM.StartGame();
    }

    public void GoBackToClass()
    {
        sceneToLoad = LevelLoader.Scene.Classroom;
        if (GameData.Instance)GameData.Instance.currentRecess += 1;
        Fadein();
    }

    public void GoBackToRecess()
    {
        sceneToLoad = LevelLoader.Scene.Recess;
        if (GameData.Instance)GameData.Instance.currentClass += 1;
        Fadein();
    }

    public void ChangeScene()
    {
        LevelLoader.Load(sceneToLoad);
    }


    // Tutorial code, only used for the first recess if the player chooses to do the tutorial.
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

    void Fadein()
    {
        diagManager.RemoveBindings();
        fadeInPanel.SetActive(true);
        fadeInPanel.GetComponent<Image>().DOFade(1f,1f).OnComplete(FadeinFinished);;
        //fadeInPanel.GetComponentInChildren<TextMeshProUGUI>().DOFade(1f,1f).OnComplete(FadeinFinished);
    }

    void FadeinFinished()
    {
        ChangeScene();
    }

    public void UseTimeSlot(int numberOfTimeSlots)
    {
        if (isRecess)
        {
            for (int i = 0; i< numberOfTimeSlots; i++)
            {
                activityUIChecks[activitiesPerformed].SetActive(true);
                activitiesPerformed++;
            }

            if (activitiesPerformed >= maxNumberOfActivities)
            {
                GoBackToClass();
            }
        }
    }

    public void StartMiniGame(int miniGameNumber)
    {
        if (miniGameNumber == 0)
        {
            sceneToLoad = LevelLoader.Scene.PongScene;
            Fadein();
        }
    }

}