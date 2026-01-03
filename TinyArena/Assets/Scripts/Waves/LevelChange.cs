using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class LevelChange : MonoBehaviour
{
    private List<GameObject> Rings = new List<GameObject>();
    [SerializeField] Material normal;
    [SerializeField] Material translucent;
    private List<int> droppedTiles = new List<int>();
    private GameObject Map;
    void Start()
    {
        Rings = GameObject.FindGameObjectsWithTag("Ring").ToList();
        
    }

    public IEnumerator DropRandomTile()
    {
        if (droppedTiles.Count == 64) 
            yield break;
        int tileIndex = Random.Range(0, 64);
        while (droppedTiles.Contains(tileIndex))
        {
            tileIndex = Random.Range(0, 64);
        }
        droppedTiles.Add(tileIndex);
        GameObject ring = Rings[tileIndex / 16];
        List<Transform> tileList = ring.GetComponentsInChildren<Transform>()
                .Where(t => t.gameObject.name == $"Ring {tileIndex / 16}.{tileIndex % 16}")
                .ToList();
        GameObject tile = tileList.First().gameObject;
        Debug.Log($"name: {tile.name}");
        if (tile.Equals(ring) || tile.GetComponent<MeshRenderer>().material.Equals(translucent))
        {
            yield break;
        }

        tile.GetComponent<MeshRenderer>().material = translucent;
        yield return new WaitForSeconds(2f);
        tile.GetComponent<MeshCollider>().enabled = false;
        tile.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(10f);
        tile.GetComponent<MeshRenderer>().material = normal;
        tile.GetComponent<MeshCollider>().enabled = true;
        tile.GetComponent<MeshRenderer>().enabled = true;
        droppedTiles.Remove(tileIndex);
    }

    // rotates a specific ring 
    public IEnumerator RotateRandomRing()
    {
        Rings[0].transform.Rotate(new Vector3(0, 0, 1));
        yield break;
    }

    public void Lazer()
    {

    }
}
