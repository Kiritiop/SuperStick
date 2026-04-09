using UnityEngine;

public class Rifle : Gun
{
    void Awake()
    {
        ammo = 20;
        damage = 5;
        bulletSpeed = 20f;
        fireRate = 0.1f;
    }
}