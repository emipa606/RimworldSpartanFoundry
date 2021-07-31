using System.Collections.Generic;
using Verse;

namespace AchievementsExpanded
{
    // Token: 0x02000003 RID: 3
    public class ItemTrackerMultipleSpartanFoundry : ItemTracker
    {
        // Token: 0x04000006 RID: 6
        private Dictionary<ThingDef, bool> playerHasIt = new Dictionary<ThingDef, bool>();

        // Token: 0x04000005 RID: 5
        private Dictionary<ThingDef, int> thingList = new Dictionary<ThingDef, int>();

        // Token: 0x06000009 RID: 9 RVA: 0x00002394 File Offset: 0x00000594
        public ItemTrackerMultipleSpartanFoundry()
        {
        }

        // Token: 0x0600000A RID: 10 RVA: 0x000023B4 File Offset: 0x000005B4
        public ItemTrackerMultipleSpartanFoundry(ItemTrackerMultipleSpartanFoundry reference) : base(reference)
        {
            thingList = reference.thingList;
            playerHasIt = reference.playerHasIt;
            foreach (var keyValuePair in thingList)
            {
                playerHasIt.Add(keyValuePair.Key, false);
            }
        }

        // Token: 0x0600000B RID: 11 RVA: 0x00002450 File Offset: 0x00000650
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref thingList, "thingList", LookMode.Def, LookMode.Value);
            Scribe_Collections.Look(ref playerHasIt, "playerHasIt", LookMode.Def, LookMode.Value);
        }

        // Token: 0x0600000C RID: 12 RVA: 0x00002480 File Offset: 0x00000680
        public override bool Trigger()
        {
            var shouldTrigger = true;
            foreach (var keyValuePair in thingList)
            {
                playerHasIt[keyValuePair.Key] = UtilityMethods.PlayerHas(keyValuePair.Key, out _, keyValuePair.Value);
            }

            foreach (var keyValuePair2 in playerHasIt)
            {
                shouldTrigger = shouldTrigger && playerHasIt[keyValuePair2.Key];
            }

            return shouldTrigger;
        }
    }
}