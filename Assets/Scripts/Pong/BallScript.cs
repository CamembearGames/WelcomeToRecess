using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallScript : MonoBehaviour
{
    public PlayerInputActions playerControls;

    [SerializeField] private float speed;
    [SerializeField] private float speedMultiplier = 1.1f;
    [SerializeField] private Rigidbody2D ballBody;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private PongGameManager gameManager;

    [SerializeField] private float maxCollisionAngle = 45f;

    private GameObject lastPadleTouched;

    void Start()
    {
        startPosition = transform.position;
        Launch();
    }

    public void Reset()
    {
        ballBody.velocity = Vector2.zero;
        transform.position = startPosition;
        GetComponentInChildren<TrailRenderer>().Clear();
        Launch();
    }

    private void Launch()
    {
        float x = Random.Range(0,2) == 0 ? -1:1;
        float y = Random.Range(0,2) == 0 ? -1:1;

        ballBody.velocity = new Vector2(speed*x, speed*y);

    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Paddle"))
        {
            if (other.gameObject != lastPadleTouched)
            {
                gameManager.AddPass();
            }
            

            ballBody.velocity *= speedMultiplier;
            lastPadleTouched = other.gameObject;

            AdjustAngle(other.collider.GetComponent<PlayerPaddle>(), other);
        }
        
    }

    private void AdjustAngle(PlayerPaddle paddle, Collision2D collision)
    {
        Vector2 median = Vector2.zero;
        foreach (ContactPoint2D point in collision.contacts)
        {
            median += point.point;
        }
        median /= collision.contactCount;

        float absoluteDistanceFromCenter = median.y - paddle.transform.position.y;
        float relativeDistanceFromCenter = absoluteDistanceFromCenter *  2 / paddle.GetHeight();

        int angleSign = paddle.IsComputer() ? 1 : -1;
        Quaternion rot = Quaternion.AngleAxis(relativeDistanceFromCenter * maxCollisionAngle * angleSign, Vector3.forward);

        Vector2 dir = paddle.IsComputer()? Vector2.right : Vector2.left;
        Vector2 velocity = rot * dir * ballBody.velocity.magnitude;
        ballBody.velocity = velocity;
    }

}
