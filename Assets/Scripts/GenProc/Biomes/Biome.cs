using System;
using System.Collections.Generic;

public abstract class Biome : IMapGenerator
{
    protected HexGrid grid;
    public Biome(HexGrid grid)
    {
        this.grid = grid;
    }

    public abstract void Generate();


    #region Méthodes de génération

    #region /*----------- ELEVATION -----------*/
    protected void RandomizeElevation()
    {
        foreach(HexCell cell in grid.GetCells())
        {
            cell.Elevation = UnityEngine.Random.Range(0, 6);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cells">Les cellules à lisser</param>
    /// <param name="neighborsRange">Distance des voisins à regarder</param>
    protected void FlattenCells(HexCell[] cells, int neighborsRange = 1)
    {
        foreach(HexCell cell in cells)
        {
            FlattenCell(cell, neighborsRange);
        }
    }

    protected void FlattenCell(HexCell cell, int neighborsRange = 1)
    {
        List<HexCell> neighbors = new List<HexCell>();
        neighbors.Add(cell);

        //Ajouter tout les voisins à prendre en compte
        for(int i = 0; i < neighborsRange; i++)
        {
            foreach(HexCell c in neighbors)
            {
                foreach (HexDirection dir in Enum.GetValues(typeof(HexDirection)))
                {
                    HexCell neighbor = c.GetNeighbor(dir);
                    if (!neighbors.Contains(neighbor))
                    {
                        neighbors.Add(neighbor);
                    }
                }
            }
        }

        //Pondérer la valeur de la case
        int elevation = 0;
        foreach(HexCell neighbor in neighbors)
        {
            elevation += neighbor.Elevation;
        }
        elevation /= neighbors.Count;

        cell.Elevation = elevation;
    }
    #endregion

    #region /*------------- RIVERS ------------*/
    protected void GenerateRiver(HexCell from, HexCell to)
    {

    }

    protected void GenerateRiver(HexCell from, HexDirection direction, int length)
    {

    }
    #endregion

    #region /*------------- ROADS -------------*/
    protected void GenerateRoad(HexCell from, HexCell to)
    {

    }

    /// <summary>
    /// Génère une route dans une direction d'une certaine longueur
    /// </summary>
    /// <param name="from"></param>
    /// <param name="direction"></param>
    /// <param name="length"></param>
    protected void GenerateRoad(HexCell from, HexDirection direction, int length)
    {

    }


    #endregion

    #region /*---------- DECORATIONS ----------*/

    #endregion

    #endregion

}
