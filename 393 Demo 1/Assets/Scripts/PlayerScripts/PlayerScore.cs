using UnityEngine;
using UnityEngine.UI;

// Used https://www.youtube.com/watch?v=vZU51tbgMXk
// and https://www.youtube.com/watch?v=TAGZxRMloyU

public class PlayerScore : MonoBehaviour
{
    public static GameManager gm;
    public GameStatus gs = new GameStatus();

    public Text score;
    public Text wave;

    public float startTime;

    void Start()
    {
        PlayerPrefs.SetInt("KillCount", 0);

        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        }

        startTime = Time.time;
    }

    void Update()
    {
        int t = (int) (Time.time - startTime);

        gs.IncreaseScoreByTimeIfNecessary(t, startTime);
        gs.IncreaseScoreIfZombieWasKilled();

        score.text = "Score:  " + gs.playerScore.ToString();
        int waveText = gs.waveCount + 1;
        wave.text = "Wave:  " + (waveText).ToString();
    }

    public void PauseAndSave()
    {
        gs.UpdateHighScores();
    }
}
