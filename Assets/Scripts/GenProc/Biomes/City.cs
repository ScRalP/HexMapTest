using System;
using System.Collections.Generic;

public class City : Biome
{
    public City(HexGrid grid, Random rand) : base(grid, rand) { }

    public override void Generate()
    {
        //Elevation des cases
        RandomizeElevation();
        FlattenCells(grid.GetCells(), 0.8);

        //On place une ou deux rivières sur la carte
        int nbRivers = rand.Next(1, 3);
        for(int i = 0; i < nbRivers; i++)
        {
            //prendre des cases aléatoires
            HexCell from = grid.GetCells()[rand.Next(grid.GetCells().Length)];

            //taille de la rivière
            int riverLength = rand.Next(10, 30); //Todo: faire varier en fonction de la taille de la map

            //get rand direction
            Array directions = Enum.GetValues(typeof(HexDirection));
            HexDirection randDirection = (HexDirection)directions.GetValue(rand.Next(directions.Length));

            //Tracer la rivière 
            GenerateRiver(from, randDirection, riverLength);
            



        }



        //On détermine les emplacements des routes



        //HexCoordinates cityCenter = new HexCoordinates(10, 10); //random
        //HexCell cellCenter = grid.GetCell(cityCenter);

        //for (int i = 0; i < 3; i++)
        //{
        //}

        //On adaptes les tuiles adjacentes pour concorder avec les routes

    }
}
