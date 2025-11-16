using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GuldenBiome;

internal class IncidentWorker_GuldenAmbrosiaSprout : IncidentWorker
{
    private static readonly IntRange CountRange = new IntRange(10, 20);

    private const int MinRoomCells = 64;

    private const int SpawnRadius = 6;

    protected override bool CanFireNowSub(IncidentParms parms) {
        if (!base.CanFireNowSub(parms)) {
            return false;
        }

        Map map = (Map)parms.target;
        return PlantUtility.GrowthSeasonNow(map, ThingDefOf.Plant_GuldenAmbrosia) && TryFindRootCell(map, out _);
    }

    protected override bool TryExecuteWorker(IncidentParms parms) {
        Map map = (Map)parms.target;
        if (!TryFindRootCell(map, out var cell)) {
            return false;
        }

        Thing thing = null;
        int randomInRange = CountRange.RandomInRange;
        for (int i = 0; i < randomInRange; i++) {
            IntVec3 root = cell;
            Map map2 = map;
            int radius = 6;
            if (!CellFinder.TryRandomClosewalkCellNear(root, map2, radius, out var result,
                    (IntVec3 x) => CanSpawnAt(x, map))) {
                break;
            }

            result.GetPlant(map)?.Destroy();
            Thing thing2 = GenSpawn.Spawn(ThingDefOf.Plant_GuldenAmbrosia, result, map);
            if (thing == null) {
                thing = thing2;
            }
        }

        if (thing == null) {
            return false;
        }

        SendStandardLetter(parms, thing);
        return true;
    }

    private bool TryFindRootCell(Map map, out IntVec3 cell) {
        return CellFinderLoose.TryFindRandomNotEdgeCellWith(10,
            (IntVec3 x) => CanSpawnAt(x, map) && x.GetRoom(map).CellCount >= 64, map, out cell);
    }

    private bool CanSpawnAt(IntVec3 c, Map map) {
        if (!c.Standable(map) || c.Fogged(map) ||
            map.fertilityGrid.FertilityAt(c) < ThingDefOf.Plant_GuldenAmbrosia.plant.fertilityMin ||
            !c.GetRoom(map).PsychologicallyOutdoors || c.GetEdifice(map) != null ||
            !PlantUtility.GrowthSeasonNow(c, map, ThingDefOf.Plant_GuldenAmbrosia)) {
            return false;
        }

        Plant plant = c.GetPlant(map);
        if (plant != null && plant.def.plant.growDays > 10f) {
            return false;
        }

        List<Thing> thingList = c.GetThingList(map);
        for (int i = 0; i < thingList.Count; i++) {
            if (thingList[i].def == ThingDefOf.Plant_GuldenAmbrosia) {
                return false;
            }
        }

        return true;
    }
}