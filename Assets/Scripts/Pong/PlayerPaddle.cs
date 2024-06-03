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

    [SerializeField] private bool isComputer;

    [SerializeField] private GameObject ball;

    private Vector2 moveDirection = Vector2.zero;

    public float aiDeadZone = 1;

   private void Awake()
    {
        playerControls = new PlayerInputActions();
        startPosition = transform.position;
    }
    
    public void OnEnable()
    {
        if (!isComputer)
        {
            move = playerControls.Player.Move;
            move.Enable();
        }

    }

    public void OnDisable()
    {
        if (!isComputer)
        {
            move.Disable();
        }
    }

    void Start()
    {
        OnEnable();
    }

    void FixedUpdate()
    {
        if (!isComputer)
        {
            moveInput = move.ReadValue<Vector2>();
            paddleBody.velocity = new Vector2(paddleBody.velocity.x, moveInput.y*speed);
        }
        else
        {
            Vector2 ballPos = ball.transform.position - transform.position;
            if (Mathf.Abs(ballPos.y-transform.position.y)>aiDeadZone)
            {
                moveDirection = ball.transform.position - transform.position;           
                paddleBody.velocity = new Vector2(paddleBody.velocity.x, moveDirection.normalized.y*speed);
            }
        }
        
    }

    public void Reset()
    {
        paddleBody.velocity = Vector2.zero;
        transform.position = startPosition;
        
    }

    public float GetHeight()
    {
        return transform.localScale.y;
    }

    public bool IsComputer()
    {
        return isComputer;
    }
}
