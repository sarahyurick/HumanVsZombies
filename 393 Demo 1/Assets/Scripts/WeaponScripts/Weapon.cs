using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    private int currentAmmo;
    private float ammoSpeed;
    private int ammoDamage;

    private int LASER_AMMO = 20;
    private int FLAMETHROWER_AMMO = 15;
    private int GUN_AMMO = 10;
    private int BOOMERANG_AMMO = 1000000000;

    private float LASER_SPEED = 10f;
    private float FLAMETHROWER_SPEED = 7f;
    private float GUN_SPEED = 5f;
    private float BOOMERANG_SPEED = 5f;

    // Zombie health is out of 10
    private int LASER_DAMAGE = 10;
    private int FLAMETHROWER_DAMAGE = 5;
    private int GUN_DAMAGE = 3;
    private int BOOMERANG_DAMAGE = 1;

    public Weapon(string type)
    { 
        if (type == "Laser")
        { 
            currentAmmo = LASER_AMMO; 
            ammoSpeed = LASER_SPEED;
            ammoDamage = LASER_DAMAGE;
        }
        if (type == "Flamethrower")
        { 
            currentAmmo = FLAMETHROWER_AMMO; 
            ammoSpeed = FLAMETHROWER_SPEED;
            ammoDamage = FLAMETHROWER_DAMAGE;
        }
        if (type == "Gun")
        { 
            currentAmmo = GUN_AMMO; 
            ammoSpeed = GUN_SPEED;
            ammoDamage = GUN_DAMAGE;
        }
        if (type == "Boomerang")
        { 
            currentAmmo = BOOMERANG_AMMO; 
            ammoSpeed = BOOMERANG_SPEED;
            ammoDamage = BOOMERANG_DAMAGE;
        }
    }

    public bool HasAmmo()
    {
        if(currentAmmo > 0) { return true; }
        return false;
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public void UseWeapon()
    {
        currentAmmo--;
    }

    public float GetAmmoSpeed()
    {
        return ammoSpeed;
    }

    public int GetDamage()
    {
        return ammoDamage;
    }
}