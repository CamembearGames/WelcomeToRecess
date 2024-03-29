using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DG.Tweening;
using Ink.Parsed;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    [SerializeField] public ScriptableCharacter character;

    public Rigidbody rigidBody;
    public float moveSpeed, jumpSpeed;
    public PlayerInputActions playerControls;

    private Vector2 moveInput; 
    private InputAction move;
    private InputAction jump;

    public LayerMask groundLayer;
    public Transform groundPoint;

    public Animator animator;
    public Animator flipAnimator;

    public SpriteRenderer spriteRenderer;

    public GameObject SpriteHolder;

    //private bool isflipped = false;

    private bool freeMovement = true;
    private Vector3 targetPosition = Vector3.zero;


    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }
    
    public void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
    }

    public void OnDisable()
    {
        move.Disable();
    }

    void Start()
    {
        OnEnable();
    }


    // Update is called once per frame
    void Update()
    {
        if (freeMovement)
        {
            moveInput = move.ReadValue<Vector2>();
        }
        else
        {
            if ((targetPosition-this.transform.position).magnitude>0.1)
            {
                moveInput = new Vector2((targetPosition-this.transform.position).x,(targetPosition-this.transform.position).z).normalized;
            }
            else
            {
                moveInput = Vector2.zero;
                freeMovement = true;
                this.transform.position = targetPosition;

            }
        }
        
        rigidBody.velocity = new Vector3(moveInput.x * moveSpeed, rigidBody.velocity.y, moveInput.y * moveSpeed);

        animator.SetFloat("speed", (new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z)).magnitude);
        

        /*if (!isflipped && moveInput.x < 0)
        {
                DOTween.Clear();
                SpriteHolder.transform.DOLocalRotate(new Vector3 (0f,180f,0f), .5f).SetEase(Ease.InOutSine);
                isflipped = !isflipped;

        } else if (isflipped && moveInput.x > 0 )
        {
                DOTween.Clear();
                SpriteHolder.transform.DOLocalRotate(new Vector3 (0f,0f,0f), .5f).SetEase(Ease.InOutSine);
                isflipped = !isflipped;
        }*/

    }


    public void MoveToPoint(Vector3 target_position)
    {
        OnDisable();
        target_position.y = this.transform.position.y;
        freeMovement = false;
        this.targetPosition = target_position;
        //this.transform.position = target_position;
    }

    public void FinishDialog()
    {
        freeMovement = true;
    }
}
