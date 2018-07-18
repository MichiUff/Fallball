using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour {

	public void btnPlay_Click()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void btnPause_Click(GameObject menu)
    {
        menu.SetActive(true);
    }

    public void btnContinue_Click(GameObject menu)
    {
        menu.SetActive(false);
    }
    
    public void btnRestart_Click()
    {
        throw new NotImplementedException();
    }

    public void btnMainMenu_Click()
    {
        SceneManager.LoadScene("MainMenu");

    }
}
