using UnityEngine;

// Used https://www.youtube.com/watch?v=MFQhpwc6cKE
// 

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    /*
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smootedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }*/
}
