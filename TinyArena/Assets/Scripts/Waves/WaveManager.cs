using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private int waveNumber = 0;
    private bool started = false;
    private bool waiting = false;
    [SerializeField] float timeBetweenWaves = 5f;

    [SerializeField] float delayRemove = 2f;
    [SerializeField] float minDelayRemove = 1f;

    private EnemySpawner enemySpawner;
    private LevelChange levelChange;
    private void Start()
    {
        enemySpawner = GetComponent<EnemySpawner>();
        levelChange = GetComponent<LevelChange>();

        
    }

    public void Begin()
    {
        started = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            if (!enemySpawner.isSpawning && !waiting)
            {
                waveNumber++;
                enemySpawner.SpawnEnemy(EnemyCooldownCalc(), EnemyCalc(), EnemyTypeCalc());
                if (TileCalc() != 0)
                    InvokeRepeating(nameof(Drop), TileCalc(), TileCalc());
                if (RotateCalc() != 0)
                    InvokeRepeating(nameof(Rotate), RotateCalc(), RotateCalc());
                waiting = true;
            }
            if (!enemySpawner.isSpawning && waiting)
            {
                StartCoroutine(Wait(timeBetweenWaves));
            }
        }
    }

    private void Drop()
    {
        StartCoroutine(levelChange.DropRandomTile());
    }

    private void Rotate()
    {
        StartCoroutine(levelChange.RotateRandomRing());
    }

    private int EnemyCalc()
    {
        return 3;
    }

    private float EnemyCooldownCalc()
    {
        return 5;
    }

    private int[] EnemyTypeCalc()
    {
        int[] ene = new int[1];
        ene[0] = 0;
        return ene;
    }

    private float TileCalc()
    {
        return 0;
    }
    private float RotateCalc()
    {
        return 0;
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        waiting = false;
    }

}
