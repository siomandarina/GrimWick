using UnityEngine;
using System.Collections.Generic;


public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; 
    public int enemiesToSpawn = 1;
    public float spawnRadius = 1.5f;

    public List<GameObject> SpawnEnemies()
    {
        List<GameObject> result = new List<GameObject>();

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Vector2 offset = Random.insideUnitCircle * spawnRadius;
            Vector2 pos = (Vector2)transform.position + offset;

            GameObject enemy = Instantiate(prefab, pos, Quaternion.identity);
            result.Add(enemy);
        }

        return result;
    }
}