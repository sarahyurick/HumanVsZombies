using UnityEngine;
using UnityEngine.UI;

public class MusicUI : MonoBehaviour
{
    public GameObject MusicToggle;
    public GameObject SoundToggle;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Music", 0) == 1)
        {
            MusicToggle.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            MusicToggle.GetComponent<Toggle>().isOn = false;
        }

        if (PlayerPrefs.GetInt("Sound", 0) == 1)
        {
            SoundToggle.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            SoundToggle.GetComponent<Toggle>().isOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    { /*
        if (PlayerPrefs.GetInt("MusicUpdate", 0) == 1)
        {
            if (PlayerPrefs.GetInt("Music", 0) == 1)
            {
                MusicToggle.GetComponent<Toggle>().isOn = true;
            }
            else
            {
                MusicToggle.GetComponent<Toggle>().isOn = false;
            }
            PlayerPrefs.SetInt("MusicUpdate", 0);
        } 

        if(PlayerPrefs.GetInt("SoundUpdate", 0) == 1)
        {
            if (PlayerPrefs.GetInt("Sound", 0) == 1)
            {
                SoundToggle.GetComponent<Toggle>().isOn = true;
            }
            else
            {
                SoundToggle.GetComponent<Toggle>().isOn = false;
            }
            PlayerPrefs.SetInt("SoundUpdate", 0);
        } 
        */
    }
}
