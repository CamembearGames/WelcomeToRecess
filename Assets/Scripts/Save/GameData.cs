using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameData : MonoBehaviour
{

    public static GameData Instance;

    public int currentYear = 0;
    public int currentRecess = 0;
    public int currentClass = 0;

    private int maxYear = 3;
    private int maxRecess = 3;

    public Dictionary<String, int> relationshipDatabase = new Dictionary<String, int>();
    public Dictionary<String, bool> talkAlreadyDatabase = new Dictionary<String, bool>();

    public bool hasDoneTutorial = true;

    public enum Segments {
        MainMenu,
        IntroScene,
        Classroom,
        Recess,
        PongScene,
        EndOfYearBook,
        GameEnd
    }

    public Segments currentSegment;


    [SerializeField] public String[] listOfCharacters;

    private void Awake()
    {
        // If data already exists dont create
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        foreach(String character in listOfCharacters)
        {
            relationshipDatabase.Add(character, 4); 
            talkAlreadyDatabase.Add(character, false); 
        }

        currentSegment =  Segments.Recess;

    }

    public void resetTalkedTo()
    {
        var keys = new List<string>(talkAlreadyDatabase.Keys);
        foreach (string key in keys)
        {
        talkAlreadyDatabase[key] = false;
        }
    }
    public bool CheckIsLastRecess()
    {
        return currentRecess == maxRecess;
    }

    public bool CheckIsLastYear()
    {
        return currentYear == maxYear;
    }
}
