using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject[] activityUIChecks;

    private int activitiesPerformed = 0;
    public int maxNumberOfActivities = 2;
    // Start is called before the first frame update
    void Start()
    {
        ResetActivityChecks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResetActivityChecks()
    {
        foreach(GameObject check in activityUIChecks)
        {
            check.SetActive(false);
        }
    }

    public void PeformActivity()
    {
        Debug.Log("Activity performed, does checkmark appear ?");
        activityUIChecks[activitiesPerformed].SetActive(true);
        activitiesPerformed++;

        if (activitiesPerformed == maxNumberOfActivities)
        {
            LevelLoader.Load(LevelLoader.Scene.Classroom);
        }
    }
}
