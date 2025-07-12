using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] int health = 1;
    [SerializeField] int points = 100;
    [SerializeField] bool isBonusBlock = false; // Mark this as a bonus block
    [SerializeField] int bonusPoints = 20; // Extra points for bonus blocks
    [SerializeField] AudioClip hitSound; // Sound to play on hit

    SpriteRenderer spriteRenderer;
    GameManager gameManager;
    AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Early exit if no collision object
        if (collision == null || collision.gameObject == null) return;
        
        // Check if the collided object is the ball
        if (collision.gameObject.CompareTag("Ball"))
        {
            ProcessBallHit();
        }
    }
    
    private void ProcessBallHit()
    {
        // Null safety check for game manager
        if (gameManager == null)
        {
            Debug.LogWarning("GameManager not found! Cannot process score.");
            return;
        }
        
        // Play sound
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
        
        // Always award regular points for hitting the block
        gameManager.AddScore(points);
        
        // Award bonus points for bonus blocks on every hit
        if (isBonusBlock)
        {
            gameManager.AddScore(bonusPoints);
        }
        
        // Decrease the health of the block
        health--;
        
        // Update visual appearance and handle destruction
        UpdateBlockAppearance();
        
        // Check for destruction
        if (health <= 0)
        {
            // Add a small delay to let the sound play before destroying
            Invoke(nameof(HandleBlockDestruction), 0.1f);
        }
    }
    
    private void UpdateBlockAppearance()
    {
        if (spriteRenderer == null) return;
        
        switch (health)
        {
            case 3:
                spriteRenderer.color = Color.red; // High health
                break;
            case 2:
                spriteRenderer.color = Color.green; // Medium health
                break;
            case 1:
                spriteRenderer.color = Color.yellow; // Low health
                break;
            default:
                // For bonus blocks with health > 3, gradually change color
                if (isBonusBlock && health > 3)
                {
                    float healthRatio = (float)health / 10f;
                    // Transition from cyan (full health) to magenta (low health)
                    spriteRenderer.color = Color.Lerp(Color.magenta, Color.cyan, healthRatio);
                }
                break;
        }
    }
    
    private void HandleBlockDestruction()
    {
        // Only check for win condition if this is a regular block
        if (!isBonusBlock)
        {
            int remainingBlocks = CountRemainingRegularBlocks();
            Debug.Log($"Regular block destroyed. Remaining blocks: {remainingBlocks}");
            
            if (remainingBlocks <= 0)
            {
                if (gameManager != null)
                {
                    gameManager.WinGame();
                }
                else
                {
                    Debug.LogWarning("Cannot trigger win condition - GameManager not found!");
                }
            }
        }
        else
        {
            Debug.Log($"Bonus block destroyed after {10 - health} hits!");
        }
        
        // Destroy the block
        Destroy(gameObject);
    }

    // Counts how many regular (non-bonus) blocks remain in the scene
    // Number of regular blocks remaining
    private int CountRemainingRegularBlocks()
    {
        try
        {
            Block[] allBlocks = FindObjectsByType<Block>(FindObjectsSortMode.None);
            int regularBlockCount = 0;
            
            foreach (Block block in allBlocks)
            {
                // Count only regular blocks (not bonus blocks) and exclude this block if it's being destroyed
                if (block != null && !block.isBonusBlock && block != this)
                {
                    regularBlockCount++;
                }
            }
            
            return regularBlockCount;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error counting remaining blocks: {e.Message}");
            return 1; // Return 1 to prevent accidental win condition
        }
    }
}
