using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    public Rigidbody rigidBody;
    public float moveSpeed, jumpSpeed;
    public PlayerInputActions playerControls;

    private Vector2 moveInput; 
    private InputAction move;
    private InputAction jump;

    public LayerMask groundLayer;
    public Transform groundPoint;
    private bool isGrounded;

    public Animator animator;
    public Animator flipAnimator;

    public SpriteRenderer spriteRenderer;

    public GameObject SpriteHolder;

    private bool isflipped = false;


    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }
    
    private void OnEnable()
    {
        move = playerControls.Player.Move;
        jump = playerControls.Player.Jump;

        move.Enable();
        jump.Enable();

        jump.performed += Jump;
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogManager.GetInstance().dialogIsPlaying)
        {
            return;
        }
        moveInput = move.ReadValue<Vector2>();
        rigidBody.velocity = new Vector3(moveInput.x * moveSpeed, rigidBody.velocity.y, moveInput.y * moveSpeed);

        animator.SetFloat("speed", (new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z)).magnitude);

        RaycastHit hit;            
        if (Physics.Raycast(groundPoint.position, Vector3.down, out hit, .9f, groundLayer))
        {
            isGrounded = true;
        }
        else{
            isGrounded = false;    
        }
        animator.SetBool("isJumping", !isGrounded);

        

        if (!isflipped && moveInput.x < 0)
        {
                DOTween.Clear();
                SpriteHolder.transform.DOLocalRotate(new Vector3 (0f,180f,0f), .5f).SetEase(Ease.InOutSine);
                isflipped = !isflipped;

        } else if (isflipped && moveInput.x > 0 )
        {
                DOTween.Clear();
                SpriteHolder.transform.DOLocalRotate(new Vector3 (0f,0f,0f), .5f).SetEase(Ease.InOutSine);
                isflipped = !isflipped;
        }

    }

    
    private void Jump (InputAction.CallbackContext context)
    {
        if (DialogManager.GetInstance().dialogIsPlaying)
        {
            return;
        }
        if (isGrounded)
        {
            rigidBody.velocity += new Vector3(0f, jumpSpeed, 0f);
        }
    }


}
