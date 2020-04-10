using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public Vector2 currentPosition;
    public Vector2 newPosition;
    public Vector2 velocity = new Vector2(0.0f, 0.0f);
    public GameObject spartan;
    public GameObject zombie;
    public Vector2 offset = new Vector2(0.0f, 0.0f);

    private Player player;
    private int damage;
    private AmmoObject ammo;

    private void Awake()
    {
        ammo = new AmmoObject();
    }

    void Update()
    {
        PlayerController playerController = spartan.GetComponent<PlayerController>();
        player = playerController.player;
        if (player.currentWeapon == null)
        {
            damage = 0;
        }
        else
        {
            damage = player.currentWeapon.GetDamage();
        }

        currentPosition = new Vector2(transform.position.x, transform.position.y);
        newPosition = currentPosition + velocity * Time.deltaTime;

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition + offset, newPosition + offset);

        foreach (RaycastHit2D hit in hits)
        {
            GameObject other = hit.collider.gameObject;
            if (other != spartan && isNotWeapon(other.tag))
            {
                string tag = other.tag;
                if (ammo.HitZombie(tag)) {
                    ZombieMovement zombieController = other.GetComponent<ZombieMovement>();
                    zombieController.zombie.UpdateHealth(damage);
                }
                Destroy(gameObject);
                break;
            }
        }

        transform.position = newPosition;
    }

    private bool isNotWeapon(string tag)
    {
        if (tag == "Laser") { return false; }
        else if (tag == "Flamethrower") { return false; }
        else if (tag == "Gun") { return false; }
        else if (tag == "Boomerang") { return false; }
        return true;
    }
}

public class AmmoObject {

    public bool HitZombie(string tag)
    {
        if(tag == "Zombies")
        {
            return true;
        }
        return false;
    }
}