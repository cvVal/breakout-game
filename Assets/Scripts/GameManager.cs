using UnityEngine;

public class GameManager : MonoBehaviour
{
    int score = 0;

    public void AddScore(int points)
    {
        score += points;
    }
}
