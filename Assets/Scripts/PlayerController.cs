using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D myRigidbody2D;

    [SerializeField] float speed = 3f;

    // Stores direction of movement
    float direction;
    Vector2 initialPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
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
}