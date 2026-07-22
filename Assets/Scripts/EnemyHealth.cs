using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 5;

    public GameObject enemyPrefab;

    public int spawnCount = 2;
    public float spawnRadius = 2f;

    public bool canSpawn = true;

    // 🔥 NUEVO: vida de enemigos spawnados
    public int spawnedEnemyHealth = 3;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (canSpawn)
        {
            SpawnEnemies();
        }

        Destroy(gameObject);
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);

            GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            EnemyHealth eh = newEnemy.GetComponent<EnemyHealth>();

            if (eh != null)
            {
                eh.canSpawn = false;              // no más spawn
                eh.health = spawnedEnemyHealth;  
            }

            EnemyFollow follow = newEnemy.GetComponent<EnemyFollow>();
            if (follow != null)
            {
                follow.followPlayer = true;
                follow.ActivateEnemy();
                follow.neverStop = true;
            }
        }
    }
}