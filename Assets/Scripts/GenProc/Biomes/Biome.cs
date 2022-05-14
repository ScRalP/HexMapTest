using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Biome : IMapGenerator
{
    protected HexGrid grid;
    protected System.Random rand;
    protected Array directions;
    protected List<HexCell> biomeCells;
    protected GameObject[] prefabs;

    public Biome(HexGrid grid, System.Random rand)
    {
        this.grid = grid;
        this.rand = rand;
        this.directions = Enum.GetValues(typeof(HexDirection));
        this.biomeCells = new List<HexCell>();
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
    protected HexCell GenerateRiver(HexCell start, HexDirection direction, int length, double rigidity = 1)
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

        return currentCell;
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
   public abstract void SetBiomeColor();

   public abstract void FillPrefabsTab();

   protected bool CanDraw3DObject(HexCell[] neighborhood)
   {
      bool canMakeObject = false;

      if (neighborhood[1] != null && neighborhood[2] != null)
      {
         if (neighborhood[0].Elevation == neighborhood[1].Elevation && neighborhood[0].Elevation == neighborhood[2].Elevation)
         {
            canMakeObject = true;
         }
      }
      return canMakeObject;
   }

   protected float Get3DObjectHeight(GameObject prefab)
   {
      float prefabHeight;
      Debug.Log(prefab);

      if (prefab.GetComponent<Renderer>() != null)
      {
         prefabHeight = prefab.GetComponent<Renderer>().bounds.size.y;
      }
      else
      {
         prefabHeight = prefab.GetComponentInChildren<Renderer>().bounds.size.y;
      }

      return prefabHeight;
   }

   protected Vector3 ComputeIntersectionBetweenCells(Vector3 cell1, Vector3 cell2)
   {
      Vector3 intersection = new Vector3();

      intersection.x = cell1.x + (cell2.x - cell1.x) / 2;
      intersection.y = cell1.y + (cell2.y - cell1.y) / 2;
      intersection.z = cell1.z + (cell2.z - cell1.z) / 2;

      return intersection;
   }

   protected void Draw3DObject(Vector2 position, HexCell parent, bool firstElement)
   {
      int obj = 0; //ID for drawing first element

      if(!firstElement)
      {
         obj = UnityEngine.Random.Range(1, prefabs.Length - 1); //Pick another ID
      }

      GameObject prefab = prefabs[obj];
      float prefabHeight = Get3DObjectHeight(prefab);

      // World positionning
      Vector3 worldPos = new Vector3(position.x, parent.Position.y + prefabHeight / 2.0f, position.y);
      GameObject.Instantiate(prefab, worldPos, Quaternion.identity, parent.transform);
   }

   public void Draw()
   {
      FillPrefabsTab();

      Vector2 centerPos = new Vector2(biomeCells[0].Position.x, biomeCells[0].Position.z);

      //Draw first object at center of the biome
      Draw3DObject(centerPos, biomeCells[0], true);

      for (int i = 0; i < biomeCells.Count; i++)
      {
         HexDirection[] directions = { HexDirection.NE, HexDirection.NW, HexDirection.SE, HexDirection.SW, HexDirection.E, HexDirection.W };

         foreach (HexDirection dir in directions)
         {
            bool canMakeObject;

            HexCell[] neighborhood = new HexCell[3];
            neighborhood[0] = biomeCells[i];
            neighborhood[1] = biomeCells[i].GetNeighbor(dir);

            HexCell[] secondNeighbors = {biomeCells[i].GetNeighbor(HexDirectionExtensions.Next(dir)),
                                         biomeCells[i].GetNeighbor(HexDirectionExtensions.Previous(dir)),
                                         biomeCells[i].GetNeighbor(HexDirectionExtensions.Opposite(dir))};

            for (int j = 0; j < secondNeighbors.Length; j++)
            {
               neighborhood[2] = secondNeighbors[j];

               int chance = UnityEngine.Random.Range(0, 30);
               canMakeObject = CanDraw3DObject(neighborhood) && (chance % 6 == 0);

               if (canMakeObject)
               {
                  Vector3 temp1 = ComputeIntersectionBetweenCells(neighborhood[0].Position, neighborhood[1].Position);
                  Vector3 temp2 = ComputeIntersectionBetweenCells(neighborhood[0].Position, neighborhood[2].Position);

                  Vector3 tempPosition = ComputeIntersectionBetweenCells(temp1, temp2);
                  Vector2 position = new Vector2(tempPosition.x, tempPosition.z);

                  if (!grid.GetObjPositions().Contains(position))
                  {
                     grid.GetObjPositions().Add(position);
                     Draw3DObject(position, neighborhood[0], false);
                  }
               }
            }
         }
      }
   }
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

   public void SetBiomeCells(HexCell center, int size)
   {
      biomeCells.Add(center);
      HexCell currentCell = center;

      for(int cell = 0; cell <= size/2; cell++)
      {
         for(int dir = 0; dir <= 5; dir++)
         {
            HexCell neighbor = currentCell.GetNeighbor((HexDirection)dir);

            if (neighbor != null)
            {
               biomeCells.Add(neighbor);
               currentCell = neighbor;
            }
         }
      }
   }
}
