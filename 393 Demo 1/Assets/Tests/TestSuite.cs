using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestSuite
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestSuiteSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestSuiteWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        [Test]
        public void onePlusOneEqualsTwo()
        {
            int result = 1 + 1;
            Assert.AreEqual(result, 2);
        }

        [Test]
        public void UserInputUpdatesPlayerPosition()
        {
            Player player = new Player();
            player.currentPosition = Vector3.zero;
            Vector3 StartPosition = player.currentPosition;
            player.UpdatePosition(1.0f, 1.0f);
            Assert.AreNotEqual(StartPosition, player.currentPosition);
        }

        /*
        [UnityTest]
        public IEnumerator PlayerCantWalkThroughBuildings()
        {
            yield return null;
        } */

        [Test]
        public void MovingMouseUpdatesCrossHairDirection()
        {
            CrossHairObject crossHair = new CrossHairObject();
            crossHair.Direction = Vector2.zero;
            Vector2 StartDirection = crossHair.Direction;
            crossHair.UpdateDirection(1.0f, 1.0f);
            Assert.AreNotEqual(StartDirection, crossHair.Direction);
        }

        /*
        [UnityTest]
        public IEnumerator ClickingMouseFiresBullet()
        {
            yield return null;
        } */

        [Test]
        public void ZombiesMove()
        {
            Zombie zombie = new Zombie();
            zombie.currentPosition = Vector2.zero;
            Vector2 StartPosition = zombie.currentPosition;
            zombie.UpdatePosition(1.0f, 1.0f);
            Assert.AreNotEqual(StartPosition, zombie.currentPosition);
        }

        /*
        [UnityTest]
        public IEnumerator ZombiesCantWalkThroughBuildings()
        {
            yield return null;
        } */

        [Test]
        public void BulletCollisionKillsZombie()
        {
            BulletObject bullet = new BulletObject();
            string tag = "Zombies";
            Assert.True(bullet.HitZombie(tag));
        }

        [Test]
        public void ZombieCollisionLowersPlayerHealth()
        {
            Player player = new Player();
            string tag = "Zombies";
            int StartHealth = player.currentHealth;
            player.HandleCollision(tag);
            int ResultHealth = player.currentHealth;
            Assert.AreEqual(StartHealth - 1, ResultHealth);
        }

        [Test]
        public void ThreeZombieCollisionsKillPlayer()
        {
            Player player = new Player();
            string tag = "Zombies";
            player.HandleCollision(tag);
            player.HandleCollision(tag);
            player.HandleCollision(tag);
            Assert.True(player.IsDead());
        }

        // To add: CheckPlayerCollisionDetection ?
        // Animations?
        // User input?
    }
}
