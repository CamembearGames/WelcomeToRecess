using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public Rigidbody rigidBody;
    public float moveSpeed, jumpSpeed;
    public InputAction playerControls;

    private Vector2 moveInput; 

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.x = Input.GetAxis("Vertical");
        moveInput.Normalize();

        rigidBody.velocity = new Vector3(moveInput.x * moveSpeed, rigidBody.velocity.y, moveInput.y * moveSpeed);
    }
}
