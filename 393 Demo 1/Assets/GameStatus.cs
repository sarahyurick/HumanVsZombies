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
    public bool shouldTriggerNewWave = false;
    public bool timerTriggered = false;

    public int currentKills = 0;
    public int zombiesSpawned = 0;

    public int TIME_PER_SCOREINCREASE = 30;

    public void MarkFirstWave()
    {
        firstWave = false;
        zombiesSpawned = NumberOfZombiesToSpawn(waveCount);
    }

    public int NumberOfZombiesToSpawn(int waveCount)
    {
        return 3 + 3 * waveCount;
    }

    public void IncreaseScore(int amount)
    {
        playerScore = playerScore + amount;
        if (playerScore == WAVE2_TRIGGER || playerScore == WAVE3_TRIGGER || playerScore == WAVE4_TRIGGER)
        {
            NextWave();
        }
    }

    private void NextWave()
    {
        waveCount++;
        zombiesSpawned += NumberOfZombiesToSpawn(waveCount);
        shouldTriggerNewWave = true;
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
}
