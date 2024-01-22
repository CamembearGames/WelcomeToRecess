using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomManager : MonoBehaviour
{
    public bool canClick = false;
    private void Start() 
    {
        /*Debug.Log("Start classroom");
        Debug.Log(GameData.Instance.currentRecess);
        Debug.Log(GameData.Instance.currentYear);*/
        canClick = true;
    }
    public void GoToRecess(){
        LevelLoader.Load(LevelLoader.Scene.Recess);
    }
}
