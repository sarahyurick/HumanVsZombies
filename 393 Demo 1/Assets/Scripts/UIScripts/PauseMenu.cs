using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour

// Used https://www.youtube.com/watch?v=JivuXdrIHK0
{
    public static UIManager uim = new UIManager("Gameplay");

    public GameObject pauseMenuUI;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeOrPause();
        }
    }

    public void ResumeOrPause ()
    {
        if (uim.FirstPauseClick()) {
            // Game is paused
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
        } else
        {
            // Game resumes
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void LoadMainMenu()
    {
        if(uim.ClickQuitToMainMenu())
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
