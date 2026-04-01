using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public enum InputMode { Keyboard, Gamepad }

    [Header("Input Mode")]
    public InputMode inputMode = InputMode.Keyboard;

    [Header("Gun Settings")]
    public GameObject[] bulletPrefab;
    public GameObject playerParent = null;
    public Transform firePoint;
    public float fireRate = 0.3f;
    
    [Header("Input")]
    public Key shootKey;

    private float nextFireTime = 0f;
    private Vector2 direction = Vector2.right;

    [SerializeField] private AudioClip ShootSFX;


    private void Start()
    {
        firePoint = transform.GetChild(0).gameObject.transform;
    }

    void Update()
    {

        if(playerParent != null)
        {
            if(playerParent.GetComponent<PlayerMovement>().directionFacing == "right")
            {
                this.transform.rotation = Quaternion.Euler(0,180,0); 
            } 
            else if (playerParent.GetComponent<PlayerMovement>().directionFacing == "left")
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if (playerParent != null)
        {

            if (inputMode == InputMode.Gamepad)
            {
                if (Gamepad.current != null)
                {
                    if (Gamepad.current.rightTrigger.wasPressedThisFrame && Time.time >= nextFireTime)
                    {
                        Shoot();
                        nextFireTime = Time.time + fireRate;
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

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(playerParent == null && collision.gameObject.CompareTag("Player"))
        {
            playerParent = collision.gameObject;
            this.transform.SetParent(playerParent.transform, false);
            this.transform.position = playerParent.transform.position;
            Destroy(this.GetComponent<Collider2D>());
            this.inputMode = (Gun.InputMode)playerParent.GetComponent<PlayerMovement>().inputMode;
            this.shootKey = playerParent.GetComponent<PlayerMovement>().shootKey;
            
        }
    }
    
}