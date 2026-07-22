using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 1;
    public float attackCooldown = 1f;

    private bool canAttack = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!canAttack) return;

        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();

            if (player != null)
            {
                Debug.Log("Voy a quitar " + damage + " de vida");

                player.TakeDamage(damage);

                StartCoroutine(AttackCooldown());
            }
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }
}