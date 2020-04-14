using UnityEngine;
using UnityEngine.UI;

public class MusicClass : MonoBehaviour
{

    // Used https://answers.unity.com/questions/1260393/make-music-continue-playing-through-scenes.html

    public AudioSource _audioSource;
    public bool newGame;
    public Toggle MusicToggle;
    public Toggle SoundToggle;

    private void Awake()
    {
        // DontDestroyOnLoad(transform.gameObject);
        // _audioSource = GetComponent<AudioSource>();
        if(newGame)
        {
            PlayerPrefs.SetInt("Music", 1);
            PlayerPrefs.SetInt("Sound", 1);
        }
    }

    public void Update()
    {
        /*
        if(PlayerPrefs.GetInt("Music") == 1 && MusicToggle.isOn == false)
        {
            MusicToggle.isOn = true;
        }
        if (PlayerPrefs.GetInt("Music") == 0 && MusicToggle.isOn == true)
        {
            MusicToggle.isOn = false;
        }
        Debug.Log(PlayerPrefs.GetInt("Music") + " " + MusicToggle.isOn);

        if (PlayerPrefs.GetInt("Sound") == 1 && SoundToggle.isOn == false)
        {
            SoundToggle.isOn = true;
        }
        if (PlayerPrefs.GetInt("Sound") == 0 && SoundToggle.isOn == true)
        {
            SoundToggle.isOn = false;
        }*/
    }

    public void PlayMusic()
    {
        if(PlayerPrefs.GetInt("Music") == 1)
        {
            if (_audioSource.isPlaying) return;
            _audioSource.Play();
        } else
        {
            StopMusic();
        }
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }

    public void ToggleMusic()
    {
        if (PlayerPrefs.GetInt("Music") == 1)
        {
            PlayerPrefs.SetInt("Music", 0);
            StopMusic();
        } else
        {
            PlayerPrefs.SetInt("Music", 1);
            PlayMusic();
        }
        PlayerPrefs.Save();
    }

    public void ToggleSound()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            PlayerPrefs.SetInt("Sound", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        PlayerPrefs.Save();
    }
}