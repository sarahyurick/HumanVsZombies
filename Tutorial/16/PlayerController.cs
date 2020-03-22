using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.5f;

    // Start is called before the first frame update
    /*
    void Start()
    {

    }
    */

    public int playerId = 0;
    // private Player player;
    // public bool useController;

    // public Animator topAnimator;
    // public Animator bottomAnimator;
    public Animator animator;
    public GameObject crossHair;

    public Rigidbody2D rb;

    public GameObject arrowPrefab;

    Vector3 movement;
    Vector3 aim;
    bool isAiming;
    bool endOfAiming;

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
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        animator.SetFloat("AimHorizontal", aim.x);
        animator.SetFloat("AimVertical", aim.y);
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
    }

    private void AimAndShoot()
    {
        // aim = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
        Vector2 shootingDirection = new Vector2(aim.x, aim.y);

        if(aim.magnitude > 0.0f)
        {
            // aim.Normalize();
            crossHair.transform.localPosition = aim * 0.4f;
            crossHair.SetActive(true);

            shootingDirection.Normalize();
            if (endOfAiming)
            {
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection * 3.0f;
                arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
                Destroy(arrow, 2.0f);
            }
        } else
        {
            crossHair.SetActive(false);
        }
    }
}
