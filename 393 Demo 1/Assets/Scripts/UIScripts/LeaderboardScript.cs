using UnityEngine;
using UnityEngine.UI;

public class LeaderboardScript : MonoBehaviour
{
    public static UIManager uim = new UIManager("Highscores");
    public Text highScore1;
    public Text highScore2;
    public Text highScore3;

    void Start()
    {
        highScore1.text = "1st Place  " + PlayerPrefs.GetInt("FirstPlace", 0).ToString();
        highScore2.text = "2nd Place  " + PlayerPrefs.GetInt("SecondPlace", 0).ToString();
        highScore3.text = "3rd Place  " + PlayerPrefs.GetInt("ThirdPlace", 0).ToString();
    }

    public void Reset ()
    {
        if(uim.ClickReset())
        {
            PlayerPrefs.DeleteKey("FirstPlace");
            PlayerPrefs.DeleteKey("SecondPlace");
            PlayerPrefs.DeleteKey("ThirdPlace");
            highScore1.text = "1st Place  0";
            highScore2.text = "2nd Place  0";
            highScore3.text = "3rd Place  0";
        }
    }
}
