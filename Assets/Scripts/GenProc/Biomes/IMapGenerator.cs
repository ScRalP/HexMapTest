using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Chaque biome se génère de facon différente les un des autres
/// </summary>
public interface IMapGenerator
{
    void Generate();
}
