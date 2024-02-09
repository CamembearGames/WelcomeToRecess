using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameData : MonoBehaviour
{

    public static GameData Instance;

    // General game data
    public int currentYear = 0;
    public int currentRecess = 0;
    public int currentClass = 0;
    public int activitiesDone = 0;
    public int activitiesMax = 2;

    private int maxYear = 3;
    private int maxRecess = 3;

    // Relashionship databases and also who has talked to whom
    public Dictionary<String, int> relationshipDatabase = new Dictionary<String, int>();
    public Dictionary<String, bool> talkAlreadyDatabase = new Dictionary<String, bool>();

    // Has the player done the tutorial, set to false after the tutorial starts
    public bool hasDoneTutorial = false;

    // Segments are used to know at whatt point the character is in the game
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

    // Variable to save player position when changing scenes
    public Vector3 lastPlayerPosition = Vector3.zero;

    // List of character names with which the player can have an interation with, the names have to be the same as the prefab. Probably can be automated in the future
    [SerializeField] public String[] listOfCharacters;

    public bool miniGameWon = false;

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

        // For each character at the start of the game we fill in the database with default values
        foreach(String character in listOfCharacters)
        {
            relationshipDatabase.Add(character, 7); 
            talkAlreadyDatabase.Add(character, false); 
        }

        // Game starts on the recess screen
        currentSegment =  Segments.Recess;

        // Set to default start entrance position
        lastPlayerPosition = new Vector3(-7.76f,0.55f,-7.79f);

    }

    // we reset whom the character has talked to each change of Scene
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
