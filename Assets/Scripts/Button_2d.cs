using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_2d : MonoBehaviour
{

    public Sprite upButton;
    public Sprite downButton;

    public bool canBeActive = true;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject DialogPanel;
  
    public ClassroomStudents Classroom;


    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown() {
        if(canBeActive)
        {
            spriteRenderer.sprite = downButton;
            GetComponentInParent<ClassroomStudent>().updatePortrait();
            GetComponentInParent<ClassroomStudent>().updateDialog();
            Classroom.DesactivateButtons();
            DialogPanel.SetActive(true);
        }

    }

    private void OnMouseUp() {
        if(canBeActive)
        {
            spriteRenderer.sprite = upButton;
        }
    }
}
