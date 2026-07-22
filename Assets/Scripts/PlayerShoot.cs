using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Camera mainCamera;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (mainCamera == null) mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 dir = GetDirectionToMouse();

            FlipTowards(dir); 

            anim.SetTrigger("Attack");

            StartCoroutine(ShootAfterDelay(dir));
        }
    }

    Vector2 GetDirectionToMouse()
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        return ((Vector2)mouseWorldPos - (Vector2)transform.position).normalized;
    }

    void FlipTowards(Vector2 dir)
    {
        Vector3 scale = transform.localScale;

        if (dir.x > 0)
            scale.x = Mathf.Abs(scale.x);
        else if (dir.x < 0)
            scale.x = -Mathf.Abs(scale.x);

        transform.localScale = scale;
    }

    IEnumerator ShootAfterDelay(Vector2 dir)
    {
        yield return new WaitForSeconds(0.15f);

        GameObject bullet = Instantiate(
            bulletPrefab,
            (Vector2)transform.position + dir * 1f,
            Quaternion.identity
        );

        bullet.GetComponent<Bullet>().direction = dir;
    }
}