using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneScript : MonoBehaviour
{
    public void SkipIntro(){
        LevelLoader.Load(LevelLoader.Scene.Classroom);
        GameData.Instance.currentRecess = 0;
        GameData.Instance.currentYear = 0;
    }

    public void QuitGame(){
        Application.Quit();
    }
}
