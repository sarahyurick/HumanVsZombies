using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Input settings:")]
    public int playerId = 0;
    public Player player;

    [Space]
    [Header("Character attributes:")]
    public float MOVEMENT_BASE_SPEED = 3.0f;

    [Space]
    [Header("Character statistics:")]
    public Movement Movement;
    Vector3 moving;
    public Aim Aimer;
    Vector3 aim;
    public Shoot Shoot;
    bool shooting;
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

    [Space]
    [Header("Sound effects:")]
    public AudioSource LaserAudio;
    public AudioSource FlamethrowerAudio;
    public AudioSource GunAudio;
    public AudioSource BoomerangAudio;

    void Awake()
    {
        if (PlayerPrefs.GetInt("Sound", 0) != 0 && PlayerPrefs.GetInt("Sound", 0) != 1)
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        if (PlayerPrefs.GetInt("Music", 0) != 0 && PlayerPrefs.GetInt("Music", 0) != 1)
        {
            PlayerPrefs.SetInt("Music", 0);
        }

        PlayerPrefs.SetInt("Paused", 0);
        player = new Player();
        playerHealthBar.SetMaxHealth(player.MAX_HEALTH);

        weaponHealthBar.SetMaxHealth(1);
        weaponHealthBar.SetHealth(0);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Movement = new Movement(MOVEMENT_BASE_SPEED);
        Aimer = new Aim();
        Shoot = new Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("Paused", 0) == 0)
        {
            ProcessInputs();
            AimAndShoot();
            Animate();
        } else if(PlayerPrefs.GetInt("Paused", 0) == 2)
        {
            PlayerPrefs.SetInt("Paused", 0);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        string gameObjectTag = coll.gameObject.tag;
        bool needToHandleFurther = player.HandleCollision(gameObjectTag);

        // Stuff that needs to be updated after a collision
        // Health if it was a zombie
        playerHealthBar.SetHealth(player.getCurrentHealth());
        if(player.IsDead())
        {
            // Destroy(gameObject);
            FindObjectOfType<GameManager>().EndGame();
        }

        // If it was a weapon remove it from the map
        if(player.isHolding(gameObjectTag))
        {
            Destroy(coll.gameObject);
            weaponHealthBar.SetMaxHealth(player.currentWeapon.GetCurrentAmmo());
        }

        if(needToHandleFurther)
        {
            string hitBodyType = coll.gameObject.GetComponent<Rigidbody2D>().bodyType.ToString();
            bool handledCorrectly = Movement.CheckMovementStatus(gameObjectTag, hitBodyType);
            if(!handledCorrectly)
            {
                // Game will terminate if something went wrong
                FindObjectOfType<GameManager>().EndGame();
            }
        }
        
    }

    private void ProcessInputs()
    {
        moving = Movement.Calculate(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"),
            Time.deltaTime);
        // transform.position += moving;

        rb.velocity = new Vector2(moving.x, moving.y);

        if (player.isHoldingWeapon()) {
            aim = Aimer.AimDirection(aim,
                Input.GetAxis("Mouse X"),
                Input.GetAxis("Mouse Y"),
                0.0f);

            shooting = Input.GetButtonUp("Fire1");
        } else
        {
            aim = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    private void Animate()
    {
        if (moving != Vector3.zero)
        {
            animator.SetFloat("Horizontal", moving.x);
            animator.SetFloat("Vertical", moving.y);
        }
        animator.SetFloat("Magnitude", moving.magnitude);

        animator.SetBool("holdingLaser", player.isHoldingLaser());
        animator.SetBool("holdingFlamethrower", player.isHoldingFlamethrower());
        animator.SetBool("holdingGun", player.isHoldingGun());
        animator.SetBool("holdingBoomerang", player.isHoldingBoomerang());
    }

    private void AimAndShoot()
    {
        Vector2 shootingDirection = Shoot.CalculateDirection(aim);

        if (aim.magnitude > 0.0f)
        {
            crossHair.transform.localPosition = aim;
            crossHair.SetActive(true);

            if (shooting && player.isHoldingWeapon())
            {
                // Need to figure out what we're shooting
                GameObject ammo = AmmoToFire();

                // Shoot it
                Ammo ammoScript = ammo.GetComponent<Ammo>();
                ammoScript.velocity = shootingDirection * player.currentWeapon.GetAmmoSpeed();
                ammoScript.spartan = gameObject;
                ammo.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
                Destroy(ammo, 2.0f);

                int ammoLeft = Shoot.ShootWeapon(player);
                weaponHealthBar.SetHealth(ammoLeft);
            }
        }
        else
        {
            crossHair.SetActive(false);
        }
    }

    private GameObject AmmoToFire()
    {
        if (player.isHoldingLaser())
        {
            if (PlayerPrefs.GetInt("Sound", 0) == 1) { LaserAudio.Play(); }
            return Instantiate(laserBeamPrefab, transform.position, Quaternion.identity);
        }
        else if (player.isHoldingFlamethrower())
        {
            if (PlayerPrefs.GetInt("Sound", 0) == 1) { FlamethrowerAudio.Play(); }
            return Instantiate(firePrefab, transform.position, Quaternion.identity);
        }
        else if (player.isHoldingGun())
        {
            if (PlayerPrefs.GetInt("Sound", 0) == 1) { GunAudio.Play(); }
            return Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            if (PlayerPrefs.GetInt("Sound", 0) == 1) { BoomerangAudio.Play(); }
            return Instantiate(boomerangPrefab, transform.position, Quaternion.identity);
        }
    }
}