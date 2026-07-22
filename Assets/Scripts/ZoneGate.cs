using UnityEngine;

public class ZoneGate : MonoBehaviour
{
    public GameObject doorVisual;       
    public Collider2D blockingCollider; 

    public void Close()
    {
        if (doorVisual != null) doorVisual.SetActive(true);
        if (blockingCollider != null) blockingCollider.enabled = true;
    }

    public void Open()
    {
        if (doorVisual != null) doorVisual.SetActive(false);
        if (blockingCollider != null) blockingCollider.enabled = false;
    }
}