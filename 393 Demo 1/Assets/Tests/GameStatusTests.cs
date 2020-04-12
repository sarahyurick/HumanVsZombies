using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameStatusTests
{
    [Test]
    public void PCT1_PressingWMovesPlayerUp()
    {
        // Pressing W with speed 2 should change y position by +2
        var speed = 2;
        Assert.AreEqual(2, new Movement(speed).Calculate(0, 1, 1).y, 0.1f);
    }

    // public void GST1_ZombieKillIncreasesPlayerScore()

    // public void GST2_GameplayTimeIncreasesPlayerScore()

    // public void GST3_NewWaveSpawnsNewWeaponOnMap()

    // public void GST4_GameOverMenuAppearsWhenPlayerHealthReachesZero()

    // public void GST4_HighscoresUpdatedWhenPlayerHealthReachesZero()
    // current score and internal timer reset to zero?
}
