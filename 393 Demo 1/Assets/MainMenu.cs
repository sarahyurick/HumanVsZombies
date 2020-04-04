using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Used https://www.youtube.com/watch?v=zc8ac_qUXQY

    public void PlayGame ()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
