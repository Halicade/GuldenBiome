using RimWorld;
using Verse;

namespace GuldenBiome;

internal class ThinkNode_ConditionalCropEating : ThinkNode_ConditionalPawnKind
{
    protected override bool Satisfied(Pawn pawn) {
        return base.Satisfied(pawn) && pawn.Faction == null && EldenRim_GuldenForest_ModSettings.Enable_CropEating &&
               pawn.needs.food.CurInstantLevel <= pawn.needs.food.PercentageThreshHungry;
    }
}