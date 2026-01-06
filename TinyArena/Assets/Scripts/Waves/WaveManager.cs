using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.AI.Navigation;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int waveNumber = 0;
    private bool started = false;
    private bool waiting = false;
    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] float enemyScaleFactor = 0.8f;

    public AudioClip waveChange;

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
                //AudioManager.Instance.PlaySFX(waveChange);
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
        StartCoroutine(levelChange.RotateRandomRing(RotateSpeedCalc()));
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
                return Mathf.FloorToInt(enemyScaleFactor * waveNumber);
        }
    }

    private float EnemyCooldownCalc()
    {
        return waveNumber > 12 ? 2 : 7 - Mathf.Ceil(waveNumber / 3f);
    }

    private List<int> EnemyTypeCalc()
    {
        var types = new List<int>();
        if (waveNumber != 3 && waveNumber != 7)
            types.Add(0);
        if (waveNumber >= 3 && waveNumber != 7 && waveNumber != 8)
            types.Add(1);
        if (waveNumber >= 7)
            types.Add(2);
        return types;
    }

    private float TileCalc()
    {
        return waveNumber switch
        {
            < 5 => 0f,
            >= 5 and < 9 => 1.5f,
            _ => 2 / (Mathf.Pow(waveNumber, 0.75f) - 4) // improvized formula
        };
    }
    private float RotateCalc()
    {
        return waveNumber < 9?0: waveNumber > 20?2: 12 - Mathf.Floor(waveNumber / 2);
    }

    private float RotateSpeedCalc()
    {
        return Mathf.Sqrt(waveNumber);
    }

    private bool LazerCalc() { return waveNumber > 12; }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        waiting = false;
    }

}
