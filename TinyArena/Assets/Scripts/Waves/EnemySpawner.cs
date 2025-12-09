using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemySpawner : MonoBehaviour
{
    private float waitingTime = 0f;
    private List<Vector3> spawnpoints;
    [SerializeField] GameObject[] enemies;
    public bool spawning;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var spawns = GameObject.FindGameObjectsWithTag("spawnpoint");
        foreach (var spawn in spawns)
        {
            spawnpoints.Add(spawn.GetComponent<Transform>().position);
        }
    }

    public void SpawnEnemy(int interval, int amount)
    {
        int spawns = 0;
        while (spawns < amount)
        {
            if (waitingTime + Time.deltaTime > interval)
            {
                SpawnEnemy();
                waitingTime = 0;
                spawns++;
            }
            else
                waitingTime += Time.deltaTime;
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(getEnemy(), getSpawnPoint(), Quaternion.identity);
    }
    private GameObject getEnemy()
    {
        int enemytype = Random.Range(0, enemies.Length);
        return enemies[enemytype];
    }
    private Vector3 getSpawnPoint()
    {
        int spawnpoint = Random.Range(0, enemies.Length);
        return spawnpoints[spawnpoint];
    }
}
