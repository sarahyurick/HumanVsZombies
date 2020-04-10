using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used https://www.youtube.com/watch?v=XOjd_qU2Ido

[System.Serializable]

public class PlayerData
{
    public int wave;
    public int score;
    public int health;
    
    public PlayerData (Player player)
    {
        health = player.getCurrentHealth();
    }
}
