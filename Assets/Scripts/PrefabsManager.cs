using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsManager : MonoBehaviour
{
   HexCell[] cells;
   bool continueUpdate;
   public GameObject[] prefabs;
   List<Vector2> objPositions;

    // Start is called before the first frame update
    void Start()
    {
      cells = GetComponent<HexGrid>().GetCells();
      objPositions = new List<Vector2>();

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
                     canMakeObject = CanDraw(neighborhood) && (chance % 6 == 0);

                     if (canMakeObject)
                     {
                        Vector3 temp1 = ComputeIntersection(neighborhood[0].Position, neighborhood[1].Position);
                        Vector3 temp2 = ComputeIntersection(neighborhood[0].Position, neighborhood[2].Position);

                        Vector3 tempPosition = ComputeIntersection(temp1, temp2);
                        Vector2 position = new Vector2(tempPosition.x, tempPosition.z);

                        if (!objPositions.Contains(position))
                        {
                           objPositions.Add(position);
                           int obj = Random.Range(0, prefabs.Length);
                           GameObject prefab = prefabs[obj];
                           float prefabHeight;
                           if (prefab.GetComponent<Renderer>() != null)
                           {
                              prefabHeight = prefab.GetComponent<Renderer>().bounds.size.y;
                           }
                           else
                           {
                              prefabHeight = prefab.GetComponentInChildren<Renderer>().bounds.size.y;
                           }
                           
                           Vector3 worldPos = new Vector3(position.x, neighborhood[0].Position.y+prefabHeight/2.0f, position.y);
                           Instantiate(prefab, worldPos, Quaternion.identity, neighborhood[0].transform);
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
