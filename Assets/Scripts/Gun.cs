using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [Header("Gun Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.3f;
    

    [Header("Input")]
    public Key shootKey;

    private float nextFireTime = 0f;

    void Update()
    {
        

        if (Keyboard.current[shootKey].isPressed && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        PlayerMovement pm = GetComponentInParent<PlayerMovement>();
        Vector2 direction = Vector2.left;
        if(pm.directionFacing == "left")
        {
            direction = Vector2.left;
            
        }
        if(pm.directionFacing == "right")
        {
            direction = Vector2.right;
        }
        
        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet = bulletObj.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Init(direction);
        }
    }
}