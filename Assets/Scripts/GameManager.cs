using UnityEngine;

public class GameManager : MonoBehaviour
{
    int score = 0;
    [SerializeField] int lives = 3;
    [SerializeField] BallController ballController;
    [SerializeField] PlayerController playerController;

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
            ResetGame();
        }
    }

    void ResetGame()
    {
        ballController.LaunchBall();
        playerController.ResetPlayer();
    }
}
