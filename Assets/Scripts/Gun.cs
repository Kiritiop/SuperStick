using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

// INHERITANCE

public class Gun : MonoBehaviour
{
    public enum InputMode { Keyboard, Gamepad }

    [Header("Input Mode")]
    public InputMode inputMode = InputMode.Keyboard;

    [Header("Gun Settings")]
    public GameObject[] bulletPrefab;
    public GameObject playerParent = null;
    public Transform firePoint; 
    protected float fireRate = 0.3f;
    protected int ammo = 10; 
    protected int damage = 20; 
    protected float bulletSpeed = 15f;  //???????????
    
    [Header("Input")]
    public Key shootKey;

    protected float nextFireTime = 0f;
    protected Vector2 direction = Vector2.right;

    [SerializeField] private AudioClip ShootSFX;
    [SerializeField] private AudioClip EquipSFX;


    private void Start()
    {
        firePoint = transform.GetChild(0).gameObject.transform;
        this.transform.GetChild(1).GetComponent<TMP_Text>().text = ammo.ToString();
    }

    protected virtual void Update()
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
                    if (Gamepad.current.rightTrigger.isPressed && Time.time >= nextFireTime && ammo > 0)
                    {
                        Shoot();
                        nextFireTime = Time.time + fireRate;
                        SoundEffectManager.instance.PlaySoundEffect(ShootSFX, transform, 0.1f);
                    }
                }
            }

            if (inputMode == InputMode.Keyboard)
            {
                if (Keyboard.current[shootKey].isPressed && Time.time >= nextFireTime && ammo > 0)
                {
                    Shoot();
                    nextFireTime = Time.time + fireRate;
                    Debug.Log(Time.time + " vs " + nextFireTime);
                    SoundEffectManager.instance.PlaySoundEffect(ShootSFX, transform, 0.1f);
                }
            }

            this.transform.GetChild(1).transform.rotation = Quaternion.Euler(0, 0, 0);

        }
    }

    protected virtual void Shoot()
    {
        
        int index = Random.Range(0, bulletPrefab.Length); //????
        
        if (bulletPrefab == null || bulletPrefab.Length == 0 || firePoint == null) return;

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
        ammo -= 1;

        this.transform.GetChild(1).GetComponent<TMP_Text>().text = ammo.ToString();

        if (bullet != null)
        {
            bullet.Init(direction, damage, bulletSpeed);
        }
        
    }

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(playerParent == null && collision.gameObject.CompareTag("Player"))
        {
            SoundEffectManager.instance.PlaySoundEffect(EquipSFX, transform, 1f);
            playerParent = collision.gameObject;

            foreach(Transform child in playerParent.GetComponentInChildren<Transform>())
            {
                if (child.gameObject.CompareTag("Gun"))
                {
                    Destroy(child.gameObject);
                }
            }

            this.transform.SetParent(playerParent.transform, false);
            this.transform.position = playerParent.transform.position;
            Destroy(this.GetComponent<Collider2D>());
            this.inputMode = (Gun.InputMode)playerParent.GetComponent<PlayerMovement>().inputMode;
            this.shootKey = playerParent.GetComponent<PlayerMovement>().shootKey;
            Destroy(this.GetComponent<Rigidbody2D>());
            this.transform.GetChild(1).GameObject().SetActive(true);

        }
    }

    public static void DestroyAllGuns()
    {
        GameObject[] allObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("Gun"))
            {
                Destroy(obj);
            }
        }
    }
    
}