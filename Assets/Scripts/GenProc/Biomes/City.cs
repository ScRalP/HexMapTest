﻿using System;
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

        //On place les rivières
        int nbRivers = rand.Next(3, 5);
        List<HexCell> riversEnd = new List<HexCell>();
        for (int i = 0; i < nbRivers; i++)
        {
            HexCell riverEnd = GenerateRiver();
            riversEnd.Add(riverEnd);
        }

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
                    }
                }
            }

        }

        //Créer plusieurs villes en fonction de la taille de la carte (nombre de cell)
        int nbCities = 1 + (grid.GetCells().Length / 150);
        for (int i = 0; i < nbCities; i++)
        {
            int minLength = rand.Next(Math.Min(grid.chunkCountX, grid.chunkCountZ), Math.Max(grid.chunkCountX, grid.chunkCountZ));
            int maxLength = rand.Next(grid.chunkCountX + grid.chunkCountZ, grid.chunkCountX * grid.chunkCountZ);

            int citySize = rand.Next(minLength, maxLength);//todo: random map size
            GenerateCity(citySize);
        }

        //Mettre de la couleur sur les cases
        foreach (HexCell cell in grid.GetCells())
        {
            ChangeColor(cell);
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

    private void GenerateCity(int citySize)
    {
        HexCell cityCenter;
        do
        {
            //On détermine le centre de la ville
            int randX = rand.Next(grid.chunkCountX * HexMetrics.chunkSizeX);
            int randZ = rand.Next(grid.chunkCountX * HexMetrics.chunkSizeZ / 2);

            cityCenter = grid.GetCell(new HexCoordinates(randX - randZ / 2, randZ));
        SetBiomeCells(cityCenter, citySize);

        } while (cityCenter.WaterLevel > cityCenter.Elevation);

        int nbRoads = rand.Next(citySize, citySize * 3);
        for (int i = 0; i < nbRoads; i++)
        {
            HexDirection randDir = (HexDirection)directions.GetValue(rand.Next(directions.Length));
            int roadLength = rand.Next(0, citySize);
            GenerateRoad(cityCenter, randDir, roadLength, 0.5);
        }
    }

   public override void SetBiomeColor()
   {
      foreach(HexCell cell in biomeCells)
      {
         int colorChance = UnityEngine.Random.Range(0, 10);

         if(colorChance <= 2)
         {
            cell.Color = sandColor;
         }
         else if (colorChance >= 3 && colorChance <= 7)
         {
            cell.Color = stoneColor;
         }
         else
         {
            cell.Color = grassColor;
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
         if(parent.Color == grassColor) // Si la tuile est recouverte d'herbe
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
        if (cell.Elevation < cell.WaterLevel) { // SEA
            //Si la profondeur est élevée
            if ((cell.WaterLevel - cell.Elevation) > 1)
                cell.Color = Colors.DEEP_SEA;
            else
                cell.Color = Colors.SEA;
        }
        else if (cell.HaveFloodedNeighbor()) { // SAND
            cell.Color = Colors.SAND;
        }
        else if (cell.HasRoads) { // ROAD
            cell.Color = Colors.PAVED_ROAD;
        }
        else if (cell.Elevation > 5) { // SNOW
            cell.Color = Colors.SNOW;
        }
        else if (cell.Elevation > 3) { // MOUNTAIN
            cell.Color = Colors.MOUNTAIN;
        }
        else { // FOREST
            cell.Color = Colors.FOREST;
        }
    }
}
