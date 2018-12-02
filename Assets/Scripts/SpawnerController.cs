using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour {

    public GameObject enemyPrefab;
    public int direction;
    public float velocityRangeMax = 300;
    public float velocityRangeMin = 150;
    public float spawnDelayRangeMax = 3;
    public float spawnDelayRangeMin = 1;

    GameObject currentEnemy;
    float currentTimeBtwSpawn;


    void Start()
    {
        currentTimeBtwSpawn = Random.Range(spawnDelayRangeMin, spawnDelayRangeMax);
        if (direction == 1) Invoke("SpawnEnemy", 1);
    }

    void Update()
    {
        if (currentTimeBtwSpawn <= 0)
        {
            currentTimeBtwSpawn = 0;
            SpawnEnemy();
        }
        else
        {
            currentTimeBtwSpawn -= Time.deltaTime;
        }
    }

    void SpawnEnemy()
    {
        currentTimeBtwSpawn = Random.Range(spawnDelayRangeMin, spawnDelayRangeMax);
        currentEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        currentEnemy.GetComponent<Rigidbody2D>().AddForce(GetRandomVelocity());
    }

    Vector2 GetRandomVelocity()
    {
        return new Vector2(
            Mathf.Sign(direction) * Random.Range(velocityRangeMin, velocityRangeMax),
            Random.Range(velocityRangeMin, velocityRangeMax)
            );
    }

}
