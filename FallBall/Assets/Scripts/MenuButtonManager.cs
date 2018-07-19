using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour
{
    public static bool Paused = false;

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
        throw new NotImplementedException();
        Paused = false;
    }

    public void btnMainMenu_Click()
    {
        SceneManager.LoadScene("MainMenu");
        Paused = false;
    }
}
