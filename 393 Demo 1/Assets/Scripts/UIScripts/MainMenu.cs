using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static UIManager uim = new UIManager("MainMenu");
    // Used https://www.youtube.com/watch?v=zc8ac_qUXQY

    public void PlayGame ()
    {
        if(uim.ClickPlayButton())
        {
            SceneManager.LoadScene("IntroVideo");
        }
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Exit ()
    {
        if(uim.ClickToQuit())
        {
            Application.Quit();
        }
    }
}
