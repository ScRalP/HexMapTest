using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : IMapGenerator
{
    private HexCell[] cells { get; set; }

    public Village(HexCell[] cells)
    {
        this.cells = cells;
    }

    public void Generate()
    {

    }
}