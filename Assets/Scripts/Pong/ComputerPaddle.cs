using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ComputerPaddle : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject ball;
    [SerializeField] private Rigidbody2D paddleBody;
    [SerializeField] private Vector3 startPosition;

    public float aiDeadZone = 1;
    private Vector2 moveDirection = Vector2.zero;

   private void Awake()
    {
        startPosition = transform.position;
    }
    

    void FixedUpdate()
    {
        Vector2 ballPos = ball.transform.position - transform.position;
        if (Mathf.Abs((ballPos.y-transform.position.y))>aiDeadZone)
        {
            moveDirection = (ball.transform.position - transform.position);           
            paddleBody.velocity = new Vector2(paddleBody.velocity.x, moveDirection.normalized.y*speed);
        }
        
    }

    public void Reset()
    {
        paddleBody.velocity = Vector2.zero;
        transform.position = startPosition;
    }
}
