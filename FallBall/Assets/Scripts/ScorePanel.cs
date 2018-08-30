using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{
    private Text textComponent;

    // Use this for initialization
    void Start()
    {
        ScoreManager.Instance.OnScoreUpdate += Instance_OnScoreUpdate;
        textComponent = transform.GetComponent<Text>();
        textComponent.text = ScoreManager.Instance.CurrentScore.ToString();
    }

    private void Instance_OnScoreUpdate(int score)
    {
        textComponent.text = score.ToString();
    }

    void OnDestroy()
    {
        ScoreManager.Instance.OnScoreUpdate -= Instance_OnScoreUpdate;
    }
}
