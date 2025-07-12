using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int score = 0;
    [SerializeField] int lives = 3;
    [SerializeField] BallController ballController;
    [SerializeField] PlayerController playerController;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject gameOverPanel;

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score.ToString();
    }

    public void RemoveLife()
    {
        lives--;
        livesText.text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            // Handle game over
            Debug.Log("Game Over");
            GameOver();
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

    public void WinGame()
    {
        winPanel.SetActive(true);
        playerController.isAlive = false; // Set player to not alive
        ballController.StopBall(); // Stop the ball
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
        playerController.isAlive = false; // Set player to not alive
        ballController.StopBall(); // Stop the ball
    }

    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
