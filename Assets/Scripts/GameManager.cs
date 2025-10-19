using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    public static event Action OnScoreUpdate;

    public void UpdateScore(int addedPoints)
    {
        score += addedPoints;
        OnScoreUpdate?.Invoke();
    } 

    public int GetScore()
    {
        return score;
    }
}
