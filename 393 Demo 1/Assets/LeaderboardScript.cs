using UnityEngine;
using UnityEngine.UI;

public class LeaderboardScript : MonoBehaviour
{
    public Text highScore1;
    public Text highScore2;
    public Text highScore3;

    void Start()
    {
        highScore1.text = PlayerPrefs.GetInt("FirstPlace", 0).ToString();
        highScore2.text = PlayerPrefs.GetInt("SecondPlace", 0).ToString();
        highScore3.text = PlayerPrefs.GetInt("ThirdPlace", 0).ToString();
    }

    public void Reset ()
    {
        PlayerPrefs.DeleteKey("FirstPlace");
        PlayerPrefs.DeleteKey("SecondPlace");
        PlayerPrefs.DeleteKey("ThirdPlace");
        highScore1.text = "0";
        highScore2.text = "0";
        highScore3.text = "0";
    }
}
