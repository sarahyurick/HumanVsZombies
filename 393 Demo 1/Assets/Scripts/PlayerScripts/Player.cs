using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    // Keep track of player position?
    public UIManager uim = new UIManager("Gameplay");

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

    public bool HandleCollision(string tag)
    {
        bool needToHandleFurther = true;

        if (tag == "Zombies")
        {
            UpdatePlayerHealth();
            return !needToHandleFurther;
        }

        if (tag == "Laser" || tag == "Flamethrower" || tag == "Gun" || tag == "Boomerang")
            {
            if (!holdingWeapon)
            {
                PickUpWeapon(tag);
                holdingWeapon = true;
                currentWeapon = new Weapon(tag);
                return !needToHandleFurther;
            }
            return !needToHandleFurther;
        }

        return needToHandleFurther;

    }

    public void UpdatePlayerHealth()
    {
        currentHealth--;
    }

    public bool IsDead()
    {
        if (currentHealth <= 0) 
        {
            uim.Transition("GameOverMenu");
            return true; 
        }
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