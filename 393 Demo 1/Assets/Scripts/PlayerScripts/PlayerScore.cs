using UnityEngine;
using UnityEngine.UI;

// Used https://www.youtube.com/watch?v=vZU51tbgMXk
// and https://www.youtube.com/watch?v=TAGZxRMloyU

public class PlayerScore : MonoBehaviour
{
    public static GameManager gm;

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
    /*
    public Text highScore1;
    public Text highScore2;
    public Text highScore3;
    */

    private float startTime;
    private int TIME_PER_SCOREINCREASE = 30;

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
        /*
        highScore1.text = PlayerPrefs.GetInt("FirstPlace", 0).ToString();
        highScore2.text = PlayerPrefs.GetInt("SecondPlace", 0).ToString();
        highScore3.text = PlayerPrefs.GetInt("ThirdPlace", 0).ToString(); */
    }

    void Update()
    {
        int t = (int) (Time.time - startTime);

        if((t+1) % TIME_PER_SCOREINCREASE == 0)
        {
            IncreaseScore(10);
            timerTriggered = true;
            startTime = Time.time;
        }

        if(currentKills < PlayerPrefs.GetInt("KillCount", 0))
        {
            IncreaseScore(10);
            currentKills = PlayerPrefs.GetInt("KillCount", 0);
        }

        score.text = playerScore.ToString();
        int waveText = waveCount + 1;
        wave.text = (waveText).ToString();
        // score.text = t.ToString();
        // score.text = player.position.z.ToString("0");
    }

    public void IncreaseScore(int amount)
    {
        playerScore = playerScore + amount;
        if(playerScore == WAVE2_TRIGGER || playerScore == WAVE3_TRIGGER || playerScore == WAVE4_TRIGGER)
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
    /*
    public void UpdateHighScores (int number)
    {

        if(number > PlayerPrefs.GetInt("FirstPlace", 0))
        {
            int newThird = PlayerPrefs.GetInt("SecondPlace", 0);
            PlayerPrefs.SetInt("ThirdPlace", newThird);
            highScore3.text = newThird.ToString();

            int newSecond = PlayerPrefs.GetInt("FirstPlace", 0);
            PlayerPrefs.SetInt("SecondPlace", newSecond);
            highScore2.text = newSecond.ToString();

            PlayerPrefs.SetInt("FirstPlace", number);
            highScore1.text = number.ToString();
        } else if(number > PlayerPrefs.GetInt("SecondPlace", 0))
        {
            int newThird = PlayerPrefs.GetInt("SecondPlace", 0);
            PlayerPrefs.SetInt("ThirdPlace", newThird);
            highScore3.text = newThird.ToString();

            PlayerPrefs.SetInt("SecondPlace", number);
            highScore2.text = number.ToString();
        } else if(number > PlayerPrefs.GetInt("ThirdPlace", 0))
        {
            PlayerPrefs.SetInt("ThirdPlace", number);
            highScore3.text = number.ToString();
        }
        
    } */
    /*
    public void Reset ()
    {
        // PlayerPrefs.DeleteKey("FirstPlace");
        PlayerPrefs.DeleteAll();
        highScore1.text = "0";
        highScore2.text = "0";
        highScore3.text = "0";
    }*/
}
