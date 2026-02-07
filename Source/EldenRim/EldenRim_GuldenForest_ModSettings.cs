using Verse;

namespace GuldenBiome;

public class EldenRim_GuldenForest_ModSettings : ModSettings
{
    private static EldenRim_GuldenForest_ModSettings _instance;

    public bool EldenRim_GuldenForest_Enable_CropEating = true;

    public float EldenRim_GuldenForest_Range_CropEating = 50f;

    public bool EldenRim_GuldenForest_LockAurelian = false;

    public static float minElevation = 500;

    public static float maxElevation = 800;

    public static float addToScore = 1.5f;

    public static bool Enable_CropEating => _instance.EldenRim_GuldenForest_Enable_CropEating;

    public static float Range_CropEating => _instance.EldenRim_GuldenForest_Range_CropEating;

    public static bool LockAurelian => _instance.EldenRim_GuldenForest_LockAurelian;

    public EldenRim_GuldenForest_ModSettings() {
        _instance = this;
    }

    public override void ExposeData() {
        base.ExposeData();
        Scribe_Values.Look(ref EldenRim_GuldenForest_Enable_CropEating, "EldenRim_GuldenForest_Enable_CropEating",
            defaultValue: true);
        Scribe_Values.Look(ref EldenRim_GuldenForest_Range_CropEating, "EldenRim_GuldenForest_Range_CropEating", 50f);
        Scribe_Values.Look(ref EldenRim_GuldenForest_LockAurelian, "EldenRim_GuldenForest_LockAurelian",
            defaultValue: false);
        Scribe_Values.Look(ref minElevation, "minElevation", 500f);
        Scribe_Values.Look(ref maxElevation, "maxElevation", 800f);
        Scribe_Values.Look(ref addToScore, "addToScore", 1.5f);
    }
}