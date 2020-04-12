using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LoadSceneAfterVideoEnded : MonoBehaviour
{
    public static UIManager uim = new UIManager("IntroVideo");
    public VideoPlayer VideoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component

    void Start()
    {
        VideoPlayer.loopPointReached += LoadScene;
    }

    public void SkipToGame()
    {
        if(uim.GoToGameplay())
        {
            SceneManager.LoadScene("Gameplay");
        }
    }

    void LoadScene(VideoPlayer vp)
    {
        if (uim.GoToGameplay())
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}