using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 9f;
    public Vector2 direction;

    bool hasHit = false;

    void Start()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

void OnTriggerEnter2D(Collider2D collision)
{
    if (hasHit) return;

    if (collision.CompareTag("Enemy"))
    {
        hasHit = true;

        EnemyHealth2 enemy = collision.GetComponentInParent<EnemyHealth2>(); 

        if (enemy != null)
        {
            enemy.TakeDamage(1);
        }

        EnemyFollow follow = collision.GetComponentInParent<EnemyFollow>();

        if (follow != null)
        {
            follow.ActivateEnemy();
        }

        Destroy(gameObject);
    }

    if (collision.CompareTag("Wall"))
    {
        hasHit = true;
        Destroy(gameObject);
    }
}
}