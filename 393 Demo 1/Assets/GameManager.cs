using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// Used https://www.youtube.com/watch?v=VbZ9_C4-Qbo

public class GameManager : MonoBehaviour
{

    public static GameManager gm;

    public Transform zombiePrefab;

    public Transform spawnPoint;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;
    public Transform spawnPoint5;
    public Transform spawnPoint6;
    public Transform spawnPoint7;
    public Transform spawnPoint8;
    public Transform spawnPoint9;
    public Transform spawnPoint10;
    public Transform spawnPoint11;
    public Transform spawnPoint12;
    public Transform spawnPoint13;
    public Transform spawnPoint14;
    public Transform spawnPoint15;
    public Transform spawnPoint16;

    public Transform[] spawnPoints;
    
    bool gameHasEnded = false;

    public int spawnDelay = 2;
    public float restartDelay = 1f;

    void Start()
    {
        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        }
        spawnPoints = new Transform[17]{ spawnPoint, spawnPoint1, spawnPoint2, spawnPoint3, spawnPoint4,
            spawnPoint5, spawnPoint6, spawnPoint7, spawnPoint8, spawnPoint9,
            spawnPoint10, spawnPoint11, spawnPoint12, spawnPoint13, spawnPoint14,
            spawnPoint15, spawnPoint16 };
        SpawnZombies(3);
    }

    public void SpawnZombies(int count)
    {
        // yield return new WaitForSeconds(spawnDelay);
        for(int i = 0; i < count; i++)
        {
            int choice = Random.Range(0, spawnPoints.Length);
            Transform thisSpawnPoint = (Transform) spawnPoints.GetValue(choice);
            Instantiate(zombiePrefab, thisSpawnPoint.position, thisSpawnPoint.rotation);
        }

    }

    public static void TriggerNewWave()
    {
        gm.SpawnZombies(3);
    }

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
