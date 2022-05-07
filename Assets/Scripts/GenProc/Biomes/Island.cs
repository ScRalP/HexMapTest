using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : IMapGenerator
{
    private HexCell[] cells { get; set; }

    public Island(HexCell[] cells)
    {
        this.cells = cells;
    }

    public void Generate()
    {

    }
}