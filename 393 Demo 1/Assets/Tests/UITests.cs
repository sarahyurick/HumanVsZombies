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

    // public void UIT2_MainMenuToSettings()

    [Test]
    public void UIT3_MainMenuToHighscores()
    {
        UIManager uim = new UIManager("MainMenu");
        uim.ClickHighscores();
        Assert.AreEqual("Highscores", uim.currentScreen);
    }

    // public void UIT4_SoundSettings()

    // public void UIT5_MusicSettings()

    // public void UIT6_SettingsToMainMenu()

    // public void UIT6_SettingsToPauseMenu()

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

    // public void UIT11_PauseMenuToSettings()

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
    // public void UIT15_MainMenuToQuit()
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
