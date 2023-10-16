using System.Collections;
using System.Collections.Generic;
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
        moveInput = move.ReadValue<Vector2>();
        rigidBody.velocity = new Vector3(moveInput.x * moveSpeed, rigidBody.velocity.y, moveInput.y * moveSpeed);

        RaycastHit hit;

            
        if (Physics.Raycast(groundPoint.position, Vector3.down, out hit, .9f, groundLayer))
        {
            isGrounded = true;
        }
        else{
            isGrounded = false;
        }
    }

    
    private void Jump (InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rigidBody.velocity += new Vector3(0f, jumpSpeed, 0f);
        }
    }
}
