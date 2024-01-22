using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChairButton : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Transform spriteHolder;
    public bool isenabled = false;

    public float tweenTime = 0.25f;

    [SerializeField] private GameObject DialogPanel;
      [SerializeField] private GameObject ClassroomGameManager;

    public ClassroomStudents Classroom;
    public ClassroomStudent associatedStudent;

    void Start()
    {
        //spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    }


    void OnMouseEnter() {
        if (isenabled & ClassroomGameManager.GetComponent<ClassroomManager>().canClick)
        {
            spriteRenderer.DOColor( new Color(1f,1f,1f,1f),tweenTime).SetEase(Ease.InOutSine);
            spriteHolder.transform.DOScale( new Vector3(1.3f,1.3f,1.3f),tweenTime).SetEase(Ease.InOutSine);
        }


    }

    void OnMouseExit() {
        if (isenabled & ClassroomGameManager.GetComponent<ClassroomManager>().canClick)
        {
            spriteRenderer.DOColor( new Color(1f,1f,1f,0f),tweenTime).SetEase(Ease.InOutSine);
            spriteHolder.transform.DOScale( new Vector3(1f,1f,1f),tweenTime).SetEase(Ease.InOutSine);
        }
    }

    private void OnMouseDown() {
        if(isenabled & ClassroomGameManager.GetComponent<ClassroomManager>().canClick)
        {
            ClassroomGameManager.GetComponent<ClassroomManager>().canClick = false;
            if (associatedStudent != null)
            {
                isenabled = false;
                //associatedStudent.updatePortrait();
                associatedStudent.updateDialog();
                //DialogPanel.SetActive(true);
            }

        }
    }
}
