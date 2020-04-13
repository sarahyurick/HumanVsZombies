using UnityEngine;

public class GameStatus
{
    public bool firstWave = true;
    public bool finalWave = false;
    public bool gameHasEnded = false;

    public int playerScore = 0;
    public int waveCount = 0;

    public int WAVE2_TRIGGER = 30;
    public int WAVE3_TRIGGER = 100;
    public int WAVE4_TRIGGER = 200;
    public int MAX_WAVES = 3;
    public bool shouldTriggerNewWave = false;
    public bool timerTriggered = false;

    public int currentKills = 0;
    public int zombiesSpawned = 0;
    public int weaponCount = 0;

    public int TIME_PER_SCOREINCREASE = 10;
    public int KILL_REWARD = 10;
    public int TIME_REWARD = 10;

    public void MarkFirstWave()
    {
        firstWave = false;
        PlayerPrefs.SetInt("KillCount", 0);
        zombiesSpawned = NumberOfZombiesToSpawn(waveCount);
        weaponCount++;
    }

    public int NumberOfZombiesToSpawn(int waveCount)
    {
        return 3 + 3 * waveCount;
    }

    public bool IncreaseScoreByTimeIfNecessary(int t)
    {
        if ((t + 1) % TIME_PER_SCOREINCREASE == 0)
        {
            IncreaseScore(TIME_REWARD);
            timerTriggered = true;
        }
        return false;
    }

    public void IncreaseScoreIfZombieWasKilled()
    {
        if (currentKills < PlayerPrefs.GetInt("KillCount", 0))
        {
            IncreaseScore(KILL_REWARD);
            currentKills = PlayerPrefs.GetInt("KillCount", 0);
        }
    }

    public void IncreaseScore(int amount)
    {
        playerScore = playerScore + amount;
        if (playerScore == WAVE2_TRIGGER || playerScore == WAVE3_TRIGGER || playerScore == WAVE4_TRIGGER)
        {
            NextWave();
        }
    }

    public void NextWave()
    {
        if(waveCount < MAX_WAVES)
        {
            waveCount++;
            zombiesSpawned += NumberOfZombiesToSpawn(waveCount);
            weaponCount++;
            shouldTriggerNewWave = true;
        }
    }

    public void UpdateHighScores(int number)
    {
        if (number > PlayerPrefs.GetInt("FirstPlace", 0))
        {
            int newThird = PlayerPrefs.GetInt("SecondPlace", 0);
            PlayerPrefs.SetInt("ThirdPlace", newThird);

            int newSecond = PlayerPrefs.GetInt("FirstPlace", 0);
            PlayerPrefs.SetInt("SecondPlace", newSecond);

            PlayerPrefs.SetInt("FirstPlace", number);
        }
        else if (number > PlayerPrefs.GetInt("SecondPlace", 0))
        {
            int newThird = PlayerPrefs.GetInt("SecondPlace", 0);
            PlayerPrefs.SetInt("ThirdPlace", newThird);

            PlayerPrefs.SetInt("SecondPlace", number);
        }
        else if (number > PlayerPrefs.GetInt("ThirdPlace", 0))
        {
            PlayerPrefs.SetInt("ThirdPlace", number);
        }

    }

    /*
    public void EndGameplay()
    {
        if (gameHasEnded == false)
        {
            UpdateHighScores();
            PlayerPrefs.SetInt("KillCount", 0);
            gameHasEnded = true;
        }
    } */
}
