using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody2D myRigidbody2D;
    Vector2 initialPosition;

    [SerializeField] float speed = 5f;
    [SerializeField] GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the Rigidbody2D component attached to this GameObject
        myRigidbody2D = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        LaunchBall();
    }

    // FixedUpdate is called at a fixed interval and is used for physics calculations
    private void FixedUpdate()
    {
        /* 
            Ensure the ball's velocity is normalized to maintain a consistent speed.
            The linearVelocity property represents the object's current velocity as a Vector2, 
            which contains both direction and magnitude (speed).
            The key operation here is .normalized, which converts the velocity vector into a unit vector 
            - meaning it maintains the same direction but reduces the magnitude to exactly 1. 
            This effectively separates the direction from the speed.
        */
        myRigidbody2D.linearVelocity = myRigidbody2D.linearVelocity.normalized * speed;
    }

    // Update is called once per frame
    public void LaunchBall()
    {
        // Reset ball position to center
        transform.position = initialPosition;

        float x = 0;

        // Randomly choose between 0 and 3
        int randomDirection = Random.Range(0, 2);

        /*
                       Y+
                       ↑
                 (-1,1)│(1,1)
                    ↖  │  ↗
                X- ────┼──── X+
                    ↙  │  ↘
                (-1,-1)│(1,-1)
                       ↓
                       Y-
        */
        if (randomDirection == 0)
        {
            x = 1f;
        }
        else if (randomDirection == 1)
        {
            x = -1f;
        }

        // Ensure the Rigidbody2D component is assigned
        // Initialize the ball's velocity to move to the right
        myRigidbody2D.linearVelocity = new Vector2(x, 1f) * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is the KillBox
        if (collision.gameObject.CompareTag("KillBox"))
        {
            gameManager.RemoveLife();
        }
    }

    public void StopBall()
    {
        Destroy(gameObject); // Destroy the ball
    }
}
