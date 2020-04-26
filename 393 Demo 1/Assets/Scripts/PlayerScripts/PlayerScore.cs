using UnityEngine;
using UnityEngine.UI;

// Used https://www.youtube.com/watch?v=vZU51tbgMXk
// and https://www.youtube.com/watch?v=TAGZxRMloyU

public class PlayerScore : MonoBehaviour
{
    public static GameManager gm;
    public GameStatus gs = new GameStatus();

    public int playerScore;
    public int waveCount;

    public Text score;
    public Text wave;

    public int WAVE2_TRIGGER = 30;
    public int WAVE3_TRIGGER = 100;
    public int WAVE4_TRIGGER = 200;
    public bool shouldTriggerNewWave = false;
    public bool timerTriggered = false;

    int currentKills = 0;

    private float startTime;
    private int TIME_PER_SCOREINCREASE = 20;

    void Start()
    {
        PlayerPrefs.SetInt("KillCount", 0);

        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        }

        startTime = Time.time;
        playerScore = 0;
        waveCount = 0;

    }

    void Update()
    {
        int t = (int)(Time.time - startTime);

        if ((t + 1) % TIME_PER_SCOREINCREASE == 0)
        {
            IncreaseScore(10);
            timerTriggered = true;
            startTime = Time.time;
        }

        if (currentKills < PlayerPrefs.GetInt("KillCount", 0))
        {
            IncreaseScore(10);
            currentKills = PlayerPrefs.GetInt("KillCount", 0);
        }

        score.text = "Score:  " + playerScore.ToString();
        int waveText = waveCount + 1;
        wave.text = "Wave:  " + (waveText).ToString();
        // score.text = t.ToString();
        // score.text = player.position.z.ToString("0");
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
        shouldTriggerNewWave = true;
        // gm.TriggerNewWave(waveCount);
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

    public void PauseAndSave()
    {
        UpdateHighScores(playerScore);
    }
}