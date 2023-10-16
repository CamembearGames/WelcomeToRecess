using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelLoader
{

    public enum Scene {
        MainScene,
        Classroom
    }


    public static void Load(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }
}
