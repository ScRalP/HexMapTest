using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsManager : MonoBehaviour
{
   HexCell[] cells;
   bool continueUpdate;
   public GameObject prefabTest;
   List<Vector3> objPositions;

    // Start is called before the first frame update
    void Start()
    {
      cells = GameObject.Find("Hex Grid").GetComponent<HexGrid>().GetCells();
      objPositions = new List<Vector3>();

      continueUpdate = true;
    }

   bool CanDraw(HexCell[] neighborhood)
   {
      bool canMakeObject = false;

      if (neighborhood[1] != null && neighborhood[2] != null)
      {
         if(neighborhood[0].Elevation == neighborhood[1].Elevation && neighborhood[0].Elevation == neighborhood[2].Elevation)
         {
            canMakeObject = true;
         }
      }
      return canMakeObject;
   }

   Vector3 ComputeIntersection(Vector3 cell1, Vector3 cell2)
   {
      Vector3 intersection = new Vector3();

      intersection.x = cell1.x + (cell2.x - cell1.x) / 2;
      intersection.y = cell1.y + (cell2.y - cell1.y) / 2;
      intersection.z = cell1.z + (cell2.z - cell1.z) / 2;

      return intersection;
   }

    // Update is called once per frame
    void Update()
    {
        int i = 0;

        if(continueUpdate)
        {
            for (; i < cells.Length; i++)
            {
               HexDirection[] directions = { HexDirection.NE, HexDirection.NW, HexDirection.SE, HexDirection.SW, HexDirection.E, HexDirection.W };

               foreach (HexDirection dir in directions)
               {
                  bool canMakeObject;

                  HexCell[] neighborhood = new HexCell[3];
                  neighborhood[0] = cells[i];
                  neighborhood[1] = cells[i].GetNeighbor(dir);

                  HexCell[] secondNeighbors = {cells[i].GetNeighbor(HexDirectionExtensions.Next(dir)),
                                               cells[i].GetNeighbor(HexDirectionExtensions.Previous(dir)),
                                               cells[i].GetNeighbor(HexDirectionExtensions.Opposite(dir))};

                  for (int j = 0; j < secondNeighbors.Length; j++)
                  {
                     neighborhood[2] = secondNeighbors[j];

                     int chance = Random.Range(0, 30);
                     Debug.Log(chance);
                     canMakeObject = CanDraw(neighborhood) && (chance % 4 == 0);

                     if (canMakeObject)
                     {
                        Vector3 temp1 = ComputeIntersection(neighborhood[0].transform.position, neighborhood[1].transform.position);
                        Vector3 temp2 = ComputeIntersection(neighborhood[0].transform.position, neighborhood[2].transform.position);

                        Vector3 objectPosition = ComputeIntersection(temp1, temp2);

                        if (!objPositions.Contains(objectPosition))
                        {
                           objPositions.Add(objectPosition);
                           Instantiate(prefabTest, objectPosition, Quaternion.identity);
                        }
                     }
                  }
               }
            }
         }
        
        if(i == cells.Length)
        {
            continueUpdate = false;
        }
    }
}
