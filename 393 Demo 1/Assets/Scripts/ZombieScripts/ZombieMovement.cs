// Originally from https://answers.unity.com/questions/552674/make-a-character-walk-around-randomly.html
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieMovement : MonoBehaviour
{
    public Zombie zombie;

    private float timeToChangeDirection;
    public float MOVEMENT_BASE_SPEED = 0.75f;

    Vector3 movement;
    public Animator animator;
    public Rigidbody2D rb;

    void Awake()
    {
        zombie = new Zombie();
    }

    // Use this for initialization
    public void Start()
    {
        movement = zombie.ChangeDirection();
        timeToChangeDirection = 1.5f;
    }

    // Update is called once per frame
    public void Update()
    {
        if(zombie.IsDead())
        {
            Destroy(gameObject);
        }
        timeToChangeDirection -= Time.deltaTime;

        if (timeToChangeDirection <= 0)
        {
            movement = zombie.ChangeDirection();
            timeToChangeDirection = 1.5f;
        }

        Animate();
        rb.velocity = new Vector2(movement.x, movement.y) * MOVEMENT_BASE_SPEED;
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
}