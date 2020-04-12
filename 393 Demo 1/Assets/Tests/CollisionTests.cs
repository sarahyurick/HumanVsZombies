using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CollisionTests
{
    [Test]
    public void PCT1_PressingWMovesPlayerUp()
    {
        // Pressing W with speed 2 should change y position by +2
        var speed = 2;
        Assert.AreEqual(2, new Movement(speed).Calculate(0, 1, 1).y, 0.1f);
    }

    // public void CT1_PlayerLosesHealthWithZombieCollision()

    // public void CT2_PlayerUpdatesMovableObjectPosition()

    // public void CT3_PlayerCannotWalkThroughBuildings()

    // public void CT4_ZombieUpdatesMovableObjectPosition()

    // public void CT5_ZombieCannotWalkThroughBuildings()

    // public void CT6_MovableObjectCannotMoveThroughBuildings()

    // public void CT7_NothingCanLeaveBoundary()

    // the weapon disappears from the map
    // public void CT8_PlayerPicksUpWeaponUponCollision()

    // public void CT9_ArmedPlayerCannotPickUpWeapon()

    // public void CT10_ZombieHealthDecreasesWithAmmoCollision()
}
