using UnityEngine;
using UnityEngine.AI;

public class WaveManager : MonoBehaviour
{
    private int waveNumber;
    
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject enemy;
    private GameObject Ring1;
    private GameObject Ring2;
    private GameObject Ring3;
    private GameObject Ring4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Ring1 = GameObject.FindWithTag("Ring1");
        Ring2 = GameObject.FindWithTag("Ring2");
        Ring3 = GameObject.FindWithTag("Ring3");
        Ring4 = GameObject.FindWithTag("Ring4");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
