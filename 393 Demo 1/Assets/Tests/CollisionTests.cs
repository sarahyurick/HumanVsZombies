using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CollisionTests
{
    [Test]
    public void CT1_PlayerLosesHealthWithZombieCollision()
    {
        Player player = new Player();
        int startHealth = player.getCurrentHealth();
        player.HandleCollision("Zombies");
        Assert.AreEqual(startHealth - 1, player.getCurrentHealth());
    }

    [Test]
    public void CT2_PlayerUpdatesMovableObjectPosition()
    {
        Player player = new Player();
        Movement movement = new Movement();
        bool needToHandleFurther = player.HandleCollision("MovableObject");
        Assert.IsTrue(needToHandleFurther);

        bool handledCorrectly = movement.CheckMovementStatus("Player", "Dynamic");
        Assert.IsTrue(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("Player", "Kinematic");
        Assert.IsFalse(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("Player", "Static");
        Assert.IsFalse(handledCorrectly);

        handledCorrectly = movement.CheckMovementStatus("MovableObject", "Dynamic");
        Assert.IsTrue(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("MovableObject", "Kinematic");
        Assert.IsFalse(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("MovableObject", "Static");
        Assert.IsFalse(handledCorrectly);

        Assert.IsTrue(movement.IsValid("Player", "MovableObject"));
    }

    [Test]
    public void CT3_PlayerCannotWalkThroughBuildings()
    {
        Player player = new Player();
        Movement movement = new Movement();
        bool needToHandleFurther = player.HandleCollision("Buildings");
        Assert.IsTrue(needToHandleFurther);

        bool handledCorrectly = movement.CheckMovementStatus("Player", "Dynamic");
        Assert.IsTrue(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("Player", "Kinematic");
        Assert.IsFalse(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("Player", "Static");
        Assert.IsFalse(handledCorrectly);

        handledCorrectly = movement.CheckMovementStatus("Buildings", "Kinematic");
        Assert.IsTrue(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("Buildings", "Dynamic");
        Assert.IsFalse(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("Buildings", "Static");
        Assert.IsFalse(handledCorrectly);

        Assert.IsFalse(movement.IsValid("Player", "Buildings"));
    }

    [Test]
    public void CT4_ZombieUpdatesMovableObjectPosition()
    {
        Movement movement = new Movement();

        // Movable object
        bool handledCorrectly = movement.CheckMovementStatus("MovableObject", "Dynamic");
        Assert.IsTrue(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("MovableObject", "Kinematic");
        Assert.IsFalse(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("MovableObject", "Static");
        Assert.IsFalse(handledCorrectly);

        // Zombie
        handledCorrectly = movement.CheckMovementStatus("Zombies", "Dynamic");
        Assert.IsTrue(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("Zombies", "Kinematic");
        Assert.IsFalse(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("Zombies", "Static");
        Assert.IsFalse(handledCorrectly);

        Assert.IsTrue(movement.IsValid("Zombies", "MovableObject"));
    }

    [Test]
    public void CT5_ZombieCannotWalkThroughBuildings()
    {
        Movement movement = new Movement();

        // Building
        bool handledCorrectly = movement.CheckMovementStatus("Buildings", "Kinematic");
        Assert.IsTrue(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("Buildings", "Dynamic");
        Assert.IsFalse(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("Buildings", "Static");
        Assert.IsFalse(handledCorrectly);

        // Zombie
        handledCorrectly = movement.CheckMovementStatus("Zombies", "Dynamic");
        Assert.IsTrue(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("Zombies", "Kinematic");
        Assert.IsFalse(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("Zombies", "Static");
        Assert.IsFalse(handledCorrectly);

        Assert.IsFalse(movement.IsValid("Zombies", "Buildings"));
    }

    [Test]
    public void CT6_MovableObjectCannotMoveThroughBuildings()
    {
        Movement movement = new Movement();

        // Movable object
        bool handledCorrectly = movement.CheckMovementStatus("MovableObject", "Dynamic");
        Assert.IsTrue(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("MovableObject", "Kinematic");
        Assert.IsFalse(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("MovableObject", "Static");
        Assert.IsFalse(handledCorrectly);

        handledCorrectly = movement.CheckMovementStatus("Buildings", "Kinematic");
        Assert.IsTrue(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("Buildings", "Dynamic");
        Assert.IsFalse(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("Buildings", "Static");
        Assert.IsFalse(handledCorrectly);

        Assert.IsFalse(movement.IsValid("MovableObject", "Buildings"));
    }

    [Test]
    public void CT7_NothingCanLeaveBoundary()
    {
        Player player = new Player();
        Movement movement = new Movement();
        bool needToHandleFurther = player.HandleCollision("Border");
        Assert.IsTrue(needToHandleFurther);

        // Border
        bool handledCorrectly = movement.CheckMovementStatus("Border", "Kinematic");
        Assert.IsTrue(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("Border", "Dynamic");
        Assert.IsFalse(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("Border", "Static");
        Assert.IsFalse(handledCorrectly);

        // Player
        handledCorrectly = movement.CheckMovementStatus("Player", "Dynamic");
        Assert.IsTrue(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("Player", "Kinematic");
        Assert.IsFalse(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("Player", "Static");
        Assert.IsFalse(handledCorrectly);
        Assert.IsFalse(movement.IsValid("Player", "Border"));

        // Movable object
        handledCorrectly = movement.CheckMovementStatus("MovableObject", "Dynamic");
        Assert.IsTrue(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("MovableObject", "Kinematic");
        Assert.IsFalse(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("MovableObject", "Static");
        Assert.IsFalse(handledCorrectly);
        Assert.IsFalse(movement.IsValid("MovableObject", "Border"));

        // Zombie
        handledCorrectly = movement.CheckMovementStatus("Zombies", "Dynamic");
        Assert.IsTrue(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("Zombies", "Kinematic");
        Assert.IsFalse(handledCorrectly);
        handledCorrectly = movement.CheckMovementStatus("Zombies", "Static");
        Assert.IsFalse(handledCorrectly);
        Assert.IsFalse(movement.IsValid("Zombies", "Border"));
    }

    [Test]
    public void CT8_PlayerPicksUpWeaponUponCollision()
    {
        Player player = new Player();
        Assert.IsFalse(player.isHoldingWeapon());
        player.HandleCollision("Laser");
        Assert.IsTrue(player.isHoldingWeapon());
        Assert.IsTrue(player.isHoldingLaser());
    }

    [Test]
    public void CT9_ArmedPlayerCannotPickUpWeapon()
    {
        Player player = new Player();
        // Player picks up laser
        player.HandleCollision("Laser");
        // Player tries to pick up flamethrower
        player.HandleCollision("Flamethrower");
        Assert.IsTrue(player.isHoldingWeapon());
        Assert.IsTrue(player.isHoldingLaser());
        Assert.IsFalse(player.isHoldingFlamethrower());
    }

    [Test]
    public void CT10_ZombieHealthDecreasesWithAmmoCollision()
    {
        int damage = 1;
        AmmoObject ammo = new AmmoObject(damage);
        Zombie zombie = new Zombie();
        int startHealth = zombie.Health;
        if(ammo.HitZombie("Zombies"))
        {
            zombie.UpdateHealth(ammo.damage);
        }
        Assert.AreEqual(startHealth - 1, zombie.Health);
    }
}
