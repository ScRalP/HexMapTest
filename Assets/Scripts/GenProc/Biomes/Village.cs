
using System;
using UnityEngine;

public class Village : Biome
{
    public Village(HexGrid grid, System.Random rand) : base(grid, rand) { }

    public override void Generate()
    {

    }

   public override void SetBiomeColor()
   {
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