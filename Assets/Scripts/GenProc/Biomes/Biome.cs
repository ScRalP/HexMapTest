using System;
using System.Collections.Generic;

public abstract class Biome : IMapGenerator
{
    protected HexGrid grid;
    protected Random rand;
    public Biome(HexGrid grid, Random rand)
    {
        this.grid = grid;
        this.rand = rand;
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

    protected void FlattenCells(HexCell[] cells, double ratio = 1)
    {
        foreach(HexCell cell in cells)
        {
            FlattenCell(cell, ratio);
        }
    }

    protected void FlattenCell(HexCell cell, double ratio = 1)
    {
        int cumul = 0;
        int nbNeighbor = 0;

        foreach (HexDirection direction in Enum.GetValues(typeof(HexDirection)))
        {
            //Ajoutes de facon aléatoire un voisin ou non
            if (rand.NextDouble() < ratio)
            {
                HexCell neighbor = cell.GetNeighbor(direction);
                if (neighbor != null)
                {
                    nbNeighbor++;
                    cumul += neighbor.Elevation;
                }
            }
        }

        if (nbNeighbor != 0)
        {
            cell.Elevation = cumul / nbNeighbor;
        }

    }
    #endregion

    #region /*------------- RIVERS ------------*/
    protected void GenerateRiver(HexCell from, HexCell to)
    {
        while(!from.coordinates.Equals(to.coordinates))
        {
            //Choisir une direction

        }
    }

    protected void GenerateRiver(HexCell start, HexDirection direction, int length)
    {
        List<HexCell> riverCells = new List<HexCell>();
        HexCell currentCell = start;

        for(int i = 0; i < length; i++)
        {
            //Create a pool of direction with only available forward ones and in a precise order
            List<HexDirection> neighbors = new List<HexDirection>();
            if (currentCell.GetNeighbor(direction.Previous2()) != null && !riverCells.Contains(currentCell.GetNeighbor(direction.Previous2()))) neighbors.Add(direction.Previous2());
            if (currentCell.GetNeighbor(direction.Previous ()) != null && !riverCells.Contains(currentCell.GetNeighbor(direction.Previous ()))) neighbors.Add(direction.Previous ());
            if (currentCell.GetNeighbor(direction            ) != null && !riverCells.Contains(currentCell.GetNeighbor(direction            ))) neighbors.Add(direction            );
            if (currentCell.GetNeighbor(direction.Next     ()) != null && !riverCells.Contains(currentCell.GetNeighbor(direction.Next()     ))) neighbors.Add(direction.Next     ());
            if (currentCell.GetNeighbor(direction.Next2    ()) != null && !riverCells.Contains(currentCell.GetNeighbor(direction.Next2()    ))) neighbors.Add(direction.Next2    ());

            bool cellValid;
            do
            {
                cellValid = false;

                //Les directions étant placés dans l'ordre on viens en prendre une vers le milieu
                int randIndex = GetRandomCenteralLimitedValueBetween(0, riverCells.Count);
                HexDirection randDirection = neighbors[randIndex];
                HexCell randNeighbor = currentCell.GetNeighbor(randDirection); //on récupère la cellule qui correspond

                if(randNeighbor.Elevation > currentCell.Elevation) //Inaccessible pour une rivière
                {
                    //Remove from neighbors list
                    neighbors.RemoveAt(randIndex);
                } else { //Cellule valide
                    //Create river between cells
                    currentCell.SetOutgoingRiver(randDirection);

                    //Add it to list
                    riverCells.Add(randNeighbor);

                    //Change current cell
                    currentCell = randNeighbor;
                }
            } while (neighbors.Count > 0 || !cellValid);

            //create river between cells

            //Add it to list

            //Check if has neighbor with lower value
        }
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

    public int GetRandomCenteralLimitedValueBetween(int min, int max, int nbIterations = 100)
    {
        int result = 0;
        for(int i = 0; i < nbIterations; i++)
        {
            result += rand.Next(min, max);
        }

        return result / nbIterations;
    }

}
