using UnityEditor.Rendering;
using UnityEngine;
using System.Collections;
using UnityEditor.ShaderGraph.Internal;

public class GunSpawner : MonoBehaviour
{

    public GameObject gun;
    public GameObject fastBullet;
    private float spawnRate = 5f; //how many seconds per gun
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = spawnRate;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.getGameOver() == false && GameManager.getIsPaused() == false)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            } else
            {
                spawnGun();
                timer = spawnRate;
            }
        }
    }

    void spawnGun()
    {
        GameObject[] allObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        GameObject[] groundObjects = new GameObject[allObjects.Length];
        int groundObjectsIndex = 0;

        for (int i = 0; i < allObjects.Length; i++)
        {
            if (allObjects[i].CompareTag("Ground"))
            {
                groundObjects[groundObjectsIndex] = allObjects[i];
            }
        }

        GameObject platform = null;
        while (true)
        {
            if (platform == null)
            {
                platform = groundObjects[Random.Range(0, groundObjects.Length - 1)];
            }
            else
            {
                break;
            }
        }

        int gunType = Random.Range(0, 3);

        if (gunType == 0) //normal gun, no modifiers
        {
            Instantiate(gun, platform.transform.position + new Vector3(0, 10, 0), Quaternion.identity);
        }
        else if (gunType == 1) //attach GunFireRate script to gun
        {
            GameObject goon = Instantiate(gun, platform.transform.position + new Vector3(0, 10, 0), Quaternion.identity);
            goon.AddComponent<GunFireRate>();
            goon.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (gunType == 2) //attach GunSpeed script to gun
        {
            GameObject goon = Instantiate(gun, platform.transform.position + new Vector3(0, 10, 0), Quaternion.identity);
            goon.AddComponent<GunSpeed>();
            goon.GetComponent<GunSpeed>().bulletSpeedPrefab = fastBullet;
            goon.GetComponent<SpriteRenderer>().color = Color.green;
        }

    }

    

}
