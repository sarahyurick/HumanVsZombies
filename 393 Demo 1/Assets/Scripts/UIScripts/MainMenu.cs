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
        // if(uim.ClickPlayButton())
        {
            SceneManager.LoadScene("IntroVideo");
        }
    }

    public void CheckAudio()
    {
        if (PlayerPrefs.GetInt("Sound", 0) != 0 && PlayerPrefs.GetInt("Sound", 0) != 1)
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        if (PlayerPrefs.GetInt("Music", 0) != 0 && PlayerPrefs.GetInt("Music", 0) != 1)
        {
            PlayerPrefs.SetInt("Music", 0);
        }
    }

    public void Exit ()
    {
        // if(uim.ClickToQuit())
        {
            Application.Quit();
        }
    }
}
