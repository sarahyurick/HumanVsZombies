using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Input settings:")]
    public int playerId = 0;
    public Player player;
    private CrossHairObject crossHairObject;

    [Space]
    [Header("Character attributes:")]
    public float MOVEMENT_BASE_SPEED = 2.0f;
    public float CROSSHAIR_DISTANCE = 1f;

    [Space]
    [Header("Character statistics:")]
    Vector3 movement;
    public float movementSpeed;
    Vector3 aim;
    bool isAiming;
    bool endOfAiming;
    public PlayerHealthBar playerHealthBar;
    public WeaponHealthBar weaponHealthBar;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject crossHair;

    [Space]
    [Header("Prefabs:")]
    public GameObject laserBeamPrefab;
    public GameObject firePrefab;
    public GameObject bulletPrefab;
    public GameObject boomerangPrefab;

    void Awake()
    {
        player = new Player();
        playerHealthBar.SetMaxHealth(player.MAX_HEALTH);
        weaponHealthBar.SetMaxHealth(1);
        weaponHealthBar.SetHealth(0);
        crossHairObject = new CrossHairObject();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        AimAndShoot();
        Animate();
        Move();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        string gameObjectTag = coll.gameObject.tag;
        player.HandleCollision(gameObjectTag);

        // Stuff that needs to be updated after a collision
        // Health if it was a zombie
        playerHealthBar.SetHealth(player.getCurrentHealth());
        if(player.IsDead())
        {
            Destroy(gameObject);
            FindObjectOfType<GameManager>().EndGame();
        }

        // If it was a weapon remove it from the map
        if(player.isHolding(gameObjectTag))
        {
            Destroy(coll.gameObject);
            weaponHealthBar.SetMaxHealth(player.currentWeapon.GetCurrentAmmo());
        }
        
    }

    private void ProcessInputs()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        if (player.isHoldingWeapon()) {
            Vector3 mouseMovement = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
            aim = aim + mouseMovement;
            if (aim.magnitude > 1.0f)
            {
                aim.Normalize();
            }

            isAiming = Input.GetButton("Fire1");
            endOfAiming = Input.GetButtonUp("Fire1");

            if (movement.magnitude > 1.0f)
            {
                movement.Normalize();
            }
        } else
        {
            aim = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    private void Animate()
    {
        if (movement != Vector3.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        animator.SetFloat("Magnitude", movement.magnitude);

        animator.SetBool("holdingLaser", player.isHoldingLaser());
        animator.SetBool("holdingFlamethrower", player.isHoldingFlamethrower());
        animator.SetBool("holdingGun", player.isHoldingGun());
        animator.SetBool("holdingBoomerang", player.isHoldingBoomerang());
    }

    private void Move()
    {
        // transform.position = transform.position + movement * Time.deltaTime;
        rb.velocity = new Vector2(movement.x, movement.y) * MOVEMENT_BASE_SPEED;
    }

    private void AimAndShoot()
    {
        Vector2 shootingDirection = new Vector2(aim.x, aim.y);
        crossHairObject.UpdateDirection(aim.x, aim.y);

        if (aim.magnitude > 0.0f)
        {
            crossHair.transform.localPosition = aim * CROSSHAIR_DISTANCE;
            crossHair.SetActive(true);

            shootingDirection.Normalize();
            if (endOfAiming && player.isHoldingWeapon())
            {
                // Need to figure out what we're shooting
                GameObject ammo; 
                if(player.isHoldingLaser())
                {
                    ammo = Instantiate(laserBeamPrefab, transform.position, Quaternion.identity);
                } else if(player.isHoldingFlamethrower())
                {
                    ammo = Instantiate(firePrefab, transform.position, Quaternion.identity);
                } else if(player.isHoldingGun())
                {
                    ammo = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                } else
                {
                    ammo = Instantiate(boomerangPrefab, transform.position, Quaternion.identity);
                }

                // Shoot it
                Ammo ammoScript = ammo.GetComponent<Ammo>();
                ammoScript.velocity = shootingDirection * player.currentWeapon.GetAmmoSpeed();
                ammoScript.spartan = gameObject;
                ammo.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
                Destroy(ammo, 2.0f);

                player.currentWeapon.UseWeapon();
                if (!player.currentWeapon.HasAmmo())
                {
                    player.DropWeapon();
                    weaponHealthBar.SetHealth(0);
                } else
                {
                    weaponHealthBar.SetHealth(player.currentWeapon.GetCurrentAmmo());
                }
            }
        }
        else
        {
            crossHair.SetActive(false);
        }
    }

}

public class CrossHairObject
{
    public Vector2 Direction;
    
    public void UpdateDirection(float x, float y)
    {
        Direction = new Vector2(x, y);
    }
}