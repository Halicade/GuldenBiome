using RimWorld;
using Verse;

namespace GuldenBiome;

[DefOf]
public static class ThingDefOf
{
    public static ThingDef Plant_GuldenAmbrosia;

    static ThingDefOf() {
        DefOfHelper.EnsureInitializedInCtor(typeof(ThingDefOf));
    }
}