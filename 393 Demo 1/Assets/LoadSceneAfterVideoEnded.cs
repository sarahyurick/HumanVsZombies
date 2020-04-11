using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LoadSceneAfterVideoEnded : MonoBehaviour
{
    public VideoPlayer VideoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component

    void Start()
    {
        VideoPlayer.loopPointReached += LoadScene;
    }

    public void SkipToGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    void LoadScene(VideoPlayer vp)
    {
        SceneManager.LoadScene("Gameplay");
    }
}