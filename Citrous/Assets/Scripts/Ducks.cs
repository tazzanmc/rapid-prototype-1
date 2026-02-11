using UnityEngine;
using System.Collections;
public class Ducks : MonoBehaviour
{
    [Header("Fruit Setup")]
    public GameObject duckPrefab;   // assign ONE duck prefab per level
    public float spawnDelay = 10f;  // time before fruit appears
    public float lifeTime = 10f;    // how long fruit stays

    bool hasSpawned = false;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(spawnDelay);

        if (hasSpawned)
            yield break;

        GameObject fruit = Instantiate(duckPrefab, transform.position, Quaternion.identity);
        hasSpawned = true;

        yield return new WaitForSeconds(lifeTime);

        if (fruit != null)
            Destroy(fruit);
    }
}

