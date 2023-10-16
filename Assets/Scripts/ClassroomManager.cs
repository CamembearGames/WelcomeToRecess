using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomManager : MonoBehaviour
{
    public void GoToRecess(){
        LevelLoader.Load(LevelLoader.Scene.MainScene);
    }
}
