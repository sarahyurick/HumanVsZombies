using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class UITests
{
    [Test]
    public void PCT1_PressingWMovesPlayerUp()
    {
        // Pressing W with speed 2 should change y position by +2
        var speed = 2;
        Assert.AreEqual(2, new Movement(speed).Calculate(0, 1, 1).y, 0.1f);
    }

    // technically main menu to intro video to gameplay
    // public void UIT1_MainMenuToGameplay()

    // public void UIT2_MainMenuToSettings()

    // public void UIT3_MainMenuToHighscores()

    // public void UIT4_SoundSettings()

    // public void UIT5_MusicSettings()

    // public void UIT6_SettingsToMainMenu()

    // public void UIT6_SettingsToPauseMenu()

    // public void UIT7_HighscoresToMainMenu()

    // public void UIT8_ResettingHighscores()

    // public void UIT9_PausingGame()

    // public void UIT10_PauseMenuToGameplay()

    // public void UIT11_PauseMenuToSettings()

    // public void UIT12_PauseMenuToMainMenu()

    // public void UIT12_QuitFromPauseMenuUpdatesHighscores()

    // public void UIT13_GameOverMenuToMainMenu()

    // public void UIT14_GameOverMenuToQuit()

    // would have to add to documentation
    // public void UIT15_MainMenuToQuit()

    // would have to add to documentation
    // public void UIT16_SkipIntroVideo()
}
