using UnityEngine;
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

public class UITests
{
    [Test]
    public void UIT1_MainMenuToGameplay()
    {
        UIManager uim = new UIManager("MainMenu");
        uim.ClickPlayButton();
        Assert.AreEqual("IntroVideo", uim.currentScreen);
        uim.GoToGameplay();
        Assert.AreEqual("Gameplay", uim.currentScreen);
    }

    [Test]
    public void UIT2_MainMenuToSettings()
    {
        UIManager uim = new UIManager("MainMenu");
        uim.ClickSettings();
        Assert.AreEqual("SettingsMenu", uim.currentScreen);
    }

    [Test]
    public void UIT3_MainMenuToHighscores()
    {
        UIManager uim = new UIManager("MainMenu");
        uim.ClickHighscores();
        Assert.AreEqual("Highscores", uim.currentScreen);
    }

    [Test]
    public void UIT4_SoundSettings()
    {
        UIManager uim = new UIManager("SettingsMenu");
        int startingSound = PlayerPrefs.GetInt("Sound", 0);
        uim.ToggleSound();
        Assert.AreNotEqual(startingSound, PlayerPrefs.GetInt("Sound", 0));
    }

    [Test]
    public void UIT5_MusicSettings()
    {
        UIManager uim = new UIManager("SettingsMenu");
        int startingMusic = PlayerPrefs.GetInt("Music", 0);
        uim.ToggleMusic();
        Assert.AreNotEqual(startingMusic, PlayerPrefs.GetInt("Music", 0));
    }

    [Test]
    public void UIT6_SettingsToMainMenu()
    {
        UIManager uim = new UIManager("MainMenu");
        uim.ClickSettings();
        uim.ClickBack();
        Assert.AreEqual("MainMenu", uim.currentScreen);
    }

    [Test]
    public void UIT6_SettingsToPauseMenu()
    {
        UIManager uim = new UIManager("PauseMenu");
        uim.ClickSettings();
        uim.ClickBack();
        Assert.AreEqual("PauseMenu", uim.currentScreen);
    }

    [Test]
    public void UIT7_HighscoresToMainMenu()
    {
        UIManager uim = new UIManager("Highscores");
        uim.ClickBack();
        Assert.AreEqual("MainMenu", uim.currentScreen);
    }

    [Test]
    public void UIT8_ResettingHighscores()
    {
        UIManager uim = new UIManager("Highscores");
        bool ResetLeaderboard = uim.ClickReset();
        Assert.IsTrue(ResetLeaderboard);
    }

    [Test]
    public void UIT9_PausingGame()
    {
        UIManager uim = new UIManager("Gameplay");
        uim.FirstPauseClick();
        Assert.AreEqual("PauseMenu", uim.currentScreen);
    }

    [Test]
    public void UIT10_PauseMenuToGameplay()
    {
        UIManager uim = new UIManager("PauseMenu");
        uim.FirstPauseClick();
        Assert.AreEqual("Gameplay", uim.currentScreen);
    }

    [Test]
    public void UIT11_PauseMenuToSettings()
    {
        UIManager uim = new UIManager("PauseMenu");
        uim.ClickSettings();
        Assert.AreEqual("SettingsMenu", uim.currentScreen);
    }

    [Test]
    public void UIT12_PauseMenuToMainMenu()
    {
        UIManager uim = new UIManager("Gameplay");
        // Click to pause
        uim.FirstPauseClick();
        uim.ClickQuitToMainMenu();
        Assert.AreEqual("MainMenu", uim.currentScreen);
    }

    [Test]
    public void UIT13_GameOverMenuToMainMenu()
    {
        UIManager uim = new UIManager("GameOverMenu");
        uim.ClickToReplay();
        Assert.AreEqual("MainMenu", uim.currentScreen);
    }

    [Test]
    public void UIT14_GameOverMenuToQuit()
    {
        // From GameOver menu
        UIManager uim = new UIManager("GameOverMenu");
        uim.ClickToQuit();
        Assert.IsNull(uim.currentScreen);
        // Can also be from Main menu
        uim = new UIManager("MainMenu");
        uim.ClickToQuit();
        Assert.IsNull(uim.currentScreen);
    }
}
