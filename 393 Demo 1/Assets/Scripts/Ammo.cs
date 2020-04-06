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

    private AmmoObject ammo;

    private void Awake()
    {
        ammo = new AmmoObject();
    }

    void Update()
    {
        currentPosition = new Vector2(transform.position.x, transform.position.y);
        newPosition = currentPosition + velocity * Time.deltaTime;

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition + offset, newPosition + offset);

        foreach (RaycastHit2D hit in hits)
        {
            GameObject other = hit.collider.gameObject;
            if (other != spartan)
            {
                string tag = other.tag;
                if (ammo.HitZombie(tag)) {
                    ZombieMovement zombieController = other.GetComponent<ZombieMovement>();
                    zombieController.UpdateHealth();
                }
                Destroy(gameObject);
                break;
            }
        }

        transform.position = newPosition;
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