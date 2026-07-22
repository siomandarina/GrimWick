using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public TMP_Text label;
    public float moveSpeed = 1f;
    public float lifetime = 1f;

    private float timer;
    private Color startColor;

    public void SetText(string text, Color color)
    {
        label.text = text;
        label.color = color;
        startColor = color;
    }

    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        timer += Time.deltaTime;

        float pct = timer / lifetime;
        Color c = startColor;
        c.a = 1f - pct;
        label.color = c;

        if (timer >= lifetime)
            Destroy(gameObject);
    }
}