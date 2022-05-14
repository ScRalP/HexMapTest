using System;
using UnityEngine;

public class Desert : Biome
{
    public Desert(HexGrid grid, System.Random rand) : base(grid, rand) { }

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
            cell.Color = Color.green;
         }
         else
         {
            cell.Color = Color.yellow;
         }
      }
   }
}