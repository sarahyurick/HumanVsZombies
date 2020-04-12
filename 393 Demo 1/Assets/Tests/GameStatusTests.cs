using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameStatusTests
{
    [Test]
    public void GST1_ZombieKillIncreasesPlayerScore()
    {
        GameStatus gs = new GameStatus();
        int startingScore = gs.playerScore;
        int currentKillCount = PlayerPrefs.GetInt("KillCount", 0);
        // Increase kill count
        PlayerPrefs.SetInt("KillCount", currentKillCount+1);
        gs.IncreaseScoreIfZombieWasKilled();
        Assert.AreEqual(startingScore + gs.KILL_REWARD, gs.playerScore);
    }

    [Test]
    public void GST2_GameplayTimeIncreasesPlayerScore()
    {
        GameStatus gs = new GameStatus();
        int startingScore = gs.playerScore;
        gs.IncreaseScoreByTimeIfNecessary(gs.TIME_PER_SCOREINCREASE-1, 1.0f);
        Assert.AreEqual(startingScore + gs.TIME_REWARD, gs.playerScore);
    }

    [Test]
    public void GST3_NewWaveSpawnsNewWeaponOnMap()
    {
        GameStatus gs = new GameStatus();
        int weaponCount = gs.weaponCount;
        gs.MarkFirstWave(); // spawns laser
        Assert.AreEqual(weaponCount + 1, gs.weaponCount);
        gs.NextWave(); // spawns flamethrower
        Assert.AreEqual(weaponCount + 2, gs.weaponCount);
        gs.NextWave(); // spawns gun
        Assert.AreEqual(weaponCount + 3, gs.weaponCount);
        gs.NextWave(); // spawns boomerang
        Assert.AreEqual(weaponCount + 4, gs.weaponCount);
        int waveCount = gs.waveCount;
        gs.NextWave(); // ineffective; does not change wave count or spawn new weapon
        Assert.AreEqual(waveCount, gs.waveCount);
        Assert.AreEqual(weaponCount + 4, gs.weaponCount);
    }

    [Test]
    public void GST4_GameOverMenuAppearsWhenPlayerHealthReachesZero()
    {
        Player player = new Player();
        Assert.AreEqual("Gameplay", player.uim.currentScreen);
        for(int i = 0; i < player.MAX_HEALTH; i++)
        {
            player.UpdatePlayerHealth();
        }
        if(player.IsDead())
        {
            Assert.AreEqual("GameOverMenu", player.uim.currentScreen);
        } else
        {
            Assert.AreEqual("Gameplay", player.uim.currentScreen);
        }
    }

    [Test]
    public void GST4_HighscoresUpdateProperly()
    {
        GameStatus gs = new GameStatus();
        PlayerPrefs.SetInt("FirstPlace", 30);
        PlayerPrefs.SetInt("SecondPlace", 20);
        PlayerPrefs.SetInt("ThirdPlace", 10);
        // Player's score beats the previous first place
        gs.playerScore = 40;
        gs.UpdateHighScores();
        Assert.AreEqual(PlayerPrefs.GetInt("FirstPlace", 0), 40);
        Assert.AreEqual(PlayerPrefs.GetInt("SecondPlace", 0), 30);
        Assert.AreEqual(PlayerPrefs.GetInt("ThirdPlace", 0), 20);

        // Player's score beats the previous second place
        gs.playerScore = 35;
        gs.UpdateHighScores();
        Assert.AreEqual(PlayerPrefs.GetInt("FirstPlace", 0), 40);
        Assert.AreEqual(PlayerPrefs.GetInt("SecondPlace", 0), 35);
        Assert.AreEqual(PlayerPrefs.GetInt("ThirdPlace", 0), 30);

        // Player's score beats the previous third place
        gs.playerScore = 32;
        gs.UpdateHighScores();
        Assert.AreEqual(PlayerPrefs.GetInt("FirstPlace", 0), 40);
        Assert.AreEqual(PlayerPrefs.GetInt("SecondPlace", 0), 35);
        Assert.AreEqual(PlayerPrefs.GetInt("ThirdPlace", 0), 32);
    }
}
