using UnityEngine;
using System.Collections;
using Unity.Mathematics;
using System;

public class BlendShapeLoop : MonoBehaviour
{
    int blendShapeCount;
    SkinnedMeshRenderer skinnedMeshRenderer;
    Mesh skinnedMesh;

    int playIndex = 0;
    int previousPlayIndex = 0;

    float playIndexF = 0;

    float currentFPSRatio = 0;

    public bool playPageTurn = false;
    private bool pageTurned = false;


    void Awake ()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer> ();
        skinnedMesh = GetComponent<SkinnedMeshRenderer> ().sharedMesh;
    }

    void Start ()
    {
        blendShapeCount = skinnedMesh.blendShapeCount; 
    }

    void Update ()
    {
        if (playPageTurn)
        {
            currentFPSRatio = 60/(1/Time.deltaTime);
            
            playIndex = Mathf.RoundToInt(playIndexF);

            skinnedMeshRenderer.SetBlendShapeWeight(previousPlayIndex, 0f);
            skinnedMeshRenderer.SetBlendShapeWeight(playIndex, 100f);

            previousPlayIndex = playIndex;

            playIndexF += currentFPSRatio*(pageTurned ? -1 : 1);
            if(playIndexF > blendShapeCount -1) 
            {
                playIndexF = 119f;
                pageTurned = true;
                playPageTurn = false;
            }
            else if(playIndexF < 0)
            {
                playIndexF = 0f;
                pageTurned = false;
                playPageTurn = false;
            }
        }

    }

    public void startPageTurn()
    {
        GetComponent<Animator>().SetTrigger("TurnPage");
        playPageTurn = true;
        GetComponent<AudioSource>().Play();
    }
}