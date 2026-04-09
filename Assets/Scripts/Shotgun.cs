using UnityEngine;

public class Shotgun : Gun
{
    void Awake()
    {
        ammo = 9;
        damage = 30;
        bulletSpeed = 12f;
        fireRate = 0.8f;
    }
}