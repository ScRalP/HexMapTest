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

        //On place entre 5 et 10 rivières sur la carte
        int nbRivers = rand.Next(3, 5);
        List<HexCell> riversEnd = new List<HexCell>();
        for (int i = 0; i < nbRivers; i++)
        {
            HexCell riverEnd = GenerateRiver();
            riversEnd.Add(riverEnd);
        }

        //On ajoutes des lacs (si possible) au bout des rivières
        foreach(HexCell cell in riversEnd)
        {
            //On vérifie récursivement si les voisins sont apte à acceuilir un lac
            //todo : water level
        }

        //Créer plusieurs villes en fonction de la taille de la carte (nombre de cell)
        int nbCities = 1 + (grid.GetCells().Length / 150);
        for(int i = 0; i<nbCities; i++)
        {
            int minLength = rand.Next(Math.Min(grid.chunkCountX , grid.chunkCountZ), Math.Max(grid.chunkCountX , grid.chunkCountZ));
            int maxLength = rand.Next(         grid.chunkCountX + grid.chunkCountZ ,          grid.chunkCountX * grid.chunkCountZ );

            int citySize = rand.Next(minLength, maxLength);//todo: random map size
            GenerateCity(citySize);
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
        int minLength = rand.Next(Math.Min(grid.chunkCountX , grid.chunkCountZ), Math.Max(grid.chunkCountX , grid.chunkCountZ));
        int maxLength = rand.Next(         grid.chunkCountX + grid.chunkCountZ ,          grid.chunkCountX * grid.chunkCountZ );

        int riverLength = rand.Next(minLength, maxLength); //todo: faire varier en fonction de la taille de la map

        //get rand direction
        HexDirection randDirection = (HexDirection)directions.GetValue(rand.Next(directions.Length));

        //Tracer la rivière 
        return GenerateRiver(from, randDirection, riverLength);
    }

    private void GenerateCity(int citySize)
    {
        //On détermine le centre de la ville
        int randX = rand.Next(grid.chunkCountX * HexMetrics.chunkSizeX);
        int randZ = rand.Next(grid.chunkCountX * HexMetrics.chunkSizeZ / 2);
        Debug.Log(randX + ":" + randZ + " - " + citySize);

        HexCell cityCenter = grid.GetCell(new HexCoordinates(randX - randZ / 2, randZ));
        SetBiomeCells(cityCenter, citySize);

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
}
