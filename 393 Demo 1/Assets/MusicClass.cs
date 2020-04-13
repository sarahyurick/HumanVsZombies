using UnityEngine;
using UnityEngine.UI;

public class MusicClass : MonoBehaviour
{
    public GameObject MusicToggle;
    public GameObject SoundToggle;

    // Used https://answers.unity.com/questions/1260393/make-music-continue-playing-through-scenes.html

    public AudioSource _audioSource;

    private void Awake()
    {
        // PlayerPrefs.SetInt("Music", 1);
        // PlayerPrefs.SetInt("Sound", 1);
        // DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();

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

    public void PlayMusic()
    {
        if(PlayerPrefs.GetInt("Music", 0) == 1)
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
        if (PlayerPrefs.GetInt("Music", 0) == 1)
        {
            PlayerPrefs.SetInt("Music", 0);
            StopMusic();
            MusicToggle.GetComponent<Toggle>().isOn = false;
        } else
        {
            PlayerPrefs.SetInt("Music", 1);
            PlayMusic();
            MusicToggle.GetComponent<Toggle>().isOn = true;
        }
    }

    public void ToggleSound()
    {
        if (PlayerPrefs.GetInt("Sound", 0) == 1)
        {
            PlayerPrefs.SetInt("Sound", 0);
            SoundToggle.GetComponent<Toggle>().isOn = false;
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
            SoundToggle.GetComponent<Toggle>().isOn = true;
        }
    }
}