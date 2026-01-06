using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelChange : MonoBehaviour
{
    private List<GameObject> Rings = new List<GameObject>();
    [SerializeField] Material normal;
    [SerializeField] Material transparent1;
    [SerializeField] Material transparent2;
    private List<int> droppedTiles = new List<int>();
    private List<int> rotatingRings = new List<int>();
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
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayTileSound(removeTile, tile.transform.position);
        }
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
        tile.GetComponent<MeshRenderer>().enabled = true;
        tile.GetComponent<MeshRenderer>().material = normal;
        droppedTiles.Remove(tileIndex);
    }

    // rotates a specific ring 
    public IEnumerator RotateRandomRing(float speed)
    {
        int ringIndex = Random.Range(0, 4);
        while (rotatingRings.Contains(ringIndex))
        {
            ringIndex = Random.Range(0, 4);
        }
        droppedTiles.Add(ringIndex);
        float actualSpeed = speed;
        if (Random.Range(0, 2) % 2 == 0)
            actualSpeed *= -1;
        for (float deg = 0; deg < 90; deg += speed * Time.deltaTime)
        {
            if (Rings[ringIndex].name == "Ring 1")
            {
                Rings[ringIndex].transform.Rotate(actualSpeed * Time.deltaTime * Vector3.forward);
            }
            else
            {
                Rings[ringIndex].transform.Rotate(actualSpeed * Time.deltaTime * Vector3.up);
            }
            yield return new WaitForEndOfFrame();
        }
        droppedTiles.Remove(ringIndex);
        yield return null;
    }

    public void Lazer()
    {
        
    }


}
