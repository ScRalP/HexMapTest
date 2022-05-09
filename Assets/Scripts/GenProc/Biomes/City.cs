using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : Biome
{
    public City(HexGrid grid) : base(grid) { }

    public void Generate()
    {
        RandomizeElevation();
        FlattenCells(grid.cells);

        //On place une ou deux rivières sur la carte


        //On détermine les emplacements des routes

        //HexCoordinates cityCenter = new HexCoordinates(10, 10); //random
        //HexCell cellCenter = grid.GetCell(cityCenter);

        //for (int i = 0; i < 3; i++)
        //{
        //}

        //On adaptes les tuiles adjacentes pour concorder avec les routes

        //
    }
}
