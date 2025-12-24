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

    private float delayRemove = 2f;
    private float waitingTimeRemove = 0f;
    private float delayEnemy = 10f;
    private float waitingTimeEnemy = 0f;

    [Header("Audio")]
    public AudioClip dropTileSfx;

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
            if (delayRemove > 1f)
                delayRemove -= 0.2f;
        }
        else
            waitingTimeRemove += Time.deltaTime;

        if (waitingTimeEnemy + Time.deltaTime > delayEnemy)
        {
            Instantiate(enemy, spawnPoint, Quaternion.identity);
            waitingTimeEnemy = 0;
            if (delayEnemy > 3f)
                delayEnemy -= 0.5f;
        }
        else
            waitingTimeEnemy += Time.deltaTime;
    }

    // rotates a specific ring 
    IEnumerator RotateRing(int ringindex, float RotationAmmount)
    {
        throw new System.NotImplementedException();
    }

    IEnumerator DropRandomTile()
    {
        int ringIndex = Mathf.FloorToInt(Random.Range(0, 4));
        int tileIndex = Mathf.FloorToInt(Random.Range(0, 16));
        GameObject ring = Rings[ringIndex];
        List<Transform> tiles = ring.GetComponentsInChildren<Transform>().Where(x => !x.gameObject.Equals(ring)).ToList();
        GameObject tile = tiles[tileIndex].gameObject;
        Debug.Log($"name: {tile.name}");
        if (tile.Equals(ring) || tile.GetComponent<MeshRenderer>().material.Equals(translucent))
        {
            yield break;
        }
        
        tile.GetComponent<MeshRenderer>().material = translucent;
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(dropTileSfx);
        }
        yield return new WaitForSeconds(2f);
        tile.GetComponent<MeshCollider>().enabled = false;
        tile.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(10f);
        tile.GetComponent<MeshRenderer>().material = normal;
        tile.GetComponent<MeshCollider>().enabled = true;
        tile.GetComponent<MeshRenderer>().enabled = true;
    }
}
