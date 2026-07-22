using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyZone : MonoBehaviour
{
    public Collider2D spawnArea;
    public GameObject[] enemyPrefabs;
    public int totalEnemiesToSpawn = 8;
    public float minSpawnDelay = 0.05f;
    public float maxSpawnDelay = 1f;
    public LayerMask obstacleLayer;

    public ZoneGate[] gatesToClose;
    public ZoneGate[] gatesToOpen;
    public int[] openThresholds;

    [Header("Health Pickups")]
    public GameObject healthPickupPrefab;
    public int maxHealthPickups = 5;
    public float minPickupDelay = 1f;
    public float maxPickupDelay = 4f;

    private List<GameObject> aliveEnemies = new List<GameObject>();
    private int spawnedSoFar = 0;
    private int gatesOpened = 0;
    private bool activated = false;

    public void ActivateZone()
    {
        if (activated) return;
        activated = true;

        foreach (ZoneGate g in gatesToClose)
            g.Close();

        StartCoroutine(SpawnRoutine());

        if (healthPickupPrefab != null && maxHealthPickups > 0)
            StartCoroutine(SpawnHealthPickups());
    }

    IEnumerator SpawnRoutine()
    {
        for (int i = 0; i < totalEnemiesToSpawn; i++)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));

            Vector2 spawnPos = GetRandomPointInArea();
            GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            GameObject enemy = Instantiate(prefab, spawnPos, Quaternion.identity);

            RegisterEnemy(enemy);
            spawnedSoFar++;
        }
    }

    IEnumerator SpawnHealthPickups()
    {
        int count = Random.Range(1, maxHealthPickups + 1); 

        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(Random.Range(minPickupDelay, maxPickupDelay));

            Vector2 spawnPos = GetRandomPointInArea();
            Instantiate(healthPickupPrefab, spawnPos, Quaternion.identity);
        }
    }

    public Vector2 GetRandomPointInArea()
    {
        Bounds b = spawnArea.bounds;
        Vector2 point;
        int attempts = 0;

        do
        {
            point = new Vector2(Random.Range(b.min.x, b.max.x), Random.Range(b.min.y, b.max.y));
            attempts++;
        }
        while ((!spawnArea.OverlapPoint(point) || Physics2D.OverlapCircle(point, 0.3f, obstacleLayer))
            && attempts < 30);

        return point;
    }

    public void RegisterEnemy(GameObject enemy)
    {
        aliveEnemies.Add(enemy);

        EnemyHealth2 eh = enemy.GetComponent<EnemyHealth2>();
        eh.assignedZone = this;
        eh.onDeath += () => OnEnemyDeath(enemy);
    }

    void OnEnemyDeath(GameObject enemy)
    {
        aliveEnemies.Remove(enemy);

        while (gatesOpened < gatesToOpen.Length
            && gatesOpened < openThresholds.Length
            && aliveEnemies.Count <= openThresholds[gatesOpened])
        {
            gatesToOpen[gatesOpened].Open();
            gatesOpened++;
        }
    }
}