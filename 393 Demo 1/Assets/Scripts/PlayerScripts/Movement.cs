using UnityEngine;

public class Movement
{
    public float MOVEMENT_BASE_SPEED;

    public Movement(float speed)
    {
        MOVEMENT_BASE_SPEED = speed;
    }

    public Vector3 Calculate(float h, float v, float deltaTime)
    {
        var x = h * MOVEMENT_BASE_SPEED * deltaTime;
        var y = v * MOVEMENT_BASE_SPEED * deltaTime;

        return new Vector3(x, y, 0);
    }
}
