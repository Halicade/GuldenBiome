using System;
using UnityEngine;
using Verse;

namespace GuldenBiome;

internal class EldenRim_GuldenForest_Mod : Mod
{
    private EldenRim_GuldenForest_ModSettings settings;

    public EldenRim_GuldenForest_Mod(ModContentPack contentPack)
        : base(contentPack) {
        settings = GetSettings<EldenRim_GuldenForest_ModSettings>();
    }

    public override string SettingsCategory() {
        return "EldenRim_GuldenForest_ModName".Translate();
    }

    public override void DoSettingsWindowContents(Rect inRect) {
        Listing_Standard listing_Standard = new Listing_Standard();
        listing_Standard.Begin(inRect);
        listing_Standard.Gap();
        listing_Standard.CheckboxLabeled("EldenRim_GuldenForest_Enable_CropEating".Translate(),
            ref settings.EldenRim_GuldenForest_Enable_CropEating);
        listing_Standard.Gap();
        listing_Standard.Label(
            string.Concat("EldenRim_GuldenForest_Range_CropEating".Translate() + " (",
                settings.EldenRim_GuldenForest_Range_CropEating.ToString(), ")"), -1f);
        settings.EldenRim_GuldenForest_Range_CropEating =
            (int)Math.Round(listing_Standard.Slider(settings.EldenRim_GuldenForest_Range_CropEating, 1f, 100f));
        listing_Standard.Gap();
        listing_Standard.GapLine();
        listing_Standard.CheckboxLabeled("EldenRim_GuldenForest_LockAurelianStone".Translate(),
            ref settings.EldenRim_GuldenForest_LockAurelian,
            tooltip: "EldenRim_GuldenForest_LockAurelianStoneTip".Translate());
        listing_Standard.Gap();
        listing_Standard.GapLine();


        EldenRim_GuldenForest_ModSettings.minElevation = listing_Standard.SliderLabeled(
            "EldenRim_GuldenForest_MinElevation".Translate(EldenRim_GuldenForest_ModSettings.minElevation),
            EldenRim_GuldenForest_ModSettings.minElevation, 0,
            1500);

        if (EldenRim_GuldenForest_ModSettings.minElevation >= EldenRim_GuldenForest_ModSettings.maxElevation) {
            EldenRim_GuldenForest_ModSettings.minElevation = EldenRim_GuldenForest_ModSettings.maxElevation - 1;
        }

        EldenRim_GuldenForest_ModSettings.maxElevation = listing_Standard.SliderLabeled(
            "EldenRim_GuldenForest_MaxElevation".Translate(EldenRim_GuldenForest_ModSettings.maxElevation),
            EldenRim_GuldenForest_ModSettings.maxElevation, 1,
            1501);
        if (EldenRim_GuldenForest_ModSettings.maxElevation <= EldenRim_GuldenForest_ModSettings.minElevation) {
            EldenRim_GuldenForest_ModSettings.maxElevation = EldenRim_GuldenForest_ModSettings.minElevation + 1;
        }

        EldenRim_GuldenForest_ModSettings.addToScore = listing_Standard.SliderLabeled(
            "EldenRim_GuldenForest_AddToScore".Translate(
                EldenRim_GuldenForest_ModSettings.addToScore.ToString(format: "0.00")),
            EldenRim_GuldenForest_ModSettings.addToScore, 0.1f,
            5f, tooltip: "EldenRim_GuldenForest_AddToScoreTip".Translate());

        if (listing_Standard.ButtonText("EldenRim_GuldenForest_Reset".Translate(), widthPct: 0.3f)) {
            settings.EldenRim_GuldenForest_Enable_CropEating = true;
            settings.EldenRim_GuldenForest_Range_CropEating = 50f;
            settings.EldenRim_GuldenForest_LockAurelian = false;
            
            EldenRim_GuldenForest_ModSettings.minElevation = 500f;
            EldenRim_GuldenForest_ModSettings.maxElevation = 800f;
            EldenRim_GuldenForest_ModSettings.addToScore = 1.5f;
        }
        
        listing_Standard.End();
        base.DoSettingsWindowContents(inRect);
    }
}