using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D myRigidbody2D;

    [SerializeField] float speed = 3f;
    [SerializeField] AudioClip paddleHitSound; // Sound to play when the paddle hits the ball

    // Stores direction of movement
    float direction;
    Vector2 initialPosition;
    public bool isAlive = true;
    AudioSource audioSource;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Handle player input for movement
        if (isAlive)
            HandleInput();

    }

    // FixedUpdate is called at a fixed interval and is used for physics calculations
    private void FixedUpdate()
    {
        // Apply the movement to the Rigidbody2D
        myRigidbody2D.linearVelocity = new Vector2(direction * speed, 0);
    }

    void HandleInput()
    {
        // Check for Left Arrow key or A key
        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
        {
            direction = -1f;
        }
        // Check for Right Arrow key or D key
        else if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            direction = 1f;
        }
        // Stop movement when no keys are pressed
        else
        {
            direction = 0f;
        }
    }

    public void ResetPlayer()
    {
        transform.position = initialPosition;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is the ball
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Play paddle hit sound
            if (paddleHitSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(paddleHitSound);
            }
        }
    }
}