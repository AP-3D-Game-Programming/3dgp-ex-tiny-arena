using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class WaveManager : MonoBehaviour
{
    private int waveNumber;
    
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject enemy;
    private List<GameObject> Rings = new List<GameObject>();
    [SerializeField] Material normal;
    [SerializeField] Material translucent;
    private Vector3 spawnPoint;

    [SerializeField] float delayRemove = 2f;
    [SerializeField] float minDelayRemove = 1f;
    private float waitingTimeRemove = 0f;

    private int waveCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // should be removed after gamemanager exists
        Cursor.lockState = CursorLockMode.Locked;
        Rings.Add(GameObject.FindWithTag("Ring1"));
        Rings.Add(GameObject.FindWithTag("Ring2"));
        Rings.Add(GameObject.FindWithTag("Ring3"));
        Rings.Add(GameObject.FindWithTag("Ring4"));
        spawnPoint = GameObject.FindWithTag("SpawnPoint").GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitingTimeRemove + Time.deltaTime > delayRemove)
        {
            StartCoroutine(DropRandomTile());
            waitingTimeRemove = 0;
            if (delayRemove > minDelayRemove)
                delayRemove -= 0.2f;
        }
        else
            waitingTimeRemove += Time.deltaTime;

        
    }

    // rotates a specific ring 
    IEnumerator RotateRing(int ringindex, float RotationAmmount)
    {
        throw new System.NotImplementedException();
    }

    
}
