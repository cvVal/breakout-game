using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D myRigidbody2D;

    [SerializeField] float speed = 3f;

    // Stores direction of movement
    float direction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        myRigidbody2D.linearVelocity = new Vector2(direction * speed, 0);
    }

    void HandleInput()
    {
        // Check for Left Arrow key
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            direction = -1f;
        }
        // Check for Right Arrow key
        else if (Keyboard.current.rightArrowKey.isPressed)
        {
            direction = 1f;
        }
        // Stop movement when no keys are pressed
        else
        {
            direction = 0f;
        }
    }
}