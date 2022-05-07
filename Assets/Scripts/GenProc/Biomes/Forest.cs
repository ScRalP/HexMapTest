using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : IMapGenerator
{
    private HexCell[] cells { get; set; }

    public Forest(HexCell[] cells)
    {
        this.cells = cells;
    }

    public void Generate()
    {

    }
}