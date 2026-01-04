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
    private List<int> droppedTiles = new List<int>();
    private GameObject Map;
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


        GameObject tile = tileList.First().gameObject;
        MeshRenderer render = tile.GetComponent<MeshRenderer>();
        Color col = render.material.color;
        for (float elapsed = 0f; elapsed < 2; elapsed += Time.deltaTime) {
            col.a = 1f - elapsed / 2;
            render.material.color = col;
            yield return new WaitForEndOfFrame();
        }
        col.a = 0;
        render.material.color = col;
        tile.GetComponent<MeshCollider>().enabled = false;
        yield return new WaitForSeconds(10f);
        tile.GetComponent<MeshCollider>().enabled = true;
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
