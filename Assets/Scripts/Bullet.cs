using UnityEngine;

// Aggregation: Bullet is fired by Gun but lives independently
public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed; 
    public int damage; 
    public float lifetime = 3f; 

    private Vector2 direction;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().sortingOrder = 2;
    }

    public void Init(Vector2 dir, int dmg, float spd)
    {
        damage = dmg;
        speed = spd;
        direction = dir;
        rb.linearVelocity = dir * speed; // SPEED RECIEVED FROM GUN CLASS
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Don't hit the player who fired
        PlayerHealth health = other.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.TakeDamage(damage); // DAMAGE RECIEVED FROM GUN CLASS
            Destroy(gameObject);
        }

        // Destroy bullets upon hitting the ground/walls
        if (other.CompareTag("Ground"))
            Destroy(gameObject);
    }
}