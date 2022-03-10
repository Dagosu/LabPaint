using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Parameters
    [SerializeField] float maximumTimeBtwSpawns = 5f;
    [SerializeField] Color32[] enemyColor = default;

    //References
    [SerializeField] GameObject enemyPrefab = default;
    [SerializeField] Transform[] spawnSpots = default;

    //Variables
    float timeBtwSpawns;
    float startTimeBtwSpawns;

    void Start()
    {
        timeBtwSpawns = Random.Range(0, maximumTimeBtwSpawns);
        startTimeBtwSpawns = timeBtwSpawns;
    }

    void Update()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        if (startTimeBtwSpawns >= timeBtwSpawns)
        {
            int spawnIndex = Random.Range(0, spawnSpots.Length);
            int colorIndex = Random.Range(0, enemyColor.Length);

            GameObject enemy = Instantiate(enemyPrefab, spawnSpots[spawnIndex].position, Quaternion.identity);
            enemy.GetComponent<SpriteRenderer>().color = enemyColor[colorIndex];

            timeBtwSpawns = Random.Range(0, maximumTimeBtwSpawns);
            startTimeBtwSpawns = 0;
        }
        else
            startTimeBtwSpawns += Time.deltaTime;
    }
}
