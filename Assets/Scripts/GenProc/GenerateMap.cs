﻿using System;

public class GenerateMap
{
    private HexGrid grid;
    private Random rand;
    private Biome[] biomePool;

    public GenerateMap(HexGrid grid)
    {
        this.grid = grid;
        this.rand = new Random();
        this.biomePool = new Biome[] { /*new Desert(grid), new Village(grid), new Island(grid), new Forest(grid),*/ new City(grid) };
    }

    /// <summary>
    /// Procedural generation of the map
    /// </summary>
    public void Generate()
    {
        //Tirage d'un biome & génération
        Biome biome = biomePool[rand.Next(biomePool.Length)];
        biome.Generate();
    }

}
