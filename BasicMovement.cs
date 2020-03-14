using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float speed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 arrow = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        arrow = speed * arrow * Time.deltaTime;
        transform.position = transform.position + arrow;

        // wasd version
        Vector3 pos = transform.position;
        if (Input.GetKey ("w")) { pos.y += speed * Time.deltaTime; }
        if (Input.GetKey ("s")) { pos.y -= speed * Time.deltaTime; }
        if (Input.GetKey ("d")) { pos.x += speed * Time.deltaTime; }
        if (Input.GetKey ("a")) { pos.x -= speed * Time.deltaTime; }
        transform.position = pos;

    }
}
