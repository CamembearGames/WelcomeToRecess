using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPaddle : MonoBehaviour
{
    public PlayerInputActions playerControls;

    private Vector2 moveInput; 
    private InputAction move;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D paddleBody;

    [SerializeField] private Vector3 startPosition;

   private void Awake()
    {
        playerControls = new PlayerInputActions();
        startPosition = transform.position;
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

    void FixedUpdate()
    {
        moveInput = move.ReadValue<Vector2>();

        paddleBody.velocity = new Vector2(paddleBody.velocity.x, moveInput.y*speed);
        
    }

    public void Reset()
    {
        paddleBody.velocity = Vector2.zero;
        transform.position = startPosition;
        
    }
}
