using UnityEngine;
using UnityEngine.UI;

// Used https://www.youtube.com/watch?v=vZU51tbgMXk
// and https://www.youtube.com/watch?v=TAGZxRMloyU

public class PlayerScore : MonoBehaviour
{
    public Transform player;
    public int playerScore;

    public Text score;
    public Text highScore1;
    public Text highScore2;
    public Text highScore3;

    private float startTime;

    void Start()
    {
        startTime = Time.time;
        playerScore = 0;
        highScore1.text = PlayerPrefs.GetInt("FirstPlace", 0).ToString();
        highScore2.text = PlayerPrefs.GetInt("SecondPlace", 0).ToString();
        highScore3.text = PlayerPrefs.GetInt("ThirdPlace", 0).ToString();
    }

    void Update()
    {
        float t = Time.time - startTime;
        if(t % 10 == 0)
        {
            playerScore++;
        }
        score.text = player.position.z.ToString("0");
    }

    public void CalculateScore ()
    {
        int number = Random.Range(1, 7);
        score.text = number.ToString();

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
        
    }

    public void Reset ()
    {
        // PlayerPrefs.DeleteKey("FirstPlace");
        PlayerPrefs.DeleteAll();
        highScore1.text = "0";
        highScore2.text = "0";
        highScore3.text = "0";
    }
}
