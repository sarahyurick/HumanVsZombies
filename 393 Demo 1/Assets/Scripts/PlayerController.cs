using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Input settings:")]
    public int playerId = 0;
    private Player player;
    private CrossHairObject crossHairObject;

    [Space]
    [Header("Character attributes:")]
    public float MOVEMENT_BASE_SPEED = 1.0f;
    public float AMMO_BASE_SPEED = 5.0f;
    public float CROSSHAIR_DISTANCE = 0.4f;

    [Space]
    [Header("Character statistics:")]
    Vector3 movement;
    public float movementSpeed;
    Vector3 aim;
    bool isAiming;
    bool endOfAiming;
    public PlayerHealthBar playerHealthBar;

    [Space]
    [Header("Weapon statistics:")]
    bool holdingWeapon;
    float weaponHealth = 3;
    bool holdingLaser;
    bool holdingFlamethrower;
    bool holdingGun;
    bool holdingSlingshot;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject crossHair;

    [Space]
    [Header("Prefabs:")]
    public GameObject bulletPrefab;

    void Awake()
    {
        player = new Player();
        playerHealthBar.SetMaxHealth(player.MAX_HEALTH);
        holdingWeapon = false;
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
        playerHealthBar.SetHealth(player.currentHealth);
        holdingWeapon = player.holdingWeapon;
        if(player.IsDead())
        {
            Destroy(gameObject);
            StartCoroutine(DeathRoutine());
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    private void ProcessInputs()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        if (holdingWeapon) {
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
        animator.SetBool("holdingWeapon", holdingWeapon);
    }

    private void Move()
    {
        // transform.position = transform.position + movement * Time.deltaTime;
        rb.velocity = new Vector2(movement.x, movement.y) * MOVEMENT_BASE_SPEED;
        player.UpdatePosition(rb.position.x, rb.position.y);
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
            if (endOfAiming && holdingWeapon)
            {
                GameObject ammo = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Ammo ammoScript = ammo.GetComponent<Ammo>();
                ammoScript.velocity = shootingDirection * AMMO_BASE_SPEED;
                // bullet.GetComponent<Bullet>().velocity = shootingDirection * BULLET_BASE_SPEED;
                ammoScript.spartan = gameObject;
                ammo.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
                Destroy(ammo, 2.0f);

                weaponHealth--;
                if(weaponHealth <= 0) { 
                    holdingWeapon = false;
                    player.holdingWeapon = false;
                }
            }
        }
        else
        {
            crossHair.SetActive(false);
        }
    }

    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(500);
    }
}

public class Player
{
    public Vector3 currentPosition;
    public int MAX_HEALTH = 3;
    public int currentHealth = 3;
    public bool holdingWeapon = false;

    public void UpdatePosition(float x, float y)
    {
        currentPosition = new Vector3(x, y, 0.0f);
    }

    public void HandleCollision(string tag)
    {
        if (tag == "Zombies")
        {
            UpdatePlayerHealth();
        }
        if (tag == "Buildings")
        {
            holdingWeapon = !holdingWeapon;
        }
    }

    public void UpdatePlayerHealth()
    {
        currentHealth--;
    }

    public bool IsDead()
    {
        if(currentHealth <= 0) { return true;  }
        return false;
    }

    public void SavePlayer ()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer ()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        currentHealth = data.health;
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