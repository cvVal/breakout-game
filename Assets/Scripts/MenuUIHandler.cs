using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUIHandler : MonoBehaviour
{
    [Header("Score Display UI")]
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI lastScoreText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadAndDisplayScores();
    }
    
    /// <summary>
    /// Called when the scene becomes active (including returning from game scene)
    /// </summary>
    void OnEnable()
    {
        LoadAndDisplayScores();
    }

    /// <summary>
    /// Loads saved score data and updates the UI display
    /// </summary>
    void LoadAndDisplayScores()
    {
        // Load saved data from PlayerPrefs
        GameData.LoadData();
        
        // Update Best Score UI
        if (bestScoreText != null)
        {
            bestScoreText.text = "BEST SCORE: " + FormatScore(GameData.BestScore);
        }
        else
        {
            Debug.LogWarning("Best Score Text component not assigned in MenuUIHandler!");
        }
        
        // Update Last Score UI
        if (lastScoreText != null)
        {
            if (GameData.LastScore > 0)
            {
                lastScoreText.text = "LAST SCORE: " + FormatScore(GameData.LastScore);
            }
            else
            {
                lastScoreText.text = "LAST SCORE: --";
            }
        }
        else
        {
            Debug.LogWarning("Last Score Text component not assigned in MenuUIHandler!");
        }
        
        // Debug information
        Debug.Log($"Loaded scores - Best: {GameData.BestScore}, Last: {GameData.LastScore}");
    }
    
    /// <summary>
    /// Formats score numbers with proper formatting (e.g., 1,420 instead of 1420)
    /// </summary>
    /// <param name="score">The score to format</param>
    /// <returns>Formatted score string</returns>
    string FormatScore(int score)
    {
        if (score == 0)
        {
            return "0";
        }
        return score.ToString("N0"); // Adds thousand separators
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    /// <summary>
    /// Public method to refresh the score display (useful when returning from game)
    /// </summary>
    public void RefreshScoreDisplay()
    {
        LoadAndDisplayScores();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
