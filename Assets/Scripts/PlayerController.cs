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
    private bool isGrounded;

    public Animator animator;
    public Animator flipAnimator;

    public SpriteRenderer spriteRenderer;

    public GameObject SpriteHolder;

    private bool isflipped = false;

    public bool canMove = true;

    private bool freeMovement = true;
    private Vector3 targetPosition = Vector3.zero;

    public GameObject answer1;
    public GameObject answer2;
    public GameObject answer3;


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
        if (!canMove)
        {
            return;
        }

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
            }
        }
        
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
        if (DialogManager.GetInstance().dialogIsPlaying || !canMove)
        {
            return;
        }
        if (isGrounded)
        {
            rigidBody.velocity += new Vector3(0f, jumpSpeed, 0f);
        }
    }

    public void MoveToPoint(Vector3 target_position)
    {
        target_position.y = this.transform.position.y;
        freeMovement = false;
        targetPosition = target_position;
        //this.transform.position = target_position;
    }

    public void FinishDialog()
    {
        freeMovement = true;
    }

    public void ShowDialogBox()
    {
        answer1.SetActive(true);
        answer1.GetComponent<Animation>().Play("BubleAnim");
        answer2.SetActive(true);
        answer2.GetComponent<Animation>().Play("BubleAnim");
        answer3.SetActive(true);
        answer3.GetComponent<Animation>().Play("BubleAnim");
    }

    public void StopMoving()
    {
        canMove = false;
        rigidBody.velocity = Vector3.zero;
        animator.SetFloat("speed", (new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z)).magnitude);
    }

    public void StartMoving()
    {
        canMove = true;

    }
}
