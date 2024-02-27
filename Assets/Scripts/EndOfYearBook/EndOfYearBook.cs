using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfYearBook : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start end of year book");
        Debug.Log(GameData.Instance.currentRecess);
        Debug.Log(GameData.Instance.currentYear);
    }

    public void LoadNextYear()
    {
        if(GameData.Instance.CheckIsLastYear())
        {
            LevelLoader.Load(LevelLoader.Scene.IntroScene);
        }
        else
        {
            LevelLoader.Load(LevelLoader.Scene.Schoolyard);
        }
        
    }
}
