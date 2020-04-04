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
    public float BULLET_BASE_SPEED = 3.0f;
    public float CROSSHAIR_DISTANCE = 0.4f;

    [Space]
    [Header("Character statistics:")]
    Vector3 movement;
    public float movementSpeed;
    Vector3 aim;
    bool isAiming;
    bool endOfAiming;

    [Space]
    [Header("Weapon statistics:")]
    bool holdingWeapon;
    float weaponHealth = 3;
    bool holdingLaser;
    bool holdingFlamethrower;
    bool holdingGun;
    bool holdingSlingshot;

    [Space]
    [Header("Player health bar:")]
    public float barDisplay; //current progress
    public Vector2 pos = new Vector2(20, 40);
    public Vector2 size = new Vector2(60, 20);
    public Texture2D emptyTex;
    public Texture2D fullTex;

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
        barDisplay = player.barDisplay;
        holdingWeapon = true; // TODO: starts false
        crossHairObject = new CrossHairObject();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        if (holdingWeapon) { AimAndShoot(); }
        Animate();
        Move();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        string gameObjectTag = coll.gameObject.tag;
        player.HandleCollision(gameObjectTag);
        barDisplay = player.barDisplay;
        if(player.IsDead())
        {
            Destroy(gameObject);
            StartCoroutine(DeathRoutine());
            Application.Quit();
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
    }

    void OnGUI()
    {
        //draw the background:
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), emptyTex);

        //draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), fullTex);
        GUI.EndGroup();
        GUI.EndGroup();
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
            if (endOfAiming)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.velocity = shootingDirection * BULLET_BASE_SPEED;
                // bullet.GetComponent<Bullet>().velocity = shootingDirection * BULLET_BASE_SPEED;
                bulletScript.spartan = gameObject;
                bullet.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
                Destroy(bullet, 2.0f);

                weaponHealth--;
                if(weaponHealth <= 0) { holdingWeapon = false; }
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
    public int Health = 3;
    public float barDisplay = 3;

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
    }

    public void UpdatePlayerHealth()
    {
        Health--;
        barDisplay = Health/3f;
    }

    public bool IsDead()
    {
        if(Health <= 0) { return true;  }
        return false;
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