using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool isSpawning = false;
    private float waitingTime = 0f;
    private List<Vector3> spawnpoints;
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> allowedEnemies = new List<GameObject>();
    private Transform parent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var spawns = GameObject.FindGameObjectsWithTag("spawnpoint");
        foreach (var spawn in spawns)
        {
            spawnpoints.Add(spawn.GetComponent<Transform>().position);
        }
        parent = GameObject.FindGameObjectWithTag("enemies").GetComponent<Transform>();
    }

    public void SpawnEnemy(float interval, int amount, int[] allowedTypes)
    {
        if (!isSpawning)
        {
            isSpawning = true;
            allowedEnemies.RemoveAll(e => true);
            foreach (int type in allowedTypes)
            {
                allowedEnemies.Add(enemies[type]);
            }
            StartCoroutine(SpawnEnemy(interval, amount));
        }
        
    }

    private IEnumerator SpawnEnemy(float interval, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(getEnemy(), getSpawnPoint(), Quaternion.identity, parent);
            yield return new WaitForSeconds(interval);
        }
        yield return new WaitUntil(() => parent.childCount == 0);
        isSpawning = false;
    }
    private GameObject getEnemy()
    {
        int enemytype = Random.Range(0, allowedEnemies.Count);
        return allowedEnemies[enemytype];
    }
    private Vector3 getSpawnPoint()
    {
        int spawnpoint = Random.Range(0, spawnpoints.Count);
        return spawnpoints[spawnpoint];
    }
}
