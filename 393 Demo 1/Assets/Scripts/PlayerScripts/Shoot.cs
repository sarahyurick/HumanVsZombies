using UnityEngine;

public class Shoot
{
    public Vector2 CalculateDirection(Vector3 aim)
    {
        Vector2 shootingDirection = new Vector2(aim.x, aim.y);
        shootingDirection.Normalize();
        return shootingDirection;
    }

    // Returns the ammo left afterwards
    public int ShootWeapon(Player player)
    {
        player.currentWeapon.UseWeapon();

        if (!player.currentWeapon.HasAmmo())
        {
            player.DropWeapon();
            return 0;
        }
        else
        {
            return player.currentWeapon.GetCurrentAmmo();
        }
    }
}
