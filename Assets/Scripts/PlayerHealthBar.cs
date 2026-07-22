using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Color barColor = Color.green; 
    public Vector3 offset = new Vector3(0, 1f, 0);
    public float barWidth = 1f;
    public float barHeight = 0.15f;

    private Transform fillTransform;
    private Transform bgTransform;

    void Start()
    {
        if (playerHealth == null)
            playerHealth = GetComponent<PlayerHealth>();

        CreateBar();
    }

    void CreateBar()
    {
        Sprite pixelSprite = CreatePixelSprite();
        Material unlitMat = new Material(Shader.Find("Sprites/Default"));

        SpriteRenderer playerRenderer = GetComponent<SpriteRenderer>();
        string sortLayer = playerRenderer != null ? playerRenderer.sortingLayerName : "Default";
        int baseOrder = playerRenderer != null ? playerRenderer.sortingOrder : 0;

        GameObject bg = new GameObject("HealthBarBackground");
        SpriteRenderer bgRenderer = bg.AddComponent<SpriteRenderer>();
        bgRenderer.sprite = pixelSprite;
        bgRenderer.material = unlitMat;
        bgRenderer.color = new Color(0.15f, 0.15f, 0.15f);
        bgRenderer.sortingLayerName = sortLayer;
        bgRenderer.sortingOrder = baseOrder + 10;
        bgTransform = bg.transform;

        GameObject fill = new GameObject("HealthBarFill");
        SpriteRenderer fillRenderer = fill.AddComponent<SpriteRenderer>();
        fillRenderer.sprite = pixelSprite;
        fillRenderer.material = unlitMat;
        fillRenderer.color = barColor; 
        fillRenderer.sortingLayerName = sortLayer;
        fillRenderer.sortingOrder = baseOrder + 11;
        fillTransform = fill.transform;
    }

    Sprite CreatePixelSprite()
    {
        Texture2D tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.white);
        tex.Apply();
        return Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1f);
    }

    void LateUpdate()
    {
        if (playerHealth == null) return;

        Vector3 basePos = transform.position + offset;

        bgTransform.position = basePos;
        bgTransform.localScale = new Vector3(barWidth, barHeight, 1f);

        float pct = Mathf.Clamp01((float)playerHealth.health / playerHealth.maxHealth);
        fillTransform.localScale = new Vector3(barWidth * pct, barHeight, 1f);

        float leftEdge = basePos.x - (barWidth / 2f);
        fillTransform.position = new Vector3(leftEdge + (barWidth * pct) / 2f, basePos.y, basePos.z);
    }
}