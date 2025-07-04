using UnityEngine;

public class GameManager : MonoBehaviour
{
    int score = 0;
    [SerializeField] int lives = 3;

    public void AddScore(int points)
    {
        score += points;
    }

    public void RemoveLife()
    {
        lives--;
        if (lives <= 0)
        {
            // Handle game over
            Debug.Log("Game Over");
        }
        else
        {
            Debug.Log("Lives remaining: " + lives);
        }
    }
}
