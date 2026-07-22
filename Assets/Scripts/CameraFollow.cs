using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 0, -10);
    public float smoothSpeed = 5f;

    private Camera cam;
    private Bounds currentBounds;
    private bool hasBounds = false;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        if (hasBounds)
        {
            float camHalfHeight = cam.orthographicSize;
            float camHalfWidth = camHalfHeight * cam.aspect;

            float minX = currentBounds.min.x + camHalfWidth;
            float maxX = currentBounds.max.x - camHalfWidth;
            float minY = currentBounds.min.y + camHalfHeight;
            float maxY = currentBounds.max.y - camHalfHeight;

            float clampedX = (minX <= maxX) ? Mathf.Clamp(smoothPosition.x, minX, maxX) : currentBounds.center.x;
            float clampedY = (minY <= maxY) ? Mathf.Clamp(smoothPosition.y, minY, maxY) : currentBounds.center.y;

            smoothPosition = new Vector3(clampedX, clampedY, smoothPosition.z);
        }

        transform.position = smoothPosition;
    }

    public void SetBounds(Collider2D areaCollider)
    {
        currentBounds = areaCollider.bounds;
        hasBounds = true;
    }
}