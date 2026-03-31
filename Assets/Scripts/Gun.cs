using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public enum InputMode { Keyboard, Gamepad }

    [Header("Input Mode")]
    public InputMode inputMode = InputMode.Keyboard;

    [Header("Gun Settings")]
    public GameObject[] bulletPrefab;
    public GameObject playerParent;
    public Transform firePoint;
    public float fireRate = 0.3f;
    
    [Header("Input")]
    public Key shootKey;

    private float nextFireTime = 0f;
    private Vector2 direction = Vector2.right;

    [SerializeField] private AudioClip ShootSFX;

    void Update()
    {
        if (inputMode == InputMode.Gamepad)
        {
            if (Gamepad.current != null)
            {
                if (Gamepad.current.rightTrigger.wasPressedThisFrame && Time.time >= nextFireTime)
                {
                    nextFireTime = Time.time + fireRate;
                    Shoot();
                    SoundEffectManager.instance.PlaySoundEffect(ShootSFX, transform, 0.1f);
                }
            }
        }

        if (inputMode == InputMode.Keyboard)
        {
            if (Keyboard.current[shootKey].isPressed && Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
                SoundEffectManager.instance.PlaySoundEffect(ShootSFX, transform, 0.1f);
            }
        }
    }

    void Shoot()
    {
        
        int index = Random.Range(0, bulletPrefab.Length);
        if (bulletPrefab == null || firePoint == null) return;

        if (inputMode == InputMode.Keyboard)
        {
            Vector2 mouseScreen = Mouse.current.position.ReadValue();
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
            mouseWorld.z = 0;

            direction = ((Vector2)mouseWorld - (Vector2)transform.position).normalized;
        }
    
        else if (inputMode == InputMode.Gamepad)
        {
            if (Gamepad.current != null)
            {
                Vector2 rightStick = Gamepad.current.rightStick.ReadValue();
                Debug.Log(rightStick);

                if (rightStick.magnitude > 0.2f)
                    direction = rightStick.normalized;
            }
        }

        Vector2 spawnPos = (Vector2)transform.position + direction * 1f;
        GameObject bulletObj = Instantiate(bulletPrefab[index], spawnPos, Quaternion.identity);
        Bullet bullet = bulletObj.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Init(direction);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("SDJFKLSD");
        if(playerParent == null && collision.gameObject.CompareTag("Player"))
        {
            playerParent = collision.gameObject;
            this.transform.SetParent(playerParent.transform, false);
        }
    }

}