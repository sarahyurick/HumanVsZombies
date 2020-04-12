using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ZombieTests
{
    [Test]
    public void PCT1_PressingWMovesPlayerUp()
    {
        // Pressing W with speed 2 should change y position by +2
        var speed = 2;
        Assert.AreEqual(2, new Movement(speed).Calculate(0, 1, 1).y, 0.1f);
    }

    // public void ZCT1_ZombiesSpawnWithNewGame()

    // public void ZCT2_ZombiesSpawnWithNewWave()

    // public void ZCT3_ZombiesMove()

    // public void ZCT4_ZombiesDiesWhenHealthReachesZero()
}
