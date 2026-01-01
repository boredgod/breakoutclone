using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class TileAutomator : MonoBehaviour
{
    [Range(0,100)][SerializeField] int initialChance;
    [Range(1,8)][SerializeField] int birthLimit;
    [Range(1,8)][SerializeField] int deathLimit;
    [Range(1,10)][SerializeField] int numRepeat;
    int[,] terrainMap;
    [SerializeField] Vector3Int tMapSize;

    // [SerializeField]Tilemap topMap;
    // [SerializeField]Tilemap bottomMap;
    // [SerializeField] RuleTile topTile;
    // [SerializeField] RuleTile bottomTile;
    [SerializeField] GameObject prefabBrick;
    SpriteRenderer spriteRenderer;
    float spriteWidth;
    float spriteHeight;

    int width;
    int height;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = prefabBrick.GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.bounds.size.x;
        spriteHeight = spriteRenderer.bounds.size.y;
        DoSimulation(numRepeat);
    }

    // Update is called once per frame
    void Update()
    {
       
        // ClearMaps(true); 
    }

    void DoSimulation(int numRepeat)
    {
        // ClearMaps(false);
        width = tMapSize.x;
        height = tMapSize.y;

        if (terrainMap == null)
        {
            terrainMap = new int[width, height];
            InitialPosition();
        }

        for(int i=0; i < numRepeat; i++)
        {
            terrainMap = GenerateTilePosition(terrainMap);
        }
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if(terrainMap[i,j] == 1)
                {
                    // topMap.SetTile(new Vector3Int(-i + width/2, -j + height/2, 0), topTile);
                    // bottomMap.SetTile(new Vector3Int(-i + width/2, -j + height/2, 0), bottomTile);
                    Instantiate(prefabBrick, new Vector3Int(-i + width/2, 1+ -j + height/2, 0), Quaternion.identity);
                }
            }
        }

    }
    int[,] GenerateTilePosition(int[,] oldMap)
    {
        int[,] newMap = new int[width, height];
        int neighbors;
        BoundsInt myBounds = new BoundsInt(-1, -1, 0, 3, 3, 1);
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                neighbors = 0;
                foreach( var b in myBounds.allPositionsWithin)
                {
                    if(b.x == 0 && b.y == 0)
                    {
                        continue;
                    }
                    if(i + b.x >= 0 && i+b.x < width && j+b.y>= 0 && j+b.y < height)
                    {
                        neighbors += oldMap[i+b.x, j+b.y];// getting neighbors from oldmap
                    }
                    else
                    {
                        neighbors++;//border for terrain
                    }
                }

                if( oldMap[i,j] == 1)
                {
                    if(neighbors < deathLimit)
                    {
                        newMap[i,j] = 0;
                    }
                    else
                    {
                        newMap[i,j] = 1;
                    }
                }
                if(oldMap[i,j] == 0)
                {
                    if(neighbors > birthLimit)
                    {
                        newMap[i,j] = 1;
                    }
                    else
                    {
                        newMap[i,j] = 0;
                    }
                }

            }
        }

        return newMap;
    }
    void InitialPosition()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                terrainMap[i,j] = Random.Range(1,101) < initialChance ? 1 : 0;
            }
        }
    }
    // void ClearMaps(bool complete)
    // {
    //     topMap.ClearAllTiles();
    //     bottomMap.ClearAllTiles();
    //     if(complete)
    //     {
    //         terrainMap = null;
    //     }
    // }
}
