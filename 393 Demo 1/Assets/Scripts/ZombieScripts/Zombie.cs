using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie
{
    public Vector3 position = Vector3.zero;
    public int MAX_HEALTH = 10;
    public int Health = 10;

    public Vector3 ChangeDirection()
    {
        float[] directions = { -1.0f, 0.0f, 1.0f };
        int choiceIndex = Random.Range(0, directions.Length);
        float x = directions[choiceIndex];
        choiceIndex = Random.Range(0, directions.Length);
        float y = directions[choiceIndex];
        if(x == 0.0f && y == 0.0f)
        {
            x = 1.0f;
        }
        position = new Vector3(x, y, 0.0f);
        return position;
    }

    public void UpdateHealth(int damage)
    {
        Health = Health - damage;
    }

    public bool IsDead()
    {
        if (Health <= 0) 
        {
            // Update total kill count
            int kills = PlayerPrefs.GetInt("KillCount", 0);
            PlayerPrefs.SetInt("KillCount", kills + 1);
            return true; 
        }
        return false;
    }
}