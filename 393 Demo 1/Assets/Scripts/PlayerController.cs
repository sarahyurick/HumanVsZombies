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
        if(player.IsDead())
        {
            Destroy(gameObject);
        }
    }

    private void ProcessInputs()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

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
            }
        }
        else
        {
            crossHair.SetActive(false);
        }
    }


}

public class Player
{
    public Vector3 currentPosition;
    public int Health = 3;

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