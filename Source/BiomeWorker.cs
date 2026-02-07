using RimWorld;
using RimWorld.Planet;

namespace GuldenBiome;

public class BiomeWorker : BiomeWorker_TemperateForest
{
    public override float GetScore(BiomeDef biome, Tile tile, PlanetTile planetTile) {
        // We only want tiles with an elevation between 500 and 800
        if (tile.elevation <= EldenRim_GuldenForest_ModSettings.minElevation || tile.elevation >= EldenRim_GuldenForest_ModSettings.maxElevation) {
            return -100;
        }
        // Get what the score is of the parent biome, temperate forest
        float score = base.GetScore(biome, tile, planetTile);
        
        if (score <= 0) {
            return -100;
        }

        // Only 1.5 because otherwise it would cover other biomes instead of just temperate forest
        return score + EldenRim_GuldenForest_ModSettings.addToScore;
    }
}