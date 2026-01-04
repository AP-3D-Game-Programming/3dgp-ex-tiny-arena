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
    [SerializeField] Material transparent1;
    [SerializeField] Material transparent2;
    private List<int> droppedTiles = new List<int>();
    private GameObject Map;

    [SerializeField] AudioClip removeTile;
    void Start()
    {
        Rings = GameObject.FindGameObjectsWithTag("Ring").ToList();
        Map = GameObject.FindGameObjectWithTag("Map");
    }

    public IEnumerator DropRandomTile()
    {
        // find tile
        if (droppedTiles.Count == 64) 
            yield break;
        int tileIndex = Random.Range(0, 64);
        while (droppedTiles.Contains(tileIndex))
        {
            tileIndex = Random.Range(0, 64);
        }
        droppedTiles.Add(tileIndex);
        List<Transform> tileList = Map.GetComponentsInChildren<Transform>()
                .Where(t => t.gameObject.name == $"Ring {(tileIndex / 16)+ 1}.{tileIndex % 16}")
                .ToList();

        // remove tile
        GameObject tile = tileList.First().gameObject;
        // play sfx
        var source = tile.AddComponent<AudioSource>();
        source.pitch = Random.Range(0.95f, 1.05f);
        source.PlayOneShot(removeTile, 0.3f);
        // make transparent
        tile.GetComponent<MeshRenderer>().material = transparent1;
        yield return new WaitForSeconds(1f);
        tile.GetComponent<MeshRenderer>().material = transparent2;
        yield return new WaitForSeconds(1f);
        // remove
        tile.GetComponent<MeshCollider>().enabled = false;
        tile.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(10f);
        tile.GetComponent<MeshCollider>().enabled = true;
        tile.GetComponent<MeshRenderer>().enabled = false;
        tile.GetComponent<MeshRenderer>().material = normal;
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
