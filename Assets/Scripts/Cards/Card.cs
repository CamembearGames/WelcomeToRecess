using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool hasBennPlayed;

    public int handIndex;

    private void OnMouseDown() 
    {
        if (hasBennPlayed == false)
        {
            transform.position += Vector3.up *5;
            hasBennPlayed = true;
        }
    }

}
