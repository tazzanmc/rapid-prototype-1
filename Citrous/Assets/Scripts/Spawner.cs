using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyMovement[] enemyPrefabs;
    public Transform spawnPoint;

    public float spawnDelay = 3f;
    int spawned = 0;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnDelay);
    }

    void SpawnEnemy()
    {
        if (spawned >= enemyPrefabs.Length)
        {
            CancelInvoke(nameof(SpawnEnemy));
            return;
        }

        Instantiate(enemyPrefabs[spawned], spawnPoint.position, Quaternion.identity);
        spawned++;
    }
}



