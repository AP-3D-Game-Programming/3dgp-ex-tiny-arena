using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class WaveManager : MonoBehaviour
{
    private int waveNumber = 0;

    [SerializeField] float delayRemove = 2f;
    [SerializeField] float minDelayRemove = 1f;
    private float waitingTimeRemove = 0f;

    private EnemySpawner enemySpawner;
    private LevelChange levelChange;

    private void Start()
    {
        enemySpawner = GetComponent<EnemySpawner>();
        levelChange = GetComponent<LevelChange>();
    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating(enemySpawner.SpawnEnemy(100, 5), 0, EnemyCooldownCalc());

        
    }

    private float EnemyCalc()
    {
        return 0;
    }

    private float EnemyCooldownCalc()
    {
        return 0;
    }

    private float EnemyTypeCalc()
    {
        return 0;
    }

    private float TileCalc()
    {
        return 0;
    }
    private float RotateCalc()
    {
        return 0;
    }

}
