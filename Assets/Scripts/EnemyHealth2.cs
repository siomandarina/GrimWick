using UnityEngine;

public class EnemyHealth2 : MonoBehaviour
{
    public int health = 5;

    [Header("Split (opcional)")]
    public bool canSplit = false;
    public GameObject splitPrefab; 
    public int splitCount = 2;
    public float splitRadius = 1f;
    public int splitHealth = 3;

    [HideInInspector] public EnemyZone assignedZone;
    public event System.Action onDeath;

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
        if (canSplit && splitPrefab != null)
        {
            for (int i = 0; i < splitCount; i++)
            {
                Vector3 spawnPos;

                if (assignedZone != null)
                {
                    spawnPos = assignedZone.GetRandomPointInArea();
                }
                else
                {
                    Vector2 offset = Random.insideUnitCircle * splitRadius;
                    spawnPos = transform.position + (Vector3)offset;
                }

                GameObject child = Instantiate(splitPrefab, spawnPos, Quaternion.identity);

                EnemyHealth2 eh = child.GetComponent<EnemyHealth2>();
                if (eh != null)
                {
                    eh.canSplit = false;
                    eh.health = splitHealth;

                    if (assignedZone != null)
                        assignedZone.RegisterEnemy(child);
                }

                EnemyFollow follow = child.GetComponent<EnemyFollow>();
                if (follow != null)
                {
                    follow.followPlayer = true;
                    follow.ActivateEnemy();
                }
            }
        }

        onDeath?.Invoke();
        Destroy(gameObject);
    }
}
