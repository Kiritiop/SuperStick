using UnityEditor.Rendering;
using UnityEngine;
using System.Collections;

public class GunSpawner : MonoBehaviour
{

    public GameObject gun;
    public float spawnRate = 10000f; //in seconds

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameOver == false && GameManager.isPaused == false)
        {
            spawnGun();
            StartCoroutine(Wait(spawnRate));
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

        Instantiate(gun, platform.transform.position + new Vector3(0,10,0), Quaternion.identity);

    }

    public IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Debug.Log("SDJFLKSDJF");
    }

}
