using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Biome : IMapGenerator
{
    protected HexGrid grid;
    protected System.Random rand;
    protected Array directions;

    public Biome(HexGrid grid, System.Random rand)
    {
        this.grid = grid;
        this.rand = rand;
        this.directions = Enum.GetValues(typeof(HexDirection));
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

    }

    /// <summary>
    /// Génère une rivière dans une direction d'une certaine longueur
    /// </summary>
    /// <param name="start">Point de départ de la route</param>
    /// <param name="direction">Direction voulue</param>
    /// <param name="length">Longueur de la route</param>
    protected void GenerateRiver(HexCell start, HexDirection direction, int length, double rigidity = 1)
    {
        List<HexCell> riverCells = new List<HexCell>();
        HexCell currentCell = start;

        for(int i = 0; i < length; i++)
        {
            //Create a pool of direction with only available forward ones and in a precise order
            List<HexDirection> directions = new List<HexDirection>();
            if (currentCell.GetNeighbor(direction.Previous2()) != null && !riverCells.Contains(currentCell.GetNeighbor(direction.Previous2()))) directions.Add(direction.Previous2());
            if (currentCell.GetNeighbor(direction.Previous ()) != null && !riverCells.Contains(currentCell.GetNeighbor(direction.Previous ()))) directions.Add(direction.Previous ());
            if (currentCell.GetNeighbor(direction            ) != null && !riverCells.Contains(currentCell.GetNeighbor(direction            ))) directions.Add(direction            );
            if (currentCell.GetNeighbor(direction.Next     ()) != null && !riverCells.Contains(currentCell.GetNeighbor(direction.Next()     ))) directions.Add(direction.Next     ());
            if (currentCell.GetNeighbor(direction.Next2    ()) != null && !riverCells.Contains(currentCell.GetNeighbor(direction.Next2()    ))) directions.Add(direction.Next2    ());

            bool cellValid = false;
            while (directions.Count > 0 && !cellValid)
            {
                cellValid = false;

                //Les directions étant placés dans l'ordre on viens en prendre une vers le milieu
                int randIndex = GetRandomCenteralLimitedValueBetween(0, directions.Count, rigidity);

                HexDirection randDirection = directions[randIndex];
                HexCell randNeighbor = currentCell.GetNeighbor(randDirection); //on récupère la cellule qui correspond

                if(randNeighbor.Elevation > currentCell.Elevation) //Inaccessible pour une rivière
                {
                    //Remove from direction list
                    directions.RemoveAt(randIndex);
                } else { //Cellule valide
                    cellValid = true;

                    //Create river between cells
                    currentCell.SetOutgoingRiver(randDirection);

                    //Add it to list
                    riverCells.Add(randNeighbor);

                    //Change current cell
                    currentCell = randNeighbor;
                }
            }
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
    /// <param name="start">Point de départ de la route</param>
    /// <param name="direction">Direction voulue</param>
    /// <param name="length">Longueur de la route</param>
    /// <param name="rigidity">A quel point la route doit suivre la direction (0 = peu rigide, 1 = tres rigide)</param>
    protected void GenerateRoad(HexCell start, HexDirection direction, int length, double rigidity = 1)
    {
        HexCell currentCell = start;

        for (int i = 0; i < length; i++)
        {
            //Create a pool of direction with only available forward ones and in a precise order
            List<HexDirection> directions = new List<HexDirection>();
            if (currentCell.GetNeighbor(direction.Previous2()) != null) directions.Add(direction.Previous2());
            if (currentCell.GetNeighbor(direction.Previous ()) != null) directions.Add(direction.Previous ());
            if (currentCell.GetNeighbor(direction            ) != null) directions.Add(direction            );
            if (currentCell.GetNeighbor(direction.Next     ()) != null) directions.Add(direction.Next     ());
            if (currentCell.GetNeighbor(direction.Next2    ()) != null) directions.Add(direction.Next2    ());

            bool cellValid = false;
            while (directions.Count > 0 && !cellValid)
            {
                cellValid = false;

                //Les directions étant placés dans l'ordre on viens en prendre une vers le milieu
                int randIndex = GetRandomCenteralLimitedValueBetween(0, directions.Count, rigidity);

                HexDirection randDirection = directions[randIndex];
                HexCell randNeighbor = currentCell.GetNeighbor(randDirection); //on récupère la cellule qui correspond

                if (Math.Abs(randNeighbor.Elevation - currentCell.Elevation) >= 2 && randNeighbor.WaterLevel < randNeighbor.Elevation) //Inaccessible pour une route
                {
                    //Remove from direction list
                    directions.RemoveAt(randIndex);
                }
                else
                { //Cellule valide
                    cellValid = true;

                    //Create road between cells
                    currentCell.AddRoad(randDirection);

                    //Change current cell
                    currentCell = randNeighbor;
                }
            }
        }

    }

    #endregion

    #region /*---------- DECORATIONS ----------*/

    #endregion

    #endregion

    public int GetRandomCenteralLimitedValueBetween(int min, int max, double rigidity = 1)
    {
        int nbIterations = (int)(10 * rigidity);

        if (min > max) return 0;

        int result = 0;
        for(int i = 0; i < nbIterations; i++)
        {
            result += rand.Next(min, max);
        }

        return (int)Math.Floor((double)(result / nbIterations));
    }

}
