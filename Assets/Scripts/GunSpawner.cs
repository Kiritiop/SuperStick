using UnityEngine;

public class GunSpawner : MonoBehaviour
{
    public GameObject pistolPrefab;
    public GameObject riflePrefab;
    public GameObject sniperPrefab;
    public GameObject shotgunPrefab;
    private float spawnRate = 5f;
    private float timer;

    void Start()
    {
        timer = spawnRate;
    }

    void Update()
    {
        if (!GameManager.getGameOver() && !GameManager.getIsPaused())
        {
            if (timer > 0)
                timer -= Time.deltaTime;
            else
            {
                SpawnGun();
                timer = spawnRate;
            }
        }
    }

    void SpawnGun()
    {
        // Find all ground objects correctly
        GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("Ground");

        if (groundObjects.Length == 0)
        {
            Debug.LogWarning("No ground objects found!");
            return;
        }

        // Pick a random platform safely
        GameObject platform = groundObjects[Random.Range(0, groundObjects.Length)];

        // Get the top of the platform using its collider
        float spawnY = platform.transform.position.y;
        Collider2D col = platform.GetComponent<Collider2D>();
        if (col != null)
            spawnY = col.bounds.max.y + 0.5f;

        Vector3 spawnPos = new Vector3(platform.transform.position.x, spawnY, 0);

        int gunType = Random.Range(0, 3);

        // if (gunType == 0)
        // {
        //     Instantiate(gun, spawnPos, Quaternion.identity);
        // }
        // else if (gunType == 1)
        // {
        //     GameObject goon = Instantiate(gun, spawnPos, Quaternion.identity);
        //     goon.AddComponent<GunFireRate>();
        //     goon.GetComponent<SpriteRenderer>().color = Color.red;
        // }
        // else if (gunType == 2)
        // {
        //     GameObject goon = Instantiate(gun, spawnPos, Quaternion.identity);
        //     goon.AddComponent<GunSpeed>();
        //     goon.GetComponent<GunSpeed>().bulletSpeedPrefab = fastBullet;
        //     goon.GetComponent<SpriteRenderer>().color = Color.green;
        // }

                GameObject[] weaponTypes = { pistolPrefab, riflePrefab, sniperPrefab, shotgunPrefab };
        GameObject chosenWeapon = weaponTypes[Random.Range(0, weaponTypes.Length)];

        Instantiate(chosenWeapon, spawnPos, Quaternion.identity);
    }
}