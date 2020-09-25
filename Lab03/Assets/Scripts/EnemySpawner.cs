using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // prefabs for enemy
    public GameObject redEnemyPrefab;
    public GameObject yellowEnemyPrefab;
    public GameObject blueEnemyPrefab;

    // spawn boundary
    public Rect spawnBoundary;  // the edge of the enemies can spawn

    // spawn rate for different enemy
    public float redSpawnRate;
    public float yellowSpawnRate;
    public float blueSpawnRate;

    public float timeBetweenEnemySpawn;
    private float spawnTimer;

    public GameObject enemyParent;

    // Start is called before the first frame update
    void Start()
    {
        redSpawnRate = 5f;
        yellowSpawnRate = 0f;
        blueSpawnRate = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        // spawn an enemy every 'timeBetweenSpawn' second
        if (spawnTimer >= timeBetweenEnemySpawn)
        {
            spawnTimer = 0.0f;
            SpawnEnemy();
            UpdateEnemySpawnRates();
        }
    }

    void SpawnEnemy()
    {
        float spawnDirection = Random.Range(1, 5); // left, up, right, down
        Vector3 spawnPos = Vector3.zero;

        // spawn pos based on screen direction
        if (spawnDirection == 1)
            spawnPos = new Vector3(spawnBoundary.xMin, Random.Range(spawnBoundary.yMin,
                spawnBoundary.yMax), 0);

        if (spawnDirection == 2)
            spawnPos = new Vector3(Random.Range(spawnBoundary.xMin, spawnBoundary.xMax), 
                spawnBoundary.yMax, 0);

        if (spawnDirection == 3)
            spawnPos = new Vector3(spawnBoundary.xMax, Random.Range(spawnBoundary.yMin,
                spawnBoundary.yMax), 0);
        else
            spawnPos = new Vector3(Random.Range(spawnBoundary.xMin, spawnBoundary.xMax),
                spawnBoundary.yMin, 0);

        // spawn enemy
        GameObject enemy = Instantiate(GetEnemyToSpawn(), spawnPos, Quaternion.identity,
            enemyParent.transform);
    }

    // return the enemy to be spawned
    GameObject GetEnemyToSpawn()
    {
        float total = redSpawnRate + yellowSpawnRate + blueSpawnRate;
        float ranNum = Random.Range(0.0f, total);

        if (ranNum <= redSpawnRate)
            return redEnemyPrefab;

        if (ranNum <= yellowSpawnRate + redSpawnRate)
            return yellowEnemyPrefab;

        if (ranNum <= total)
            return blueEnemyPrefab;

        return null;  // error happen
    }

    void UpdateEnemySpawnRates()
    {
        redSpawnRate += 0.05f;
        yellowSpawnRate += 0.03f;
        blueSpawnRate += 0.03f;
    }
}
