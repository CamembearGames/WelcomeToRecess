using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomStudents : MonoBehaviour
{
    public Button_2d[] buttons;

    public void DesactivateButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].canBeActive = false;
        }
    }

    public void ActivateButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].canBeActive = true;
        }
    }
}
