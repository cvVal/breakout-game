using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] int health = 1;
    [SerializeField] int points = 100;

    SpriteRenderer spriteRenderer;
    GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindFirstObjectByType<GameManager>();
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
            // Decrease the health of the block
            health--;
            gameManager.AddScore(points); // Add score to the game manager

            switch (health)
            {
                case 3:
                    spriteRenderer.color = Color.red; // Full health
                    break;
                case 2:
                    spriteRenderer.color = Color.green; // Half health
                    break;
                case 1:
                    spriteRenderer.color = Color.yellow; // Low health
                    break;
                default:
                    Destroy(gameObject); // No health left
                    /* 
                    Optionally, you can add a delay before destroying the block by passing the unit of time in seconds
                    Destroy(gameObject, 2f);
                    */
                    break;
            }
        }
    }
}
