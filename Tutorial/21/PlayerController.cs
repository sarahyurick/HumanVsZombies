using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    /*
    void Start()
    {

    }
    */

    [Header("Input settings:")]
    public int playerId = 0;
    // private Player player;
    // public bool useController;

    [Space]
    [Header("Character attributes:")]
    public float MOVEMENT_BASE_SPEED = 1.0f;
    public float ARROW_BASE_SPEED = 3.0f;
    public float CROSSHAIR_DISTANCE = 0.4f;

    [Space]
    [Header("Character statistics:")]
    Vector3 movement;
    // public Vector2 movementDirection;
    public float movementSpeed;
    Vector3 aim;
    bool isAiming;
    bool endOfAiming;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    // public Animator topAnimator;
    // public Animator bottomAnimator;
    public Animator animator;
    public GameObject crossHair;

    [Space]
    [Header("Prefabs:")]
    public GameObject arrowPrefab;

    void Awake()
    {
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

    private void ProcessInputs()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        // Vector3 movement = new Vector3(Input.GetAxis("MoveHorizontal"), Input.GetAxis("MoveVertical"), 0.0f);

        // movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        // movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        // movementDirection.Normalize();
        Vector3 mouseMovement = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
        aim = aim + mouseMovement;
        if(aim.magnitude > 1.0f)
        {
            aim.Normalize();
        }

        // aim = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
        isAiming = Input.GetButton("Fire1");
        endOfAiming = Input.GetButtonUp("Fire1");

        if(movement.magnitude > 1.0f)
        {
            movement.Normalize();
        }
    }

    private void Animate()
    {
        if(movement != Vector3.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        animator.SetFloat("Magnitude", movement.magnitude);

        if(aim != Vector3.zero)
        {
            animator.SetFloat("AimHorizontal", aim.x);
            animator.SetFloat("AimVertical", aim.y);
        }
        animator.SetFloat("AimMagnitude", aim.magnitude);
        animator.SetBool("Aim", isAiming);
    }

    private void Move()
    {
        // transform.position = transform.position + movement * Time.deltaTime;

        /*
        // wasd version
        Vector3 pos = transform.position;
        if (Input.GetKey ("w")) { pos.y += speed * Time.deltaTime; }
        if (Input.GetKey ("s")) { pos.y -= speed * Time.deltaTime; }
        if (Input.GetKey ("d")) { pos.x += speed * Time.deltaTime; }
        if (Input.GetKey ("a")) { pos.x -= speed * Time.deltaTime; }
        transform.position = pos;
        */

        rb.velocity = new Vector2(movement.x, movement.y);
        // rb.velocity = movementDirection * movementSpeed * MOVEMENT_BASE_SPEED;
    }

    private void AimAndShoot()
    {
        // aim = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
        Vector2 shootingDirection = new Vector2(aim.x, aim.y);

        if(aim.magnitude > 0.0f)
        {
            // aim.Normalize();
            crossHair.transform.localPosition = aim * CROSSHAIR_DISTANCE;
            crossHair.SetActive(true);

            shootingDirection.Normalize();
            if (endOfAiming)
            {
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                Arrow arrowScript = arrow.GetComponent<Arrow>();
                arrowScript.velocity = shootingDirection * ARROW_BASE_SPEED;
                // arrow.GetComponent<Arrow>().velocity = shootingDirection * ARROW_BASE_SPEED;
                arrowScript.archer = gameObject;
                arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
                Destroy(arrow, 2.0f);
            }
        } else
        {
            crossHair.SetActive(false);
        }
    }
}
