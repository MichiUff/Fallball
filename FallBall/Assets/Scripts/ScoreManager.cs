using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager Instance;
    
    private int currentScore;
    public int StartScore = 0;

    public int CurrentScore
    {
        get
        {
            return currentScore;
        }
        set
        {
            currentScore = value;
            OnScoreUpdated();
        }
    }

    // Use this for initialization
    void Start()
    {
        Instance = this;
        currentScore = StartScore;
        OnScoreUpdated();
    }

    public delegate void UpdateScore(int score);
    public event UpdateScore OnScoreUpdate;

    void OnScoreUpdated()
    {
        if (OnScoreUpdate != null)
            OnScoreUpdate(currentScore);
    }
}
