using RimWorld;
using RimWorld.Planet;

namespace GuldenBiome;

public class BiomeWorker : BiomeWorker_TemperateForest
{
    public override float GetScore(BiomeDef biome, Tile tile, PlanetTile planetTile) {
        if (tile.elevation is <= 500 or >= 800) {
            return -100;
        }

        var score = base.GetScore(biome, tile, planetTile);

        if (score <= 0) {
            return -100;
        }

        // Only 1 because otherwise it would cover other biomes instead of just temperate forest
        return score + 1.5f;
    }
}