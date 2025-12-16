using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelChange : MonoBehaviour
{
    private List<GameObject> Rings = new List<GameObject>();
    [SerializeField] Material normal;
    [SerializeField] Material translucent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rings = GameObject.FindGameObjectsWithTag("ring").ToList();
    }

    // Update is called once per frame

    public IEnumerator DropRandomTile()
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

    // rotates a specific ring 
    public IEnumerator RotateRandomRing()
    {
        throw new System.NotImplementedException();
    }
}
