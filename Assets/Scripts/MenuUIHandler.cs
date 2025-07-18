using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUIHandler : MonoBehaviour
{
    [Header("Score Display UI")]
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI lastScoreText;
    
    [Header("Player Input")]
    [SerializeField] TMP_InputField playerNameInput;
    
    // Placeholder text constant
    private const string PLACEHOLDER_TEXT = "Enter your name...";
    
    // Player name constraints
    private const int MAX_NAME_LENGTH = 15;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadAndDisplayScores();
        LoadPlayerName();
        UpdatePlaceholder();
        SetupInputFieldEvents();
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
            if (GameData.BestScore > 0)
            {
                bestScoreText.text = $"BEST SCORE: {GameData.BestScorePlayer} {FormatScore(GameData.BestScore)}";
            }
            else
            {
                bestScoreText.text = "BEST SCORE: --";
            }
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
                lastScoreText.text = $"LAST SCORE: {GameData.LastScorePlayer} {FormatScore(GameData.LastScore)}";
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
        Debug.Log($"Loaded scores - Best: {GameData.BestScore} ({GameData.BestScorePlayer}), Last: {GameData.LastScore} ({GameData.LastScorePlayer})");
    }
    
    /// <summary>
    /// Loads the current player name into the input field
    /// </summary>
    void LoadPlayerName()
    {
        if (playerNameInput != null)
        {
            // Load the actual player name (empty if no name set, which will show placeholder)
            playerNameInput.text = GameData.CurrentPlayer;
            
            Debug.Log($"Loaded player name: '{GameData.CurrentPlayer}' (empty = shows placeholder)");
        }
        else
        {
            Debug.LogWarning("Player Name Input Field not assigned in MenuUIHandler!");
        }
    }
    
    /// <summary>
    /// Sets up input field event listeners and constraints
    /// </summary>
    void SetupInputFieldEvents()
    {
        if (playerNameInput != null)
        {
            // Set character limit
            playerNameInput.characterLimit = MAX_NAME_LENGTH;
            
            // Save player name when they finish editing (press Enter or click away)
            playerNameInput.onEndEdit.AddListener(OnPlayerNameChanged);
            
            // Add focus events to handle placeholder behavior
            playerNameInput.onSelect.AddListener(OnInputFieldSelected);
            playerNameInput.onDeselect.AddListener(OnInputFieldDeselected);
        }
    }
    
    /// <summary>
    /// Called when player finishes editing their name
    /// </summary>
    /// <param name="newName">The new player name</param>
    void OnPlayerNameChanged(string newName)
    {
        // Trim whitespace - allow empty names for anonymous play
        string trimmedName = newName.Trim();
        
        GameData.SetCurrentPlayer(trimmedName);
        Debug.Log($"Player name changed to: '{GameData.CurrentPlayer}' (length: {trimmedName.Length}, anonymous: {string.IsNullOrEmpty(trimmedName)})");
    }
    
    /// <summary>
    /// Called when input field is selected/focused
    /// </summary>
    /// <param name="text">Current text in field</param>
    void OnInputFieldSelected(string text)
    {
        // This is called when the player clicks on the input field
        // The placeholder will automatically hide when typing starts
        Debug.Log("Input field selected");
    }
    
    /// <summary>
    /// Called when input field loses focus
    /// </summary>
    /// <param name="text">Final text in field</param>
    void OnInputFieldDeselected(string text)
    {
        // This is called when the player clicks away from the input field
        // The placeholder will automatically show if the field is empty
        Debug.Log($"Input field deselected with text: '{text}'");
    }
    
    /// <summary>
    /// Clean up event listeners when object is destroyed
    /// </summary>
    void OnDestroy()
    {
        if (playerNameInput != null)
        {
            playerNameInput.onEndEdit.RemoveListener(OnPlayerNameChanged);
            // playerNameInput.onValueChanged.RemoveListener(OnPlayerNameTyping);
            playerNameInput.onSelect.RemoveListener(OnInputFieldSelected);
            playerNameInput.onDeselect.RemoveListener(OnInputFieldDeselected);
        }
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
    
    /// <summary>
    /// Updates the placeholder text and input field appearance
    /// </summary>
    public void UpdatePlaceholder()
    {
        if (playerNameInput != null && playerNameInput.placeholder != null)
        {
            if (playerNameInput.placeholder.TryGetComponent<TextMeshProUGUI>(out var placeholderText))
            {
                placeholderText.text = PLACEHOLDER_TEXT;
                Color placeholderColor = placeholderText.color;
                placeholderColor.a = 0.5f;
                placeholderText.color = placeholderColor;
            }
        }
    }

    public void StartGame()
    {
        // Ensure player name is saved before starting the game
        if (playerNameInput != null)
        {
            GameData.SetCurrentPlayer(playerNameInput.text);
        }
        
        Debug.Log($"Starting game with player: {GameData.CurrentPlayer}");
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
