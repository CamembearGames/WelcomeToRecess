using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public static class LevelLoader
{

    public enum Scene {
        MainMenu,
        IntroScene,
        Classroom,
        Recess,
        PongScene,
        EndOfYearBook,
        GameEnd
    }


    public static void Load(Scene scene) {
        DOTween.KillAll();
        SceneManager.LoadScene(scene.ToString());
    }
}
