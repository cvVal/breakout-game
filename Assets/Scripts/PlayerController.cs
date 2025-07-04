using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] bool isPlayer1 = true;

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
        if (isPlayer1)
        {
            Player1Input();
        }
        else
        {
            Player2Input();
        }
        myRigidbody2D.linearVelocity = new Vector2(0, direction) * speed;
    }

    void Player1Input()
    {
        // Check for W key using new Input System
        if (Keyboard.current.wKey.isPressed)
        {
            direction = 1f;
        }
        // Check for S key using new Input System
        else if (Keyboard.current.sKey.isPressed)
        {
            direction = -1f;
        }
        // Stop movement when no keys are pressed
        else
        {
            direction = 0f;
        }
    }

    void Player2Input()
    {
        // Check for Up Arrow key using new Input System
        if (Keyboard.current.upArrowKey.isPressed)
        {
            direction = 1f;
        }
        // Check for Down Arrow key using new Input System
        else if (Keyboard.current.downArrowKey.isPressed)
        {
            direction = -1f;
        }
        // Stop movement when no keys are pressed
        else
        {
            direction = 0f;
        }
    }
}