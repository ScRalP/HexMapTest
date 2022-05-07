using System;
using UnityEngine;

public class GenerateMap
{
    private HexCell[] cells;

    public GenerateMap(HexCell[] cells)
    {
        this.cells = cells;
    }

    /// <summary>
    /// Procedural generation of the map
    /// </summary>
    /// <param name="cells"></param>
    public void Generate()
    {
        RandomizeElevation();
        RandomizeWaterLevel();

        GenerateRivers();
        GenerateRoads();
    }

    /// <summary>
    /// Create rivers from cells to others
    /// </summary>
    void GenerateRivers()
    {

    }

    /// <summary>
    /// Create roads and connections between roads
    /// </summary>
    void GenerateRoads()
    {

    }

    /// <summary>
    /// Pseudo randomize each cells elevation regarding the neighbors
    /// </summary>
    /// <param name="cells"></param>
    void RandomizeElevation()
    {
        //Randomize
        foreach(HexCell cell in cells)
        {
            cell.Elevation = UnityEngine.Random.Range(0, 6);
        }

        //Lissage
        foreach(HexCell cell in cells)
        {
            int elevationGap = 0;
            //Regarder tout les voisins
            foreach(HexDirection dir in Enum.GetValues(typeof(HexDirection)))
            {
                HexCell neighbor = cell.GetNeighbor(dir);
                if (neighbor)
                {
                    if(neighbor.Elevation > cell.Elevation) {
                        elevationGap++;
                    } else if(neighbor.Elevation < cell.Elevation) {
                        elevationGap--;
                    }
                }
            }

            cell.Elevation += elevationGap;
        }

        //Imprefections
    }

    /// <summary>
    /// Pseudo randomize each cells water level regarding the neighbors
    /// </summary>
    /// <param name="cells"></param>
    void RandomizeWaterLevel()
    {

    }
}
