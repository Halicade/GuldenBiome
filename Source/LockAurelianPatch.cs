using System.Xml;
using Verse;

namespace GuldenBiome;

public class LockAurelianPatch : PatchOperationAdd
{
    //<biomeSpecific>true</biomeSpecific> on AurelianStone/building
    

    protected override bool ApplyWorker(XmlDocument xml) {
        
        if (EldenRim_GuldenForest_ModSettings.LockAurelian) {
            return base.ApplyWorker(xml);
        }
        return true;
    }
}