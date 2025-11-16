using Verse;

namespace GuldenBiome;

internal class EldenRim_GuldenForest_ModSettings : ModSettings
{
    private static EldenRim_GuldenForest_ModSettings _instance;

    public bool EldenRim_GuldenForest_Enable_CropEating = true;

    public float EldenRim_GuldenForest_Range_CropEating = 50f;

    public static bool Enable_CropEating => _instance.EldenRim_GuldenForest_Enable_CropEating;

    public static float Range_CropEating => _instance.EldenRim_GuldenForest_Range_CropEating;

    public EldenRim_GuldenForest_ModSettings() {
        _instance = this;
    }

    public override void ExposeData() {
        base.ExposeData();
        Scribe_Values.Look(ref EldenRim_GuldenForest_Enable_CropEating, "EldenRim_GuldenForest_Enable_CropEating",
            defaultValue: true);
        Scribe_Values.Look(ref EldenRim_GuldenForest_Range_CropEating, "EldenRim_GuldenForest_Range_CropEating", 50f);
    }
}