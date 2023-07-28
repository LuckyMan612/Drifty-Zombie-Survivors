using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefab;
    [SerializeField] Transform player;
    [SerializeField] float spawnDistanceThreshold = 10f;
    [SerializeField] float spawnCooldown = 5f;
    float currentCooldown = 0f;

    
    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnEnemy();
        }
    }
    private void Update()
    {
        // Calculate the distance between the spawner and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is far away and the cooldown has elapsed
        if (currentCooldown <= 0f)
        {
            if (distanceToPlayer > spawnDistanceThreshold)
            {
                SpawnEnemy();
            }
            currentCooldown = spawnCooldown; // Reset the cooldown
        }

        // Reduce the cooldown over time
        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    private void SpawnEnemy()
    {
        // Instantiate the enemy prefab at the spawner's position
        Instantiate(enemyPrefab[Random.Range(0,enemyPrefab.Length)], transform.position, Quaternion.identity);
    }
    
}