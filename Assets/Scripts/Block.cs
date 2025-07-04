using UnityEngine;

public class Block : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is the ball
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Destroy this block
            Destroy(gameObject);

            // Optionally, you can add a delay before destroying the block by passing the unit of time in seconds
            // Destroy(gameObject, 2f);
        }
    }
}
