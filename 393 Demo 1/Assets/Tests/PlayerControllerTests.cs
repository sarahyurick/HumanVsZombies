using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerControllerTests
{

    [Test]
    public void PCT1_PressingWMovesPlayerUp()
    {
        // Pressing W with speed 2 should change y position by +2
        var speed = 2;
        Assert.AreEqual(2, new Movement(speed).Calculate(0, 1, 1).y, 0.1f);
    }

    [Test]
    public void PCT1_PressingAMovesPlayerLeft()
    {
        // Pressing A with speed 2 should change x position by -2
        var speed = 2;
        Assert.AreEqual(-2, new Movement(speed).Calculate(-1, 0, 1).x, 0.1f);
    }

    [Test]
    public void PCT1_PressingSMovesPlayerDown()
    {
        // Pressing S with speed 2 should change y position by -2
        var speed = 2;
        Assert.AreEqual(-2, new Movement(speed).Calculate(0, -1, 1).y, 0.1f);
    }

    [Test]
    public void PCT1_PressingDMovesPlayerRight()
    {
        // Pressing D with speed 2 should change x position by +2
        var speed = 2;
        Assert.AreEqual(2, new Movement(speed).Calculate(1, 0, 1).x, 0.1f);
    }

    [Test]
    public void PCT2_MovingMouseUpAimsCrossHairUp()
    {
        // Start out not aiming
        Vector3 aim = Vector3.zero;
        // Receiving a Mouse Y input of 1 relative to the player
        // moves the Mouse's Y position by +1
        Assert.AreEqual(1, new Aim().AimDirection(aim, 0, 1, 0).y, 0.1f);
    }

    [Test]
    public void PCT2_MovingMouseLeftAimsCrossHairLeft()
    {
        // Start out not aiming
        Vector3 aim = Vector3.zero;
        // Receiving a Mouse X input of -1 relative to the player
        // moves the Mouse's x position by -1
        Assert.AreEqual(-1, new Aim().AimDirection(aim, -1, 0, 0).x, 0.1f);
    }

    [Test]
    public void PCT2_MovingMouseDownAimsCrossHairDown()
    {
        // Start out not aiming
        Vector3 aim = Vector3.zero;
        // Receiving a Mouse Y input of -1 relative to the player
        // moves the Mouse's y position by -1
        Assert.AreEqual(-1, new Aim().AimDirection(aim, 0, -1, 0).y, 0.1f);
    }

    [Test]
    public void PCT2_MovingMouseRightAimsCrossHairRight()
    {
        // Start out not aiming
        Vector3 aim = Vector3.zero;
        // Receiving a Mouse X input of 1 relative to the player
        // moves the Mouse's x position by +1
        Assert.AreEqual(1, new Aim().AimDirection(aim, 1, 0, 0).x, 0.1f);
    }

    [Test]
    public void PCT3_AmmoLaunchedInDirectionOfFiring() // This test is pretty dumb, sorry
    {
        // Aiming the crosshair in the upper right direction
        Vector3 aim = new Vector3(1, 1, 0);

        Vector2 shootingDirection = new Vector2(aim.x, aim.y);
        shootingDirection.Normalize();
        Assert.AreEqual(shootingDirection, new Shoot().CalculateDirection(aim));
    }

    [Test]
    public void PCT3_AmmoDecrementsAfterShooting()
    {
        // Creating a player and a shooting instance
        Player player = new Player();
        Shoot shoot = new Shoot();
        // Setting the player up with a weapon, in this case the laser,
        // by having him collide with the Laser on the map
        player.HandleCollision("Laser");
        // Starting ammo
        int ammo = player.currentWeapon.GetCurrentAmmo();
        // Shooting the weapon
        shoot.ShootWeapon(player);
        // Ammo count should be 1 less
        Assert.AreEqual(ammo - 1, player.currentWeapon.GetCurrentAmmo());
    }

    [Test]
    public void PCT3_PlayerDropsWeaponAfterDrainingIt()
    {
        // Creating a player and a shooting instance
        Player player = new Player();
        Shoot shoot = new Shoot();
        // Setting the player up with a weapon, in this case the laser,
        // by having him collide with the Laser on the map
        player.HandleCollision("Laser");
        // Starting ammo
        int ammo = player.currentWeapon.GetCurrentAmmo();
        int remainingAmmo = ammo;
        // Shooting the weapon until the ammo runs out
        for(int i = 0; i < ammo; i++)
        {
            remainingAmmo = shoot.ShootWeapon(player);
        }
        // Weapon ammo should be zero
        Assert.AreEqual(0, remainingAmmo);
        // Since the ammo is zero, the player should not have a weapon anymore
        Assert.IsNull(player.currentWeapon);
    }

}
