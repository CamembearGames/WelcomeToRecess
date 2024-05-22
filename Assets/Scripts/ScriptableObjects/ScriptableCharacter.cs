using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NewScriptableCharacter", menuName = "ScriptableObjects/NewScriptableCharacter")]
public class ScriptableCharacter : ScriptableObject
{
    public string nameOfCharacter;
    public Sprite portraitOfCharacter;
    public Sprite outlineOfCharacter;
    public TextAsset classroomTalk;
    public ScriptableInteractions []interactions;

    public List<TextAsset> PotentialDialogs;//TextAsset []PotentialDialogs;s
    public List<TextAsset> BaseDialogs;//TextAsset []PotentialDialogs;
    public List<TextAsset> PriorityDialogs;

    private void Awake() {
        hideFlags = HideFlags.DontUnloadUnusedAsset;

    }
    public void ResetDialogs() {
        PotentialDialogs.Clear();
        PriorityDialogs.Clear();
        for (int i = 0; i < BaseDialogs.Count; i++)
        {
            PotentialDialogs.Add(BaseDialogs[i]);
        }
    }
}
