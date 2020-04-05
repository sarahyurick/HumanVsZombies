using UnityEngine;
using UnityEngine.SceneManagement;

// Used https://www.youtube.com/watch?v=VbZ9_C4-Qbo

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public float restartDelay = 1f;

    public void EndGame ()
    {
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            SceneManager.LoadScene("GameOver");
            // Invoke("Restart", restartDelay);
        }
    }

    void Restart ()
    {
        SceneManager.LoadScene("Gameplay");
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
