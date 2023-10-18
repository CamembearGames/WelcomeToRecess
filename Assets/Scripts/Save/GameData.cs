using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{

    public static GameData Instance;

    public int currentYear = 0;
    public int currentRecess = 0;

    private int maxYear = 3;
    private int maxRecess = 3;

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
