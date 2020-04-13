using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public string currentScreen;
    public string previousScreen;

    public UIManager(string currentScreen)
    {
        this.currentScreen = currentScreen;
    }

    public bool ClickPlayButton()
    {
        if(currentScreen == "MainMenu")
        {
            Transition("IntroVideo");
            return true;
        }
        return false;
    }

    public bool GoToGameplay()
    {
        if(currentScreen == "IntroVideo")
        {
            Transition("Gameplay");
            return true;
        }
        return false;
    }

    public bool ClickSettings()
    {
        if(currentScreen == "MainMenu")
        {
            Transition("SettingsMenu");
            return true;
        }
        if(currentScreen == "PauseMenu")
        {
            Transition("SettingsMenu");
            return true;
        }
        return false;
    }

    public bool ToggleSound()
    {
        if(currentScreen == "SettingsMenu")
        {
            MusicClass mc = new MusicClass();
            mc.ToggleSound();
            return true;
        }
        return false;
    }

    public bool ToggleMusic()
    {
        if (currentScreen == "SettingsMenu")
        {
            MusicClass mc = new MusicClass();
            mc.ToggleMusic();
            return true;
        }
        return false;
    }

    public bool ClickHighscores()
    {
        if(currentScreen == "MainMenu")
        {
            Transition("Highscores");
            return true;
        }
        return false;
    }

    public bool ClickBack()
    {
        if(currentScreen == "Highscores")
        {
            Transition("MainMenu");
            return true;
        }
        if(currentScreen == "SettingsMenu")
        {
            Transition(previousScreen);
            return true;
        }
        return false;
    }

    public bool ClickReset()
    {
        if(currentScreen == "Highscores")
        {
            return true;
        }
        return false;
    }

    public bool FirstPauseClick()
    {
        if(currentScreen == "Gameplay")
        {
            Transition("PauseMenu");
            return true;
        } else
        {
            Transition("Gameplay");
            return false;
        }
    }

    public bool ClickQuitToMainMenu()
    {
        if (currentScreen == "PauseMenu")
        {
            Transition("MainMenu");
            return true;
        }
        return false;
    }

    public bool ClickToReplay()
    {
        if(currentScreen == "GameOverMenu")
        {
            Transition("MainMenu");
            return true;
        }
        return false;
    }

    public bool ClickToQuit()
    {
        if(currentScreen == "GameOverMenu" || currentScreen == "MainMenu")
        {
            currentScreen = null;
            previousScreen = null;
            return true;
        }
        return false;
    }

    public void Transition(string newScreen)
    {
        previousScreen = currentScreen;
        currentScreen = newScreen;
    }
}
