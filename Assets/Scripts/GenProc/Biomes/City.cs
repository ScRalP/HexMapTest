using System;

public class City : Biome
{
    public City(HexGrid grid) : base(grid) { }

    public override void Generate()
    {
        RandomizeElevation();

        foreach (HexCell cell in grid.GetCells())
        {
            int cumul = 0;
            int nbNeighbor = 0;
            foreach (HexDirection direction in Enum.GetValues(typeof(HexDirection)))
            {
                HexCell neighbor = cell.GetNeighbor(direction);
                if(neighbor != null)
                {
                    nbNeighbor++;
                    cumul += neighbor.Elevation;
                }
            }
            cell.Elevation = cumul / nbNeighbor;
        }



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
