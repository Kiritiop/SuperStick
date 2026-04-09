using UnityEngine;

public class Pistol : Gun
{
    void Awake()
    {
        ammo = 7;
        damage = 15;
        bulletSpeed = 15f;
        fireRate = 0.4f;
    }
}