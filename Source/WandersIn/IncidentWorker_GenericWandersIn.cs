using RimWorld;
using UnityEngine;
using Verse;

namespace GuldenBiome;

internal class IncidentWorker_GenericWandersIn : IncidentWorker
{
    protected override bool CanFireNowSub(IncidentParms parms) {
        IncidentProperties incidentProperties = IncidentProperties.Get(def);
        if (!base.CanFireNowSub(parms) && incidentProperties != null && incidentProperties.kindDef != null) {
            return false;
        }

        Map map = (Map)parms.target;
        return !map.gameConditionManager.ConditionIsActive(GameConditionDefOf.ToxicFallout) &&
               map.mapTemperature.SeasonAndOutdoorTemperatureAcceptableFor(incidentProperties.kindDef.race) &&
               TryFindEntryCell(map, out _);
    }

    protected override bool TryExecuteWorker(IncidentParms parms) {
        IncidentProperties incidentProperties = IncidentProperties.Get(def);
        Map map = (Map)parms.target;
        if (!TryFindEntryCell(map, out var cell)) {
            return false;
        }

        int value = GenMath.RoundRandom(StorytellerUtility.DefaultThreatPointsNow(map) /
                                        incidentProperties.kindDef.combatPower);
        int randomInRange = incidentProperties.max.RandomInRange;
        value = Mathf.Clamp(value, 1, randomInRange);
        int num = Rand.RangeInclusive(90000, 150000);
        IntVec3 result = IntVec3.Invalid;
        if (!RCellFinder.TryFindRandomCellOutsideColonyNearTheCenterOfTheMap(cell, map, 10f, out result)) {
            result = IntVec3.Invalid;
        }

        Pawn pawn = null;
        for (int i = 0; i < value; i++) {
            IntVec3 loc = CellFinder.RandomClosewalkCellNear(cell, map, 10);
            pawn = PawnGenerator.GeneratePawn(incidentProperties.kindDef, (Faction)null);
            GenSpawn.Spawn(pawn, loc, map, Rot4.Random);
            if (incidentProperties.leaveMapAfterTime) {
                pawn.mindState.exitMapAfterTick = Find.TickManager.TicksGame + num;
            }

            if (result.IsValid) {
                pawn.mindState.forcedGotoPosition = CellFinder.RandomClosewalkCellNear(result, map, 10);
            }
        }

        Find.LetterStack.ReceiveLetter(def.letterLabel.Formatted(incidentProperties.kindDef.label).CapitalizeFirst(),
            def.letterText.Formatted(value, incidentProperties.kindDef.label), def.letterDef, pawn);
        return true;
    }

    private bool TryFindEntryCell(Map map, out IntVec3 cell) {
        return RCellFinder.TryFindRandomPawnEntryCell(out cell, map, CellFinder.EdgeRoadChance_Animal + 0.2f);
    }
}