using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] public Dictionary<String, int> relationshipDatabase = new Dictionary<String, int>();
    public Dictionary<String, bool> talkAlreadyDatabase = new Dictionary<String, bool>();

    // Has the player done the tutorial, set to false after the tutorial starts
    public bool hasDoneTutorial = false;

    public List<ScriptableInteractions> Interactions;
    public List<ScriptableCharacter> AvailableCharacters;//TextAsset []PotentialDialogs;s

    public List<TextAsset> SpecialDialogs;

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

    public enum PassiveActivities {
        None,
        WaterPlants
    }

    public PassiveActivities currentPassiveActivity;
    public GameObject currentSelectedPassiveObject;

    public int numberOfTimesBushWatered = 0;

    public bool hasWatered = false;


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

         foreach (ScriptableCharacter character in AvailableCharacters)
        {
            character.ResetDialogs();
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // For each character at the start of the game we fill in the database with default values
        foreach(String character in listOfCharacters)
        {
            relationshipDatabase.Add(character, 5); 
            talkAlreadyDatabase.Add(character, false); 
        }
        relationshipDatabase["John"] = 3;

        // Game starts on the recess screen
        //currentSegment =  Segments.Recess;

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

    public void AddInteraction(ScriptableInteractions interaction)
    {
        Interactions.Add(interaction);
    }
}
