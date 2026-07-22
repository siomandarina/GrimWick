using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 10;
    public GameObject floatingTextPrefab;
    public AudioClip pickupSound; 

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth ph = collision.GetComponent<PlayerHealth>();

            if (ph != null)
            {
                ph.Heal(healAmount);

                if (floatingTextPrefab != null)
                {
                    GameObject ft = Instantiate(
                        floatingTextPrefab,
                        transform.position + Vector3.up * 0.5f,
                        Quaternion.identity
                    );
                    ft.GetComponent<FloatingText>().SetText("+" + healAmount, Color.green);
                }

                if (pickupSound != null)
                {
                    AudioSource.PlayClipAtPoint(pickupSound, transform.position);
                }

                Destroy(gameObject);
            }
        }
    }
}