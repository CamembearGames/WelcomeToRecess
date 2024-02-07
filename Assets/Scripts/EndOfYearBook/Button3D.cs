using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button3D : MonoBehaviour
{
    [SerializeField] private YearBookManager bookGM;

    private bool hasButtonBeenPressed = false;
    private void OnMouseDown() {
        if (!hasButtonBeenPressed)
        {
            hasButtonBeenPressed = true;
            GetComponent<MeshCollider>().isTrigger = false;
            bookGM.OpenBook();
        }

    }
}
