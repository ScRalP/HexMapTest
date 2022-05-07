/// <summary>
/// On vient mettre ici tout les paramètres pour la génération de la map
/// On les sépares en biomes
/// Un biome sera tiré aléatoirement au moment de la génération
/// </summary>
public class GenerationVariables
{
    #region Liste des variables

    // Relief
    public int FlattenRatio { get; set; } // entre 0 et 1 (chances que la case soit lissé : 1 = lissé - 0 = pas lissé)
   
    // Décors
    public double BuildingSpawnRatio { get; set; } // entre 0 et 1, taux d'apparition de batiments (immeuble, gare, maisons, bibliothèques, etc.)
    public double GreenarySpawnRatio { get; set; } // entre 0 et 1, taux d'apparition de verdure (arbres, plantes, arbustres, etc.)
    
    // Couleurs


    // Assets
    public string[] DecorationAssets { get; set; } // décors (statues, fontaines, banc, 
    public string[] BuildingAssets { get; set; } // batiments (immeubles, maisons, tantes, igloo etc.)
    public string[] GreenaryAssets { get; set; } // végéteux (arbres, buisson, palmiers, virevoltant, etc.)
    public string[] MiscAssets { get; set; } 

    #endregion

    /// <summary>
    /// Attribution des variables pour un biome ville
    /// </summary>
    public class CityBiome
    {

    }

    /// <summary>
    /// Attribution des variables pour un biome village 
    /// </summary>
    public class VillageBiome
    {

    }

    /// <summary>
    /// Attribution des variables pour un biome désert 
    /// </summary>
    public class DesertBiome
    {

    }


}
