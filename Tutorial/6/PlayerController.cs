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
    public Animator animator;
    public GameObject crossHair;

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        // Vector3 movement = new Vector3(Input.GetAxis("MoveHorizontal"), Input.GetAxis("MoveVertical"), 0.0f);

        if (Input.GetButton("Fire1")) {
            Debug.Log("FIRE!");
        }

        MoveCrossHair();

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        transform.position = transform.position + movement * Time.deltaTime;

        /*
        // wasd version
        Vector3 pos = transform.position;
        if (Input.GetKey ("w")) { pos.y += speed * Time.deltaTime; }
        if (Input.GetKey ("s")) { pos.y -= speed * Time.deltaTime; }
        if (Input.GetKey ("d")) { pos.x += speed * Time.deltaTime; }
        if (Input.GetKey ("a")) { pos.x -= speed * Time.deltaTime; }
        transform.position = pos;
        */

    }

    private void MoveCrossHair()
    {
        Vector3 aim = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);

        if(aim.magnitude > 0.0f)
        {
            aim.Normalize();
            // aim *= 0.4f;
            crossHair.transform.localPosition = aim;
            crossHair.SetActive(true);
        } else
        {
            crossHair.SetActive(false);
        }
    }
}
