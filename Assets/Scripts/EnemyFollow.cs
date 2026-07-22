using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;

    public float speed = 2f;
    public float chaseDistance = 6f;
    public float stopDistance = 12f;
    public float attackDistance = 1.5f;

    public bool followPlayer = false;

    bool activatedByShot = false;
    public bool neverStop = false;
    public float shotAlertTime = 2f;
    float shotTimer = 0f;

    public LayerMask obstacleLayer;
    public float obstacleCheckRadius = 0.3f;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (activatedByShot)
        {
            shotTimer -= Time.deltaTime;
            if (shotTimer <= 0f) activatedByShot = false;
        }

        if (neverStop)
        {
            followPlayer = true;
        }
        else if (activatedByShot)
        {
            followPlayer = true;
        }
        else
        {
            if (distance < chaseDistance) followPlayer = true;
            if (distance > stopDistance) followPlayer = false;
        }

        bool isAttacking = distance < attackDistance;

        if (anim != null)
        {
            anim.SetFloat("Speed", followPlayer ? 1 : 0);
            anim.SetBool("IsAttacking", isAttacking);
        }

        if (followPlayer && !isAttacking)
        {
            Vector2 currentPos = transform.position;
            Vector2 desiredDir = ((Vector2)player.position - currentPos).normalized;
            float step = speed * Time.deltaTime;

            Vector2 nextPos = FindValidMove(currentPos, desiredDir, step);
            transform.position = nextPos;

            float directionX = player.position.x - transform.position.x;
            Vector3 scale = transform.localScale;
            if (Mathf.Abs(directionX) > 0.3f)
                scale.x = directionX > 0 ? 1 : -1;
            transform.localScale = scale;
        }
    }

    Vector2 FindValidMove(Vector2 currentPos, Vector2 desiredDir, float step)
    {
        float[] angleOffsets = { 0, 20, -20, 40, -40, 60, -60, 90, -90, 120, -120 };

        foreach (float angle in angleOffsets)
        {
            Vector2 dir = RotateVector(desiredDir, angle);
            Vector2 candidate = currentPos + dir * step;

            if (!Physics2D.OverlapCircle(candidate, obstacleCheckRadius, obstacleLayer))
            {
                return candidate;
            }
        }

        return currentPos; // ningún ángulo libre, se queda quieto ese frame
    }

    Vector2 RotateVector(Vector2 v, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
    }

    public void ActivateEnemy()
    {
        followPlayer = true;
        activatedByShot = true;
        shotTimer = shotAlertTime;
    }
}