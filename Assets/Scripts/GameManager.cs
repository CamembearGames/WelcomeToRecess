using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

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


    // Start is called before the first frame update
    void Start()
    {
        if (isRecess)
        {        
            ResetActivityChecks();
            recessNumber.text = GameData.Instance.currentRecess.ToString();
            yearNumber.text = GameData.Instance.currentYear.ToString();
            componentBase = vCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
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
        Debug.Log(character);
        Debug.Log(GameData.Instance.relationshipDatabase[character]);
    }

    public void GoBackToClass()
    {
        LevelLoader.Load(LevelLoader.Scene.Recess);
    }

    public void ContinueTutorial()
    {
        tutorialArea.SetActive(false);
        
    }
}
