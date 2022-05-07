using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : IMapGenerator
{
    private HexCell[] cells { get; set; }

    public City(HexCell[] cells)
    {
        this.cells = cells;
    }

    public void Generate()
    {

    }
}
