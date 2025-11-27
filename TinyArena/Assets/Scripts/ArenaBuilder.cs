using System.Collections.Generic;
using UnityEngine;

public class ArenaBuilder : MonoBehaviour
{
    [SerializeField] GameObject floor;
    [SerializeField] GameObject wall;
    [SerializeField] GameObject ceiling;

    private readonly float tileSize = 5; // would need to change prefab currently for it to work properly
    private readonly int arenaSize = 11; // must be odd curently
    
    [SerializeField] int ceilingHeight;

    // tiles go from low z to high z and then from low x to high x
    private List<GameObject> floorTiles = new List<GameObject>();
    public List<GameObject> FloorTiles { get { return floorTiles; } }
    void Start()
    {
        int arenaEnd = (arenaSize - 1) / 2;
        float wallOffset = 0.5f * tileSize;
        Quaternion ninety = Quaternion.AngleAxis(90, Vector3.up);
        Quaternion oneEighty = Quaternion.AngleAxis(180, Vector3.up);
        Quaternion twoSeventy = Quaternion.AngleAxis(270, Vector3.up);
        Quaternion upsideDown = Quaternion.AngleAxis(180, Vector3.forward);
        for (int x = -arenaEnd; x <= arenaEnd; x++)
        {
            for (int z = -arenaEnd; z <= arenaEnd; z++)
            {
                floorTiles.Add(Instantiate(floor, new Vector3(tileSize * x, 0, tileSize * z), Quaternion.identity));
                Instantiate(ceiling, new Vector3(tileSize * x, ceilingHeight, tileSize * z), upsideDown);
            }
            for (int y = 0; y < ceilingHeight / tileSize; y++)
            {
                Instantiate(wall, new Vector3(tileSize * x, tileSize * y + wallOffset, tileSize * arenaEnd + wallOffset), Quaternion.identity);
                Instantiate(wall, new Vector3(tileSize * x, tileSize * y + wallOffset, tileSize * -arenaEnd - wallOffset), oneEighty);
                Instantiate(wall, new Vector3(tileSize * arenaEnd + wallOffset, tileSize * y + wallOffset, tileSize * x), ninety);
                Instantiate(wall, new Vector3(tileSize * -arenaEnd - wallOffset, tileSize * y + wallOffset, tileSize * x), twoSeventy);
            }
        }
    }
}
