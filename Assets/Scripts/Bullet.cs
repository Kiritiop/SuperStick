using UnityEngine;

// Aggregation: Bullet is fired by Gun but lives independently
public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 15f;
    public int damage = 20;
    public float lifetime = 3f;

    private Vector2 direction;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Called by Gun right after instantiating
    public void Init(Vector2 dir)
    {
        direction = dir;
        rb.linearVelocity = direction * speed;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Don't hit the player who fired (handled by layer collision)
        PlayerHealth health = other.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.TakeDamage(damage);
            Destroy(gameObject);
        }

        // Destroy on hitting ground or walls
        if (other.CompareTag("Ground"))
            Destroy(gameObject);
    }
}