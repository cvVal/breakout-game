
using UnityEngine;

public static class GameData
{
    // PlayerPrefs keys for data persistence
    private const string BEST_SCORE_KEY = "BestScore";
    private const string LAST_SCORE_KEY = "LastScore";

    public static int BestScore { get; private set; }
    public static int LastScore { get; private set; }

    /// <summary>
    /// Loads saved data from PlayerPrefs
    /// </summary>
    public static void LoadData()
    {
        BestScore = PlayerPrefs.GetInt(BEST_SCORE_KEY, 0);
        LastScore = PlayerPrefs.GetInt(LAST_SCORE_KEY, 0);

        Debug.Log($"GameData loaded - Best: {BestScore}, Last: {LastScore}");
    }

    /// <summary>
    /// Saves current data to PlayerPrefs
    /// </summary>
    public static void SaveData()
    {
        PlayerPrefs.SetInt(BEST_SCORE_KEY, BestScore);
        PlayerPrefs.SetInt(LAST_SCORE_KEY, LastScore);
        PlayerPrefs.Save(); // Force save to disk

        Debug.Log($"GameData saved - Best: {BestScore}, Last: {LastScore}");
    }

    /// <summary>
    /// Sets the last game score and updates best score if needed
    /// </summary>
    /// <param name="score">The final score from the last game</param>
    public static void SetLastScore(int score)
    {
        LastScore = score;
        SetBestScore(score); // Also check if it's a new best score
        SaveData(); // Automatically save when data changes

        Debug.Log($"New game completed - Score: {score}, New Best: {BestScore}");
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
            BestScore = score;
            Debug.Log($"NEW BEST SCORE! {oldBest} → {BestScore}");
        }
    }

    /// <summary>
    /// Clears all saved data (useful for testing or reset functionality)
    /// </summary>
    public static void ClearAllData()
    {
        PlayerPrefs.DeleteKey(BEST_SCORE_KEY);
        PlayerPrefs.DeleteKey(LAST_SCORE_KEY);
        PlayerPrefs.Save();

        BestScore = 0;
        LastScore = 0;

        Debug.Log("All game data cleared!");
    }
}
