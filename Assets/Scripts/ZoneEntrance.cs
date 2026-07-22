using UnityEngine;

public class ZoneEntrance : MonoBehaviour
{
    public EnemyZone zone;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            zone.ActivateZone();
    }
}