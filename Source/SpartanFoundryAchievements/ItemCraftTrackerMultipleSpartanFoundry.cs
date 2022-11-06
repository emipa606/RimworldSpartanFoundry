using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace AchievementsExpanded;

public class ItemCraftTrackerMultipleSpartanFoundry : Tracker<Thing>
{
    public ThingDef madeFrom;

    private Dictionary<ThingDef, int> playerCraftedIt = new Dictionary<ThingDef, int>();

    public QualityCategory? quality;

    private Dictionary<ThingDef, int> thingList = new Dictionary<ThingDef, int>();

    public ItemCraftTrackerMultipleSpartanFoundry()
    {
    }

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

    public override string Key => "ItemCraftTrackerMultipleSpartanFoundry";

    public override MethodInfo MethodHook => AccessTools.Method(typeof(QuestManager), "Notify_ThingsProduced");

    public override MethodInfo PatchMethod =>
        AccessTools.Method(typeof(SpartanFoundry_QuestManager_Notify_ThingsProduced_Patch),
            "CheckItemCraftedMultiple");

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

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Collections.Look(ref thingList, "thingList", LookMode.Def, LookMode.Value);
        Scribe_Collections.Look(ref playerCraftedIt, "playerHasIt", LookMode.Def, LookMode.Value);
        Scribe_Defs.Look(ref madeFrom, "madeFrom");
        Scribe_Values.Look(ref quality, "quality");
    }

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