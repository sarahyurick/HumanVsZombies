using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie
{
    public int MAX_HEALTH = 10;
    private int Health = 10;

    public void UpdateHealth(int damage)
    {
        Health = Health - damage;
    }

    public bool IsDead()
    {
        if (Health <= 0) { return true; }
        return false;
    }
}