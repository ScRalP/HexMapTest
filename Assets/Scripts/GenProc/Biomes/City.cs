using System;
using UnityEngine;

public class City : Biome
{
    public City(HexGrid grid, System.Random rand) : base(grid, rand) { }

    public override void Generate()
    {

        //Elevation des cases
        RandomizeElevation();
        FlattenCells(grid.GetCells(), 0.8);

        //On place entre 5 et 10 rivières sur la carte
        int nbRivers = rand.Next(5, 10);
        for (int i = 0; i < nbRivers; i++)
        {
            GenerateRiver();
        }

        //Créer plusieurs villes en fonction de la taille de la carte (nombre de cell)
        int nbCities = 1 + (grid.GetCells().Length / 300);

        Debug.Log("cells X : "+grid.chunkCountX * HexMetrics.chunkSizeX);
        Debug.Log("cells Z : "+grid.chunkCountZ * HexMetrics.chunkSizeZ);

        for(int i = 0; i<nbCities; i++)
        {
            GenerateCity();
        }

    }

    private void GenerateRiver()
    {
        //Prendre la cellule la plus haute
        HexCell highestCellAvailable = grid.GetCells()[0];
        foreach (HexCell cell in grid.GetCells())
        {
            if (cell.Elevation > highestCellAvailable.Elevation && !cell.HasRiver)
            {
                highestCellAvailable = cell;
            }
        }

        HexCell from = highestCellAvailable;

        //taille de la rivière
        int riverLength = rand.Next(10, 30); //todo: faire varier en fonction de la taille de la map

        //get rand direction
        HexDirection randDirection = (HexDirection)directions.GetValue(rand.Next(directions.Length));

        //Tracer la rivière 
        GenerateRiver(from, randDirection, riverLength);
    }

    private void GenerateCity()
    {
        //On détermine le centre de la ville
        int randX = rand.Next(grid.chunkCountX * HexMetrics.chunkSizeX);
        int randZ = rand.Next(grid.chunkCountZ * HexMetrics.chunkSizeZ);

        HexCell cityCenter = grid.GetCell(new HexCoordinates(randX, randX + randZ / 2)); //todo: randomize

        Debug.Log(cityCenter);

        for (int i = 0; i < 13; i++)
        {
            HexDirection randDir = (HexDirection)directions.GetValue(rand.Next(directions.Length));
            int roadLength = rand.Next(0, 9);
            GenerateRoad(cityCenter, randDir, roadLength, 0.5);
        }
    }
}
