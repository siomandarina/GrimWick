using UnityEngine;

public class CameraRoomBounds : MonoBehaviour
{
    public Collider2D roomArea; // rectángulo que cubre todo el cuarto (paredes incluidas)
    public CameraFollow cameraFollow;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraFollow.SetBounds(roomArea);
        }
    }
}
