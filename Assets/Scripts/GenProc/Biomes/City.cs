using System;
using System.Collections.Generic;
using UnityEngine;

public class City : Biome
{
    public City(HexGrid grid, System.Random rand) : base(grid, rand) { }

    public override void Generate()
    {
        SetBiomeCells();
        FillPrefabsTab();


        //Elevation des cases
        RandomizeElevation();
        FlattenCells(grid.GetCells(), 0.8);

        //On place les rivières
        int nbRivers = rand.Next(3, 5);
        List<HexCell> riversEnd = new List<HexCell>();
        for (int i = 0; i < nbRivers; i++)
        {
            HexCell riverEnd = GenerateRiver();
            riversEnd.Add(riverEnd);
        }

        List<HexCell> nonFloodedCells = new List<HexCell>( grid.GetCells() );
        //On ajoutes des lacs (si possible) au bout des rivières
        foreach (HexCell cell in riversEnd)
        {
            if (cell.WaterLevel == 0)
            {
                //Ajout de lacs
                List<HexCell> waterCells = new List<HexCell>();
                int waterLevel = cell.Elevation + 1;
                GenerateWater(cell, waterCells, waterLevel);

                //Si on remplis pas trop la carte d'eau
                if (((double)waterCells.Count / (double)grid.GetCells().Length) < 0.5)
                {
                    //On ajoutes l'eau aux tuiles
                    foreach (HexCell waterCell in waterCells)
                    {
                        waterCell.WaterLevel = waterLevel;
                        nonFloodedCells.Remove(waterCell);
                    }
                }
            }
        }

        //Créer plusieurs villes en fonction du nombre de cellules restantes
        List<HexCell> citiesCenter = new List<HexCell>();
        int nbCities = 1 + (nonFloodedCells.Count / 250);
        for (int i = 0; i < nbCities; i++)
        {
            int minLength = rand.Next(Math.Min(grid.chunkCountX, grid.chunkCountZ), Math.Max(grid.chunkCountX, grid.chunkCountZ));
            int maxLength = rand.Next(grid.chunkCountX + grid.chunkCountZ, grid.chunkCountX * grid.chunkCountZ);

            int citySize = rand.Next(minLength, maxLength);
            GenerateCity(citySize, citiesCenter, nonFloodedCells);
        }

        //Mettre de la couleur sur les cases
        foreach (HexCell cell in grid.GetCells())
        {
            ChangeColor(cell);
        }

        //Ajouter du décors
        foreach (HexCell cell in grid.GetCells())
        {
            if(cell.Color == Colors.FOREST && !cell.HasRiver && !cell.HasRoads) // add Trees
            {
                //Ajouter des arbres
                int nbTrees = rand.Next(2, 8);
                for(int i = 0; i<nbTrees; i++)
                {
                    Quaternion randRotation = Quaternion.Euler(0, (float)rand.Next(360), 0);
                    float randTransformX = (float)(rand.NextDouble() - 0.5);
                    float randTransformZ = (float)(rand.NextDouble() - 0.5);

                    Draw3DObject(cell, Prefabs.CEDAR_TREE, randTransformX, randTransformZ, randRotation);
                }
            } else if(cell.Color == Colors.SAND)
            {
                //Ajouter des palmiers
                if(rand.NextDouble() < 0.5)
                {
                    Quaternion randRotation = Quaternion.Euler(0, (float)rand.Next(360), 0);
                    float randTransformX = (float)(rand.NextDouble() - 0.5);
                    float randTransformZ = (float)(rand.NextDouble() - 0.5);

                    Draw3DObject(cell, Prefabs.PALM_TREE, randTransformX, randTransformZ, randRotation);
                }
            }
            
            if (cell.HasRoads)
            {
                for (int i = 0; i < rand.Next(2); i++)
                {
                    GameObject[] houses = new GameObject[] { Prefabs.CHIKITORA_HOUSE, Prefabs.CYNDAQUIL_HOUSE, Prefabs.TOTODILE_HOUSE };

                    //Ajouter des habitations
                    Quaternion randRotation = Quaternion.Euler(0, (float)rand.Next(360), 0);
                    float randTransformX = (float)(rand.NextDouble() - 0.5);
                    float randTransformZ = (float)(rand.NextDouble() - 0.5);

                    Draw3DObject(cell, houses[rand.Next(houses.Length)], randTransformX, randTransformZ, randRotation);
                }
            }
        }
    }

    private HexCell GenerateRiver()
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
        int minLength = rand.Next(Math.Min(grid.chunkCountX, grid.chunkCountZ), Math.Max(grid.chunkCountX, grid.chunkCountZ));
        int maxLength = rand.Next(grid.chunkCountX + grid.chunkCountZ, grid.chunkCountX * grid.chunkCountZ);

        int riverLength = rand.Next(minLength, maxLength); //todo: faire varier en fonction de la taille de la map

        //get rand direction
        HexDirection randDirection = (HexDirection)directions.GetValue(rand.Next(directions.Length));

        //Tracer la rivière 
        return GenerateRiver(from, randDirection, riverLength);
    }

    private void GenerateWater(HexCell currentCell, List<HexCell> cells, int waterLevel)
    {
        cells.Add(currentCell);
        foreach (HexDirection direction in Enum.GetValues(typeof(HexDirection)))
        {
            HexCell neighbor = currentCell.GetNeighbor(direction);
            if (neighbor != null)
            {
                if (neighbor.Elevation < waterLevel && !cells.Contains(neighbor))
                {
                    GenerateWater(neighbor, cells, waterLevel);
                }
            }
        }
    }

    private void GenerateCity(int citySize, List<HexCell> citiesCenter, List<HexCell> nonFloodedCells)
    {
        HexCell cityCenter = null;
        bool isTooCloseFromOtherCity;
        do {
            isTooCloseFromOtherCity = false;

            int indexToRemove = rand.Next(nonFloodedCells.Count - 1);
            cityCenter = nonFloodedCells[indexToRemove]; 
            nonFloodedCells.RemoveAt(indexToRemove);

            foreach (HexCell cell in citiesCenter)
            {
                if (HexMetrics.DistanceBetweenCells(cityCenter, cell) < citySize)
                {
                    isTooCloseFromOtherCity = true;
                }
            }
        } while (cityCenter.WaterLevel > cityCenter.Elevation || isTooCloseFromOtherCity && nonFloodedCells.Count > 0);


        if (cityCenter != null)
        {
            citiesCenter.Add(cityCenter);

            int nbRoads = rand.Next(citySize, citySize * 3);
            for (int i = 0; i < nbRoads; i++)
            {
                HexDirection randDir = (HexDirection)directions.GetValue(rand.Next(directions.Length));
                int roadLength = rand.Next(0, citySize);
                GenerateRoad(cityCenter, randDir, roadLength, 0.5);
            }
        }
    }

    public override void FillPrefabsTab()
   {
      prefabs = new GameObject[5];
      prefabs[0] = Resources.Load<GameObject>("Prefabs/Flag");
      prefabs[1] = Resources.Load<GameObject>("Prefabs/Cyndaquil House");
      prefabs[2] = Resources.Load<GameObject>("Prefabs/Chikitora House");
      prefabs[3] = Resources.Load<GameObject>("Prefabs/Totodile House");
      prefabs[4] = Resources.Load<GameObject>("Prefabs/Cedar Tree");
   }

   public override int Choose3DObject(Vector2 position, HexCell parent, bool firstElement)
   {
      int obj = 0; // ID pour le premier objet

      if (!firstElement)
      {
         if(parent.Color == Colors.FOREST) // Si la tuile est recouverte d'herbe
         {
            obj = 5; // Place des arbres sur l'herbe
         }
         else
         {
            obj = UnityEngine.Random.Range(1, 4); // Place des habitations
         }
      }

      return obj;
   }

    private void ChangeColor(HexCell cell)
    {
        if (cell.Elevation > 5) { // SNOW
            cell.Color = Colors.SNOW;
        }
        else if (cell.Elevation > 3) { // MOUNTAIN
            cell.Color = Colors.MOUNTAIN;
        }
        else if (cell.Elevation < cell.WaterLevel) { // SEA
            //Si la profondeur est élevée
            if ((cell.WaterLevel - cell.Elevation) > 1)
                cell.Color = Colors.DEEP_SEA;
            else
                cell.Color = Colors.SEA;
        }
        else if (cell.HaveFloodedNeighbor()) { // SAND
            cell.Color = Colors.SAND;
        }
        else { // FOREST
            cell.Color = Colors.FOREST;
        }
    }
}
