using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace GuldenBiome;

internal class JobGiver_EatCrops : ThinkNode_JobGiver
{
    protected override Job TryGiveJob(Pawn pawn) {
        if (pawn.Downed) {
            return null;
        }

        float range_CropEating = EldenRim_GuldenForest_ModSettings.Range_CropEating;
        Thing thing = GenClosest.ClosestThingReachable(pawn.Position, pawn.Map,
            ThingRequest.ForGroup(ThingRequestGroup.Plant), PathEndMode.OnCell,
            TraverseParms.For(pawn), range_CropEating,
            (Predicate<Thing>)validator);
        if (thing == null) {
            return null;
        }

        return JobMaker.MakeJob(JobDefOf.Ingest, thing);

        bool validator(Thing t) {
            return t is Plant { sown: not false, IngestibleNow: not false } plant && pawn.RaceProps.CanEverEat(plant) &&
                   pawn.CanReserve(plant);
        }
    }
}