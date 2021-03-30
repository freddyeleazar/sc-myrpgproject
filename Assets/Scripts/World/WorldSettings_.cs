using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class WorldSettings_ : MonoBehaviour
{
    public WorldController_ world;
    public GroundGenerator_ groundGenerator;
    public TreeGenerator treeGenerator;
    public GameObject trees;

    [Title("Map Size", "Continental land size in meters")]
    [SuffixLabel("m")]
    public Vector2 mapSize;

    [Title("Temperature", "Maximum and minimum levels of temperature")]
    [MinMaxSlider("@possibleLimitTemperatures.x", "@possibleLimitTemperatures.y")]
    [SuffixLabel("C°")]
    public Vector2 limitTemperatures;
    public Vector2 possibleLimitTemperatures = new Vector2(-50, 50);

    [Button()]
    public void Build()
    {
        world.mechanics.stats.mapSize = mapSize;
        world.mechanics.stats.limitTemperatures = limitTemperatures;
        Tilemap baseGroundTilemap = world.graphics.grid.worldBaseGround_.baseGroundTilemap;
        Tilemap grassTilemap = world.graphics.grid.grassTilemap;
        Tilemap sandTilemap = world.graphics.grid.sandTilemap;

        for (int yPosition = 0; yPosition < mapSize.y; yPosition++)
        {
            float calculatedTemperature = (((limitTemperatures.y - limitTemperatures.x) * yPosition) / mapSize.y) + limitTemperatures.x;
            float maxMoisture = -(50 * (calculatedTemperature - possibleLimitTemperatures.y) / (possibleLimitTemperatures.x - possibleLimitTemperatures.y)) + 100;
            float minMoisture = 100 - maxMoisture;
            Vector2 moistureLimits = new Vector2(maxMoisture, minMoisture);
            for (int xPosition = 0; xPosition < mapSize.x; xPosition++)
            {
                float calculatedMoisture = (((moistureLimits.x - moistureLimits.y) * (xPosition - 0)) / (mapSize.x - 1 - 0)) + moistureLimits.y;
                Vector3Int position = new Vector3Int(xPosition, yPosition, 0);

                //Generate ground components
                Tile ground = groundGenerator.GenerateGround(calculatedTemperature, calculatedMoisture);
                baseGroundTilemap.SetTile(position, ground);

                //Generate trees
                Tree_ tree = treeGenerator.GenerateTree(calculatedTemperature, calculatedMoisture);
                if (tree != null)
                {
                    if (Random.Range(1, 4) == 1)
                    {
                        if (Random.Range(1, 100) <= ((TreeStats_)tree.mechanics_.stats_).biome.treeDensity)
                        {
                            Tree_ newTree = Instantiate(tree, baseGroundTilemap.CellToWorld(position), Quaternion.identity);
                            newTree.transform.SetParent(trees.transform);
                        }
                    }
                }
            }
        }
    }

    [Button()]
    public void Destroy()
    {
        world.graphics.grid.GetComponentsInChildren<Tilemap>().ForEach(t => t.ClearAllTiles());
        trees.GetComponentsInChildren<Tree_>().ToList().ForEach(t => DestroyImmediate(t.gameObject));
    }

    [Button]
    public void BuildGrounds()
    {
        foreach (Ground_ ground in groundGenerator.grounds)
        {
            ground.Build();
        }
    }
} 