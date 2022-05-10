using System;
using System.Collections.Generic;
using UnityEngine;

public class City : Biome
{
    public City(HexGrid grid, System.Random rand) : base(grid, rand) { }

    public override void Generate()
    {
        //Elevation des cases
        RandomizeElevation();
        FlattenCells(grid.GetCells(), 0.8);

        //On place une ou deux rivières sur la carte
        int nbRivers = rand.Next(5, 10);
        Debug.Log("nb rivers" + nbRivers);
        for(int i = 0; i < nbRivers; i++)
        {
            //Trouver un point haut
            List<HexCell> highCells = new List<HexCell>();
            foreach(HexCell cell in grid.GetCells())
            {
                if(cell.Elevation > 4 && (!cell.HasOutgoingRiver||!cell.HasIncomingRiver) )
                {
                    highCells.Add(cell);
                }
            }
            HexCell from = highCells[rand.Next(highCells.Count)];

            Debug.Log("high cells : " + highCells.Count);

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
