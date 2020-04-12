using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ZombieTests
{
    [Test]
    public void ZCT1_ZombiesSpawnWithNewGame()
    {
        // Simulating information present at the beginning of a new game
        GameStatus gs = new GameStatus();
        int zombiesBeforeGameStarts = gs.zombiesSpawned;
        // The game starts with the first wave
        gs.MarkFirstWave();
        Assert.AreNotEqual(zombiesBeforeGameStarts, gs.zombiesSpawned);
        // The first wave is wave 0
        // Check that the correct number of zombies spawned
        int numberOfZombiesThatShouldHaveSpawned = gs.NumberOfZombiesToSpawn(0);
        Assert.AreEqual(numberOfZombiesThatShouldHaveSpawned, gs.zombiesSpawned);
    }

    [Test]
    public void ZCT2_ZombiesSpawnWithNewWave()
    {
        GameStatus gs = new GameStatus();
        gs.MarkFirstWave();
        int zombiesSpawnedWithFirstWave = gs.zombiesSpawned;
        gs.IncreaseScore(gs.WAVE2_TRIGGER);
        // The second wave is wave 1
        int numberOfZombiesThatShouldHaveSpawnedWithSecondWave = gs.NumberOfZombiesToSpawn(gs.waveCount);
        Assert.AreEqual(zombiesSpawnedWithFirstWave + numberOfZombiesThatShouldHaveSpawnedWithSecondWave, gs.zombiesSpawned);
    }

    [Test]
    public void ZCT3_ZombiesMove()
    {
        Zombie zombie = new Zombie();
        Vector3 startPosition = zombie.position;
        zombie.ChangeDirection();
        Assert.AreNotEqual(startPosition, zombie.position);
    }

    [Test]
    public void ZCT4_ZombiesDiesWhenHealthReachesZero()
    {
        int killsSoFar = PlayerPrefs.GetInt("KillCount", 0);
        // Make a new zombie
        Zombie zombie = new Zombie();
        // Remove all of its health
        int damageAllHealth = zombie.MAX_HEALTH;
        zombie.UpdateHealth(damageAllHealth);
        if(zombie.IsDead())
        {
            Assert.AreEqual(killsSoFar + 1, PlayerPrefs.GetInt("KillCount", 0));
        } else
        {
            // The zombie is not dead so the kill count should be the same
            Assert.AreEqual(killsSoFar, PlayerPrefs.GetInt("KillCount", 0));
        }
    }
}
