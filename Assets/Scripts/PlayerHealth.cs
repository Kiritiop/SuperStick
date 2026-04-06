using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;
    [SerializeField] private AudioClip HurtSFX;

    // Other scripts can listen to these events
    public UnityEvent onDeath;
    public UnityEvent<int> onHealthChanged; // passes current health

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        SoundEffectManager.instance.PlaySoundEffect(HurtSFX, transform, 0.2f);

        onHealthChanged.Invoke(currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        onDeath.Invoke();
        gameObject.SetActive(false);
    }

    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        onHealthChanged.Invoke(currentHealth);
    }
}