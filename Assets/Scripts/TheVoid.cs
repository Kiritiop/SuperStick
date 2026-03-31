using System;
using UnityEngine;

public class TheVoid : MonoBehaviour
{ 

    void Update()
    {   
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth health = other.GetComponent<PlayerHealth>();
        health.TakeDamage(health.GetCurrentHealth());
    }

}
