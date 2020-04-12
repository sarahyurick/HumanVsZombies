using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    UIManager uim = new UIManager("GameOverMenu");

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReplayToMainMenu()
    {
        if(uim.ClickToReplay())
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void ExitGame()
    {
        if(uim.ClickToQuit())
        {
            Application.Quit();
        }
    }
}
