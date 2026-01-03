using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private int waveNumber = 0;
    private bool started = false;
    private bool waiting = false;
    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] float enemyScaleFactor = 0.8f;

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
                //if (LazerCalc())
                    
                waiting = true;
            }
            if (!enemySpawner.isSpawning && waiting)
            {
                CancelInvoke();
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
        switch (waveNumber)
        {
            case 1:
            case 3:
            case 4:
            case 6:
            case 7:
                return 4;
            case 2:
            case 5:
            case 8:
            case 10:
                return 6;
            case 9:
                return 7;
            default:
                return (int)Mathf.Floor(enemyScaleFactor * waveNumber);
        }
    }

    private float EnemyCooldownCalc()
    {
        return waveNumber > 12 ? 2 : 7 - Mathf.Ceil(waveNumber / 3f);
    }

    private int[] EnemyTypeCalc()
    {
        var a = new int[1];
        a[0] = 0;
        return a;
    }

    private float TileCalc()
    {
        return 0;
    }
    private float RotateCalc()
    {
        return 0;
    }

    private bool LazerCalc() { return waveNumber > 12; }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        waiting = false;
    }

}
