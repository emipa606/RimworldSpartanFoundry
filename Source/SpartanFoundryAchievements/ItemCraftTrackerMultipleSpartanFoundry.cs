using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace AchievementsExpanded
{
    // Token: 0x02000002 RID: 2
    public class ItemCraftTrackerMultipleSpartanFoundry : Tracker<Thing>
    {
        // Token: 0x04000003 RID: 3
        public ThingDef madeFrom;

        // Token: 0x04000002 RID: 2
        private Dictionary<ThingDef, int> playerCraftedIt = new Dictionary<ThingDef, int>();

        // Token: 0x04000004 RID: 4
        public QualityCategory? quality;

        // Token: 0x04000001 RID: 1
        private Dictionary<ThingDef, int> thingList = new Dictionary<ThingDef, int>();

        // Token: 0x06000005 RID: 5 RVA: 0x000020DB File Offset: 0x000002DB
        public ItemCraftTrackerMultipleSpartanFoundry()
        {
        }

        // Token: 0x06000006 RID: 6 RVA: 0x000020FC File Offset: 0x000002FC
        public ItemCraftTrackerMultipleSpartanFoundry(ItemCraftTrackerMultipleSpartanFoundry reference) :
            base(reference)
        {
            thingList = reference.thingList;
            madeFrom = reference.madeFrom;
            quality = reference.quality;
            playerCraftedIt = reference.playerCraftedIt;
            foreach (var keyValuePair in thingList)
            {
                playerCraftedIt.Add(keyValuePair.Key, 0);
            }
        }

        // Token: 0x17000001 RID: 1
        // (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        public override string Key => "ItemCraftTrackerMultipleSpartanFoundry";

        // Token: 0x17000002 RID: 2
        // (get) Token: 0x06000002 RID: 2 RVA: 0x00002057 File Offset: 0x00000257
        public override MethodInfo MethodHook => AccessTools.Method(typeof(QuestManager), "Notify_ThingsProduced");

        // Token: 0x17000003 RID: 3
        // (get) Token: 0x06000003 RID: 3 RVA: 0x0000206F File Offset: 0x0000026F
        public override MethodInfo PatchMethod =>
            AccessTools.Method(typeof(SpartanFoundry_QuestManager_Notify_ThingsProduced_Patch),
                "CheckItemCraftedMultiple");

        // Token: 0x17000004 RID: 4
        // (get) Token: 0x06000004 RID: 4 RVA: 0x00002088 File Offset: 0x00000288
        protected override string[] DebugText
        {
            get
            {
                var array = new string[2];
                var num = 0;
                var format = "MadeFrom: {0}";
                var thingDef = madeFrom;
                array[num] = string.Format(format, thingDef?.defName ?? "Any");
                array[1] = $"Quality: {quality}";
                return array;
            }
        }

        // Token: 0x06000007 RID: 7 RVA: 0x000021B0 File Offset: 0x000003B0
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref thingList, "thingList", LookMode.Def, LookMode.Value);
            Scribe_Collections.Look(ref playerCraftedIt, "playerHasIt", LookMode.Def, LookMode.Value);
            Scribe_Defs.Look(ref madeFrom, "madeFrom");
            Scribe_Values.Look(ref quality, "quality");
        }

        // Token: 0x06000008 RID: 8 RVA: 0x00002218 File Offset: 0x00000418
        public override bool Trigger(Thing thing)
        {
            base.Trigger(thing);
            var shouldTrigger = true;
            foreach (var keyValuePair in thingList)
            {
                if (keyValuePair.Key != null && thing.def != keyValuePair.Key ||
                    madeFrom != null && madeFrom != thing.Stuff)
                {
                    continue;
                }

                if (quality != null && (!thing.TryGetQuality(out var qualityCategory) || !(qualityCategory >= quality)))
                {
                    continue;
                }

                var dictionary = playerCraftedIt;
                var key = keyValuePair.Key;
                var num = dictionary[key!];
                dictionary[key] = num + 1;
            }

            foreach (var keyValuePair2 in playerCraftedIt)
            {
                shouldTrigger = shouldTrigger && playerCraftedIt[keyValuePair2.Key] >= thingList[keyValuePair2.Key];
            }

            return shouldTrigger;
        }
    }
}