using UnityEngine;

public class Aim
{
    public float CROSSHAIR_DISTANCE = 1f;

    public Vector3 AimDirection(Vector3 aim, float h, float v, float z)
    {
        Vector3 mouseMovement = new Vector3(h, v, z);
        Vector3 updatedAim = aim + mouseMovement;
        if (updatedAim.magnitude > 1.0f)
        {
            updatedAim.Normalize();
        }
        return updatedAim * CROSSHAIR_DISTANCE;
    }
}
