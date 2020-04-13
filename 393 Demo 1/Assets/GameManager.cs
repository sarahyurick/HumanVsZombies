using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// Used https://www.youtube.com/watch?v=VbZ9_C4-Qbo

public class GameManager : MonoBehaviour
{

    public static GameManager gm;
    public static PlayerScore ps;
    public static GameStatus gs;

    public Transform zombiePrefab;
    public Transform laserPrefab;
    public Transform flamethrowerPrefab;
    public Transform gunPrefab;
    public Transform boomerangPrefab;

    public Transform barrelPrefab;
    public Transform darkBoxPrefab;
    public Transform lightBoxPrefab;
    public Transform darkCratePrefab;
    public Transform lightCratePrefab;

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
    public Transform spawnPoint17;
    public Transform spawnPoint18;
    public Transform spawnPoint19;
    public Transform spawnPoint20;
    public Transform spawnPoint21;
    public Transform spawnPoint22;
    public Transform spawnPoint23;
    public Transform spawnPoint24;
    public Transform spawnPoint25;
    public Transform spawnPoint26;
    public Transform spawnPoint27;
    public Transform spawnPoint28;
    public Transform spawnPoint29;
    public Transform spawnPoint30;
    public Transform spawnPoint31;
    public Transform spawnPoint32;
    public Transform spawnPoint33;
    public Transform spawnPoint34;
    public Transform spawnPoint35;
    public Transform[] spawnPoints;

    bool finalWave = false;
    bool gameHasEnded = false;

    public Transform spawnPrefab;

    void Start()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
            ps = GameObject.FindGameObjectWithTag("GM").GetComponent<PlayerScore>();
        }
        spawnPoints = new Transform[36]{ spawnPoint, spawnPoint1, spawnPoint2, spawnPoint3, spawnPoint4,
            spawnPoint5, spawnPoint6, spawnPoint7, spawnPoint8, spawnPoint9,
            spawnPoint10, spawnPoint11, spawnPoint12, spawnPoint13, spawnPoint14,
            spawnPoint15, spawnPoint16, spawnPoint17, spawnPoint18, spawnPoint19,
            spawnPoint20, spawnPoint21, spawnPoint22, spawnPoint23, spawnPoint24,
            spawnPoint25, spawnPoint26, spawnPoint27, spawnPoint28, spawnPoint29,
            spawnPoint30, spawnPoint31, spawnPoint32, spawnPoint33, spawnPoint34,
            spawnPoint35 };
        TriggerNewWave(0);

        Transform[] moveableObjects = new Transform[5] { barrelPrefab, darkBoxPrefab, lightBoxPrefab, darkCratePrefab, lightCratePrefab };
        SpawnMovableObjects(moveableObjects);
    }

    void Update()
    {
        if (ps.shouldTriggerNewWave)
        {
            TriggerNewWave(ps.waveCount);
            ps.shouldTriggerNewWave = false;
        }
        else if (finalWave && ps.timerTriggered)
        {
            SpawnZombies(12);
            ps.timerTriggered = false;
        }
    }

    public void SpawnMovableObjects(Transform[] moveableObjects)
    {
        for (int i = 0; i < moveableObjects.Length; i++)
        {
            int choice = Random.Range(0, spawnPoints.Length);
            Transform thisSpawnPoint = (Transform)spawnPoints.GetValue(choice);
            Instantiate(moveableObjects[i], thisSpawnPoint.position, thisSpawnPoint.rotation);
        }
    }

    public void SpawnZombies(int count)
    {
        // yield return new WaitForSeconds(spawnDelay);
        for (int i = 0; i < count; i++)
        {
            int choice = Random.Range(0, spawnPoints.Length);
            Transform thisSpawnPoint = (Transform)spawnPoints.GetValue(choice);
            Instantiate(zombiePrefab, thisSpawnPoint.position, thisSpawnPoint.rotation);
            Transform dirt = Instantiate(spawnPrefab, thisSpawnPoint.position, thisSpawnPoint.rotation);
            Destroy(dirt.gameObject, 3f);
        }

    }

    public void SpawnWeapon(int waveCount)
    {
        int choice = Random.Range(0, spawnPoints.Length);
        Transform thisSpawnPoint = (Transform)spawnPoints.GetValue(choice);
        if (waveCount == 0)
        {
            Instantiate(laserPrefab, thisSpawnPoint.position, thisSpawnPoint.rotation);
        }
        else if (waveCount == 1)
        {
            Instantiate(flamethrowerPrefab, thisSpawnPoint.position, thisSpawnPoint.rotation);
        }
        else if (waveCount == 2)
        {
            Instantiate(gunPrefab, thisSpawnPoint.position, thisSpawnPoint.rotation);
        }
        else if (waveCount == 3)
        {
            Instantiate(boomerangPrefab, thisSpawnPoint.position, thisSpawnPoint.rotation);
            finalWave = true;
        }
    }

    public static void TriggerNewWave(int waveCount)
    {
        gm.SpawnZombies(3 + 3 * waveCount);
        gm.SpawnWeapon(waveCount);
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            ps.UpdateHighScores(ps.playerScore);
            gameHasEnded = true;
            SceneManager.LoadScene("GameOver");
            // Invoke("Restart", restartDelay);
        }
    }
}
