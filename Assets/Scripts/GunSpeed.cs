using UnityEngine;

public class GunSpeed : MonoBehaviour
{

    public GameObject bulletSpeedPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        for (int i = 0; i < GetComponent<Gun>().bulletPrefab.Length; i++)
        {
            this.GetComponent<Gun>().bulletPrefab[i] = bulletSpeedPrefab;
        }
        

    }

}
