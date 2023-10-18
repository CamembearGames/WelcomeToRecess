using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelLoader
{

    public enum Scene {
        MainMenu,
        IntroScene,
        Classroom,
        Recess,
        EndOfYearBook,
        GameEnd
    }


    public static void Load(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }
}
