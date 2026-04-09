using UnityEngine;

public class Sniper : Gun
{
    void Awake()
    {
        ammo = 1;
        damage = 100;
        bulletSpeed = 40f;
        fireRate = 1.5f;
    }
}