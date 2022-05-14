using UnityEngine;
using System;

public class Forest : Biome
{
    public Forest(HexGrid grid, System.Random rand) : base(grid, rand) { }

    public override void Generate()
    {
    }

   public override void SetBiomeColor()
   {
      foreach (HexCell cell in biomeCells)
      {
         int colorChance = UnityEngine.Random.Range(0, 10);

         if (colorChance == 0)
         {
            cell.Color = sandColor;
         }
         else if (colorChance >= 1 && colorChance <= 2)
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

   }
   public override int Choose3DObject(Vector2 position, HexCell parent, bool firstElement)
   {
      int obj = 0; //ID pour le premier objet

      return obj;
   }
}