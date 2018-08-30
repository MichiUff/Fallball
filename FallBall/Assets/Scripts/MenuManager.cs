using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private static Transform t;

    public static bool Paused = false;

    void Start()
    {
        t = transform;
        ScoreManager.Instance.OnScoreUpdate += Instance_OnScoreUpdate;
    }

    private void Instance_OnScoreUpdate(int score)
    {
        t.Find("panelGameOver").Find("panelMenue").Find("Score").GetComponent<Text>().text = "Score " + score.ToString();
    }

    public void btnPlay_Click()
    {
        SceneManager.LoadScene("GameScene");
        Paused = false;
    }

    public void btnPause_Click(GameObject menu)
    {
        menu.SetActive(true);
        Paused = true;
    }

    public void btnContinue_Click(GameObject menu)
    {
        menu.SetActive(false);
        Paused = false;
    }
    
    public void btnRestart_Click()
    {
        Paused = false;
        SceneManager.LoadScene("GameScene");
    }

    public void btnMainMenu_Click()
    {
        Paused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public static void GameoverScreen()
    {
        t.Find("panelGameOver").gameObject.SetActive(true);
    }

    void OnDestroy()
    {
        ScoreManager.Instance.OnScoreUpdate -= Instance_OnScoreUpdate;
    }
}
