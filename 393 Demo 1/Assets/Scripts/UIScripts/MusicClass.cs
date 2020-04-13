using UnityEngine;
using UnityEngine.UI;

public class MusicClass : MonoBehaviour
{

    // Used https://answers.unity.com/questions/1260393/make-music-continue-playing-through-scenes.html

    public AudioSource _audioSource;

    private void Awake()
    {
        // DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
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
        } else
        {
            PlayerPrefs.SetInt("Music", 1);
            PlayMusic();
        }
    }

    public void ToggleSound()
    {
        if (PlayerPrefs.GetInt("Sound", 0) == 1)
        {
            PlayerPrefs.SetInt("Sound", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
    }
}