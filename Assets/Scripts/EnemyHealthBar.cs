using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public Color barColor = Color.red;
    public Vector3 offset = new Vector3(0, 0.6f, 0);
    public float barWidth = 0.8f;
    public float barHeight = 0.1f;

    private EnemyHealth2 enemyHealth;
    private int maxHealth;
    private Transform fillTransform;
    private Transform bgTransform;

    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth2>();
        maxHealth = enemyHealth.health;

        CreateBar();
    }

    void CreateBar()
    {
        Sprite pixelSprite = CreatePixelSprite();
        Material mat = new Material(Shader.Find("Sprites/Default"));

        SpriteRenderer enemyRenderer = GetComponent<SpriteRenderer>();
        string sortLayer = enemyRenderer != null ? enemyRenderer.sortingLayerName : "Default";
        int baseOrder = enemyRenderer != null ? enemyRenderer.sortingOrder : 0;

        GameObject bg = new GameObject("EnemyHealthBarBG");
        SpriteRenderer bgR = bg.AddComponent<SpriteRenderer>();
        bgR.sprite = pixelSprite;
        bgR.material = mat;
        bgR.color = new Color(0.1f, 0.1f, 0.1f);
        bgR.sortingLayerName = sortLayer;
        bgR.sortingOrder = baseOrder + 10;
        bgTransform = bg.transform;

        GameObject fill = new GameObject("EnemyHealthBarFill");
        SpriteRenderer fillR = fill.AddComponent<SpriteRenderer>();
        fillR.sprite = pixelSprite;
        fillR.material = mat;
        fillR.color = barColor;
        fillR.sortingLayerName = sortLayer;
        fillR.sortingOrder = baseOrder + 11;
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
        if (enemyHealth == null) return;

        Vector3 basePos = transform.position + offset;

        bgTransform.position = basePos;
        bgTransform.localScale = new Vector3(barWidth, barHeight, 1f);

        float pct = Mathf.Clamp01((float)enemyHealth.health / maxHealth);
        fillTransform.localScale = new Vector3(barWidth * pct, barHeight, 1f);

        float leftEdge = basePos.x - barWidth / 2f;
        fillTransform.position = new Vector3(leftEdge + (barWidth * pct) / 2f, basePos.y, basePos.z);
    }

    void OnDestroy()
    {
        if (bgTransform != null) Destroy(bgTransform.gameObject);
        if (fillTransform != null) Destroy(fillTransform.gameObject);
    }
}