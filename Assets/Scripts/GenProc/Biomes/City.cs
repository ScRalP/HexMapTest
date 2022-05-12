using System;
using System.Collections.Generic;
using UnityEngine;

public class City : Biome
{
    public City(HexGrid grid, System.Random rand) : base(grid, rand) { }

    public override void Generate()
    {
        Array directions = Enum.GetValues(typeof(HexDirection));

        //Elevation des cases
        RandomizeElevation();
        FlattenCells(grid.GetCells(), 0.8);

        //On place entre 5 et 10 rivières sur la carte
        int nbRivers = rand.Next(5, 10);
        for (int i = 0; i < nbRivers; i++)
        {
            //Prendre la cellule la plus haute
            HexCell highestCellAvailable = grid.GetCells()[0];
            foreach(HexCell cell in grid.GetCells())
            {
                if(cell.Elevation > highestCellAvailable.Elevation && !cell.HasRiver)
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

        //On détermine le centre de la ville
        HexCell cityCenter = grid.GetCell(new HexCoordinates(5, 5)); //todo: randomize
        for (int i = 0; i < 10; i++)
        {
            HexDirection randDir = (HexDirection)directions.GetValue(rand.Next(directions.Length));
            GenerateRoad(cityCenter, randDir, 15, 0.5);
        }

    }
}
