using UnityEngine;
using UnityEngine.InputSystem;

public class GunFireRate : MonoBehaviour
{

    public float newFireRate = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.GetComponent<Gun>().fireRate = newFireRate;
    }
 
}
