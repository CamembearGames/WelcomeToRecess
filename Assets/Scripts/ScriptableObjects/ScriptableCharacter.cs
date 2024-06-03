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

    public ScriptableInteractions []interactions;

    public List<TextAsset> PotentialDialogs;
    public List<TextAsset> BaseDialogs;

    public List<TextAsset> PotentialClassDialogs;
    public List<TextAsset> BaseClassDialogs;

    public List<TextAsset> PriorityDialogs;
    public List<TextAsset> PriorityClassDialogs;

    public TextAsset DefaultDialog;

    public bool isSpecialDialog = false;

    private void Awake() {
        hideFlags = HideFlags.DontUnloadUnusedAsset;

    }
    public void ResetDialogs() {

        PotentialDialogs.Clear();
        PriorityDialogs.Clear();
        PotentialClassDialogs.Clear();
        PriorityClassDialogs.Clear();


        for (int i = 0; i < BaseDialogs.Count; i++)
        {
            PotentialDialogs.Add(BaseDialogs[i]);
        }

        for (int i = 0; i < BaseClassDialogs.Count; i++)
        {
            PotentialClassDialogs.Add(BaseClassDialogs[i]);
        }

    }

    public void reAddText(TextAsset text)
    {
        if (isSpecialDialog) PriorityDialogs.Add(text);
        else PotentialDialogs.Add(text);
    }


}
