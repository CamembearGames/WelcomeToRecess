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
    [SerializeField] public  CinemachineVirtualCamera vCamera;
    [SerializeField] public  Camera mainCamera;

    //[SerializeField] private  CinemachineVirtualCamera vCamera;

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
    [SerializeField] private GeneralUIScript UIPanel;


    [Header("UI")]
    [SerializeField] private AudioSource bellSound;


    [Header("Only needed in pong game")]
    [SerializeField] private PongGameManager pongGM;
    [SerializeField] private TutorialScriptNoPlayer tutNoPlayer;

    [Header("Story")]
    [SerializeField] private GameObject akim;
    [SerializeField] private ScriptableInteractions playerInteract;

    [SerializeField] private NonPlayableCharacterClick[] npcs;

    [SerializeField] List<TextAsset>SpecialDialogs;

    [SerializeField] private QuestionPanel QuestionPanel;


    private LevelLoader.Scene sceneToLoad;

    public bool canInteract = false;
    public bool canRotate = false;
    

    // Start is called before the first frame update
    void Start()
    {
        if (GameData.Instance.currentSegment == GameData.Segments.Classroom)
        {        
            GameData.Instance.activitiesDone = 0;
            
            recessNumber.text = GameData.Instance.currentRecess.ToString();
            yearNumber.text = GameData.Instance.currentYear.ToString();
            //componentBase = vCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);

            //player.transform.position = GameData.Instance.lastPlayerPosition;

            GameData.Instance.resetTalkedTo();
        }
        ResetActivityChecks();

        //Special Event Akim's arrival 

        if (GameData.Instance.currentSegment == GameData.Segments.Recess )
        {
            if (GameData.Instance.currentRecess == 1)
            {
                akim.SetActive(true);
                foreach (String charName in GameData.Instance.listOfCharacters)
                {
                    TextAsset textToAdd = GameData.Instance.SpecialDialogs[0];
                    AddCharacterDialog(charName, textToAdd);
                }
            }

            UIPanel.UpdateActivityText();
        }

        if (GameData.Instance.currentSegment == GameData.Segments.Classroom )
        {
            if (GameData.Instance.currentClass == 1) akim.SetActive(true);
   
        }
       
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ScreenCapture.CaptureScreenshot("screenshot.png");
            Debug.Log("A screenshot was taken!");
        }


    }

    // Reset activity checks
    //---------------------------------------------------------------------------------------------------------------------

    private void ResetActivityChecks()
    {
        for (int i = 0; i < GameData.Instance.activitiesDone; i++)
        {
            //activityUIChecks[i].SetActive(true);
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
        if (GameData.Instance.relationshipDatabase[character]>value) slider.GetComponent<SliderController>().PlayNegative();
        else slider.GetComponent<SliderController>().PlayPositive();
        GameData.Instance.relationshipDatabase[character] = value;
        slider.GetComponent<SliderController>().AnimateProgress(value);
        //Debug.Log("Relationship Updated");
        //Debug.Log(value);
    }
    public void UpdateTalkAlready(String character, bool value)
    {
        if (GameData.Instance) GameData.Instance.talkAlreadyDatabase[character] = value;
    }

    public void WaterBush(GameObject clickedObject)
    {
        GameData.Instance.currentPassiveActivity = GameData.PassiveActivities.WaterPlants;
        GameData.Instance.currentSelectedPassiveObject = clickedObject;
        QuestionPanel.ShowDialogBox();
    }

    public void GoBackToClass()
    {
        if (player) GameData.Instance.lastPlayerPosition = player.transform.position;
        sceneToLoad = LevelLoader.Scene.ClassroomV2;
        GameData.Instance.currentSegment = GameData.Segments.Classroom;
        if (GameData.Instance)GameData.Instance.currentRecess += 1;
        Fadein();
    }

    public void GoBackToRecess()
    {
        sceneToLoad = LevelLoader.Scene.Schoolyard;
        if (GameData.Instance && GameData.Instance.currentSegment == GameData.Segments.Classroom)
        {
            GameData.Instance.currentClass += 1;
            if (GameData.Instance.currentRecess == 2) 
            {
                GameData.Instance.AddInteraction(playerInteract);
                sceneToLoad = LevelLoader.Scene.EndYearBook;
            }
            //sceneToLoad = LevelLoader.Scene.Schoolyard;
        }
        
        GameData.Instance.currentSegment = GameData.Segments.Recess;
        
        Fadein();
    }
    public void UseTimeSlot(int newTimeSlots)
    {
        if (GameData.Instance.currentSegment == GameData.Segments.Recess)
        {
            GameData.Instance.activitiesDone = newTimeSlots;
            ResetActivityChecks();
            UIPanel.UpdateActivity();

        }
        else GameData.Instance.activitiesDone = newTimeSlots;
    }

    public void WateringAcknowledge()
    {
        GameData.Instance.hasWatered = false;
    }

    public void AddSpecialCharacter(int newTimeSlots)
    {
        if (GameData.Instance.currentSegment == GameData.Segments.Recess)
        {
            GameData.Instance.activitiesDone = newTimeSlots;
            ResetActivityChecks();
        }
        else GameData.Instance.activitiesDone = newTimeSlots;
    }

    public void StartMiniGame(int miniGameNumber)
    {
        if(player) GameData.Instance.lastPlayerPosition = player.transform.position;
        if (miniGameNumber == 0)
        {
            sceneToLoad = LevelLoader.Scene.PongScene;
            GameData.Instance.currentSegment = GameData.Segments.PongScene;
            Fadein();
        }
    }

    public void AddCharacterDialog(String characterName, TextAsset textToAdd)
    {
        foreach(ScriptableCharacter character in GameData.Instance.AvailableCharacters)
        {
            if (character.nameOfCharacter == characterName)
            {
                character.PriorityDialogs.Add(textToAdd);
            }
        }   
    }

    public void AddInteraction(int interactionNumber)
    {   
        ScriptableInteractions interaction = diagManager.currentNPC.character.interactions[interactionNumber];
        GameData.Instance.AddInteraction(interaction);
    }
    // Tutorial code, only used for the first recess if the player chooses to do the tutorial.
    //---------------------------------------------------------------------------------------------------------------------

    public void StartTutorial()
    {
        //tutNoPlayer.StartDialog();
        //tutNoPlayer.SwitchCamera();
        tutNoPlayer.EndCamera();
    }
    public void ContinueTutorial()
    {
        tutNoPlayer.SwitchCamera();
        
    
        //tutorialArea.GetComponent<BoxCollider>().enabled = false;
        //tutorialArea.GetComponent<PlayableDirector>().Play();
    }

    public void CancelTutorial()
    {
        tutNoPlayer.EndCamera();

        foreach (NonPlayableCharacterClick elem in npcs)
        {
            elem.GetComponent<Animation>().Play("WalkAround");
        }

        //tutorialArea.SetActive(false);
        //player.GetComponent<PlayerController>().OnEnable();
    }

    public void EnterExplanation1()
    {
        tutorialArea.GetComponent<PlayableDirector>().Pause();
        diagManager.EnterDialogMode(explanation1, tutorialChar, true, 1.0f);
    }

    public void EnterExplanation2()
    {
        tutorialArea.GetComponent<PlayableDirector>().Pause();
        diagManager.EnterDialogMode(explanation2, tutorialChar, true, 1.0f);
    }

    public void EnterExplanation3()
    {
        tutorialArea.GetComponent<PlayableDirector>().Pause();
        diagManager.EnterDialogMode(explanation3, tutorialChar, true, 1.0f);
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


    // Code question answering
    //---------------------------------------------------------------------------------------------------------------------

    public void AnswerYes()
    {
        UIPanel.PlayClick();
        QuestionPanel.DeactivateButtons();
        QuestionPanel.HideDialogBox();
        Invoke("DoActivity", 0.4f);

    }
    public void DoActivity()
    {
        switch (GameData.Instance.currentPassiveActivity)
        {
            case GameData.PassiveActivities.WaterPlants:
                GameData.Instance.numberOfTimesBushWatered +=1;
                GameData.Instance.hasWatered = true;
                UseTimeSlot(GameData.Instance.activitiesDone+1);
                Invoke("PlantShake", 0.2f);
                break;
            default:
                Debug.Log("No activity selected");
                break;
        }
    }

    public void PlantShake()
    {
        GameData.Instance.currentSelectedPassiveObject.GetComponent<Bush>().Shake();
        Invoke("ResetCamera", 1f);
    }

    public void ResetCamera()
    {
        canRotate = true;
        mainCamera.GetComponent<cameraMovement>().resetCamera();
    }

    public void AnswerNo()
    {
        UIPanel.PlayClick();
        QuestionPanel.DeactivateButtons();
        QuestionPanel.HideDialogBox();
        canRotate = true;
        mainCamera.GetComponent<cameraMovement>().resetCamera();
    }


}