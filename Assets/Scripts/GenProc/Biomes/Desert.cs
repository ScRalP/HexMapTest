using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desert : IMapGenerator
{
    private HexCell[] cells { get; set; }

    public Desert(HexCell[] cells)
    {
        this.cells = cells;
    }

    public void Generate()
    {

    }
}