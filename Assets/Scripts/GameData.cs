using UnityEngine;

public static class GameData
{
    // PlayerPrefs keys for data persistence
    private const string BEST_SCORE_KEY = "BestScore";
    private const string LAST_SCORE_KEY = "LastScore";
    private const string BEST_SCORE_PLAYER_KEY = "BestScorePlayer";
    private const string LAST_SCORE_PLAYER_KEY = "LastScorePlayer";
    private const string CURRENT_PLAYER_KEY = "CurrentPlayer";
    
    public static int BestScore { get; private set; }
    public static int LastScore { get; private set; }
    public static string BestScorePlayer { get; private set; } = "Anonymous";
    public static string LastScorePlayer { get; private set; } = "Anonymous";
    public static string CurrentPlayer { get; private set; } = "";

    /// <summary>
    /// Loads saved data from PlayerPrefs
    /// </summary>
    public static void LoadData()
    {
        BestScore = PlayerPrefs.GetInt(BEST_SCORE_KEY, 0);
        LastScore = PlayerPrefs.GetInt(LAST_SCORE_KEY, 0);
        BestScorePlayer = PlayerPrefs.GetString(BEST_SCORE_PLAYER_KEY, "Anonymous");
        LastScorePlayer = PlayerPrefs.GetString(LAST_SCORE_PLAYER_KEY, "Anonymous");
        CurrentPlayer = PlayerPrefs.GetString(CURRENT_PLAYER_KEY, ""); // Empty by default

        Debug.Log($"GameData loaded - Best: {BestScore} ({BestScorePlayer}), Last: {LastScore} ({LastScorePlayer})");
    }

    /// <summary>
    /// Saves current data to PlayerPrefs
    /// </summary>
    public static void SaveData()
    {
        PlayerPrefs.SetInt(BEST_SCORE_KEY, BestScore);
        PlayerPrefs.SetInt(LAST_SCORE_KEY, LastScore);
        PlayerPrefs.SetString(BEST_SCORE_PLAYER_KEY, BestScorePlayer);
        PlayerPrefs.SetString(LAST_SCORE_PLAYER_KEY, LastScorePlayer);
        PlayerPrefs.SetString(CURRENT_PLAYER_KEY, CurrentPlayer);
        PlayerPrefs.Save();

        Debug.Log($"GameData saved - Best: {BestScore} ({BestScorePlayer}), Last: {LastScore} ({LastScorePlayer})");
    }

    /// <summary>
    /// Sets the current player name
    /// </summary>
    /// <param name="playerName">The player's name</param>
    public static void SetCurrentPlayer(string playerName)
    {
        // Clean up the name (remove extra spaces, handle empty names)
        string cleanName = string.IsNullOrWhiteSpace(playerName) ? "" : playerName.Trim();
        CurrentPlayer = cleanName;
        SaveData(); // Save immediately when player name changes
        
        Debug.Log($"Current player set to: '{CurrentPlayer}' (empty = use placeholder)");
    }
    
    /// <summary>
    /// Gets the display name for the current player (handles placeholder logic)
    /// </summary>
    /// <returns>Player name or "Anonymous" if empty</returns>
    public static string GetDisplayName()
    {
        return string.IsNullOrWhiteSpace(CurrentPlayer) ? "Anonymous" : CurrentPlayer;
    }
    
    /// <summary>
    /// Sets the last game score and updates best score if needed
    /// </summary>
    /// <param name="score">The final score from the last game</param>
    public static void SetLastScore(int score)
    {
        LastScore = score;
        LastScorePlayer = GetDisplayName(); // Use display name for scores
        SetBestScore(score); // Also check if it's a new best score
        SaveData(); // Automatically save when data changes

        Debug.Log($"New game completed - {GetDisplayName()}: {score}, Best: {BestScore} ({BestScorePlayer})");
    }

    /// <summary>
    /// Updates the best score if the provided score is higher
    /// </summary>
    /// <param name="score">Score to compare against current best</param>
    public static void SetBestScore(int score)
    {
        if (score > BestScore)
        {
            int oldBest = BestScore;
            string oldPlayer = BestScorePlayer;
            BestScore = score;
            BestScorePlayer = GetDisplayName(); // Use display name for scores
            Debug.Log($"NEW BEST SCORE! {oldPlayer}: {oldBest} → {BestScorePlayer}: {BestScore}");
        }
    }

    /// <summary>
    /// Clears all saved data (useful for testing or reset functionality)
    /// </summary>
    public static void ClearAllData()
    {
        PlayerPrefs.DeleteKey(BEST_SCORE_KEY);
        PlayerPrefs.DeleteKey(LAST_SCORE_KEY);
        PlayerPrefs.DeleteKey(BEST_SCORE_PLAYER_KEY);
        PlayerPrefs.DeleteKey(LAST_SCORE_PLAYER_KEY);
        PlayerPrefs.DeleteKey(CURRENT_PLAYER_KEY);
        PlayerPrefs.Save();

        BestScore = 0;
        LastScore = 0;
        BestScorePlayer = "Anonymous";
        LastScorePlayer = "Anonymous";
        CurrentPlayer = ""; // Empty by default

        Debug.Log("All game data cleared!");
    }
}
