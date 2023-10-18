using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScript : MonoBehaviour
{
    public void ReturnToMenu()
    {
        LevelLoader.Load(LevelLoader.Scene.MainMenu);
    }
}
