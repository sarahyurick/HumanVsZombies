using UnityEngine;

public class Movement
{
    public float MOVEMENT_BASE_SPEED;

    public Movement(float speed)
    {
        MOVEMENT_BASE_SPEED = speed;
    }

    public Movement()
    {
        MOVEMENT_BASE_SPEED = 3f;
    }

    public Vector3 Calculate(float h, float v, float deltaTime)
    {
        var x = h * MOVEMENT_BASE_SPEED;
        var y = v * MOVEMENT_BASE_SPEED;

        return new Vector3(x, y, 0);
    }

    public bool CheckMovementStatus(string gameObjectTag, string hitBodyType)
    {
        if (gameObjectTag == "Buildings" && hitBodyType == "Kinematic")
        {
            return true;
        }
        if (gameObjectTag == "MovableObject" && hitBodyType == "Dynamic")
        {
            return true;
        }
        if (gameObjectTag == "Border" && hitBodyType == "Kinematic")
        {
            return true;
        }
        if (gameObjectTag == "Zombies" && hitBodyType == "Dynamic")
        {
            return true;
        }
        if (gameObjectTag == "Player" && hitBodyType == "Dynamic")
        {
            return true;
        }
        return false;
    }

    public bool IsValid(string o1, string o2)
    {
        if(o1 == "Player")
        {
            if(o2 == "Zombies" || o2 == "MovableObject")
            {
                return true;
            }
            return false;
        } else if (o1 == "Zombies")
        {
            if(o2 == "MovableObject")
            {
                return true;
            }
            return false;
        }
        return false;
    }
}
