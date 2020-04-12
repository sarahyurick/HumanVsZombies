using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    // Keep track of player position?

    public int MAX_HEALTH = 3;
    private int currentHealth = 3;

    private bool holdingWeapon = false;
    private bool holdingLaser = false;
    private bool holdingFlamethrower = false;
    private bool holdingGun = false;
    private bool holdingBoomerang = false;

    public Weapon currentWeapon = null;

    public int getCurrentHealth() { return currentHealth; }

    public bool isHoldingWeapon() { return holdingWeapon; }
    public bool isHoldingLaser() { return holdingLaser; }
    public bool isHoldingFlamethrower() { return holdingFlamethrower; }
    public bool isHoldingGun() { return holdingGun; }
    public bool isHoldingBoomerang() { return holdingBoomerang; }

    public void HandleCollision(string tag)
    {
        if (tag == "Zombies")
        {
            UpdatePlayerHealth();
        }

        if (!holdingWeapon)
        {
            if (tag == "Laser" || tag == "Flamethrower" || tag == "Gun" || tag == "Boomerang")
            {
                PickUpWeapon(tag);
                holdingWeapon = true;
                currentWeapon = new Weapon(tag);
            }
        }

    }

    private void UpdatePlayerHealth()
    {
        currentHealth--;
    }

    public bool IsDead()
    {
        if (currentHealth <= 0) { return true; }
        return false;
    }

    private void PickUpWeapon(string tag)
    {
        if (tag == "Laser") { holdingLaser = true; }
        if (tag == "Flamethrower") { holdingFlamethrower = true; }
        if (tag == "Gun") { holdingGun = true; }
        if (tag == "Boomerang") { holdingBoomerang = true; }
    }

    public bool isHolding(string tag)
    {
        if (tag == "Laser" && holdingLaser) { return true; }
        if (tag == "Flamethrower" && holdingFlamethrower) { return true; }
        if (tag == "Gun" && holdingGun) { return true; }
        if (tag == "Boomerang" && holdingBoomerang) { return true; }
        return false;
    }

    public void DropWeapon()
    {
        currentWeapon = null;

        holdingWeapon = false;
        holdingLaser = false;
        holdingFlamethrower = false;
        holdingGun = false;
        holdingBoomerang = false;
    }

}