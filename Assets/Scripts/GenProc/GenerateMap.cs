using System;

public class GenerateMap
{
    private HexCell[] cells;
    private Random rand;

    public GenerateMap(HexCell[] cells)
    {
        this.cells = cells;
        rand = new Random();
    }

    /// <summary>
    /// Procedural generation of the map
    /// </summary>
    /// <param name="cells"></param>
    public void Generate()
    {
        //Tirage d'un biome & génération des variables
        


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
            if (rand.NextDouble() < 0)
            {
                int elevationCumul = 0;
                //Regarder tous les voisins
                foreach(HexDirection dir in Enum.GetValues(typeof(HexDirection)))
                {
                    HexCell neighbor = cell.GetNeighbor(dir);
                    if (neighbor)
                    {
                        elevationCumul += neighbor.Elevation;
                    }
                }

                cell.Elevation = elevationCumul / 6;
            }
        }
    }

    /// <summary>
    /// Pseudo randomize each cells water level regarding the neighbors
    /// </summary>
    /// <param name="cells"></param>
    void RandomizeWaterLevel()
    {

    }
}
