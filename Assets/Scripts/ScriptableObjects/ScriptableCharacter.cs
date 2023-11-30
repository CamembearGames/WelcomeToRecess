using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewScriptableCharacter", menuName = "ScriptableObjects/NewScriptableCharacter")]
public class ScriptableCharacter : ScriptableObject
{
    public string nameOfCharacter;
    public Sprite portraitOfCharacter;
    public TextAsset classroomTalk;

}
