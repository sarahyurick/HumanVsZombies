﻿// Originally from https://answers.unity.com/questions/552674/make-a-character-walk-around-randomly.html
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieMovement : MonoBehaviour
{
    private float timeToChangeDirection;
    public float MOVEMENT_BASE_SPEED = 0.75f;

    Vector3 movement;
    public Animator animator;
    public Rigidbody2D rb;

    // Use this for initialization
    public void Start()
    {
        ChangeDirection();
    }

    // Update is called once per frame
    public void Update()
    {
        timeToChangeDirection -= Time.deltaTime;

        if (timeToChangeDirection <= 0)
        {
            ChangeDirection();
        }

        Animate();
        rb.velocity = new Vector2(movement.x, movement.y) * MOVEMENT_BASE_SPEED;
    }



    private void ChangeDirection()
    {
        float[] directions = { -1.0f, 0.0f, 1.0f };
        int choiceIndex = Random.Range(0, directions.Length);
        float x = directions[choiceIndex];
        choiceIndex = Random.Range(0, directions.Length);
        float y = directions[choiceIndex];
        movement = new Vector3(x, y, 0.0f);
        timeToChangeDirection = 1.5f;
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