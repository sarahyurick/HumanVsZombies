// Originally from https://answers.unity.com/questions/552674/make-a-character-walk-around-randomly.html
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieMovement : MonoBehaviour
{
    private float timeToChangeDirection;
    public float MOVEMENT_BASE_SPEED = 0.75f;

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

        rb.velocity = transform.up * MOVEMENT_BASE_SPEED;
    }



    private void ChangeDirection()
    {
        float angle = Random.Range(0f, 360f);
        Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);
        Vector3 newUp = quat * Vector3.up;
        newUp.z = 0;
        newUp.Normalize();
        transform.up = newUp;
        timeToChangeDirection = 1.5f;
    }
}
