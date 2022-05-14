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
            cell.Color = Color.yellow;
         }
         else
         {
            cell.Color = Color.green;
         }
      }
   }
   public override void FillPrefabsTab()
   {

   }
}