using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int score = 0;
    [SerializeField] int lives = 3;
    [SerializeField] BallController ballController;
    [SerializeField] PlayerController playerController;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject gameOverPanel;

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }

    public void RemoveLife()
    {
        lives--;
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
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
