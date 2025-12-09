using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelChange : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        yield return new WaitForSeconds(2f);
        tile.GetComponent<MeshCollider>().enabled = false;
        tile.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(10f);
        tile.GetComponent<MeshRenderer>().material = normal;
        tile.GetComponent<MeshCollider>().enabled = true;
        tile.GetComponent<MeshRenderer>().enabled = true;
    }
}
