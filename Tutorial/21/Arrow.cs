using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public Vector2 velocity = new Vector2(0.0f, 0.0f);
    public GameObject archer;
    public Vector2 offset = new Vector2(0.0f, 0.0f);

    void Update()
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition + velocity * Time.deltaTime;

        Debug.DrawLine(currentPosition + offset, newPosition + offset, Color.red);

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition + offset, newPosition + offset);

        foreach (RaycastHit2D hit in hits)
        {
            GameObject other = hit.collider.gameObject;
            if(other != archer)
            {
                if(other.CompareTag("Zombies"))
                {
                    Destroy(gameObject);
                    Destroy(other);
                    Debug.Log(other.name);
                    // Debug.Log(hit.collider.gameObject);
                    break;
                }

                if(other.CompareTag("Trees"))
                {
                    Destroy(gameObject);
                    break;
                }
            }
        }

        transform.position = newPosition;
    }
}
