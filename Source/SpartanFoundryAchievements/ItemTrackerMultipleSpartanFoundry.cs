using System.Collections.Generic;
using Verse;

namespace AchievementsExpanded;

public class ItemTrackerMultipleSpartanFoundry : ItemTracker
{
    private Dictionary<ThingDef, bool> playerHasIt = new Dictionary<ThingDef, bool>();

    private Dictionary<ThingDef, int> thingList = new Dictionary<ThingDef, int>();

    public ItemTrackerMultipleSpartanFoundry()
    {
    }

    public ItemTrackerMultipleSpartanFoundry(ItemTrackerMultipleSpartanFoundry reference) : base(reference)
    {
        thingList = reference.thingList;
        playerHasIt = reference.playerHasIt;
        foreach (var keyValuePair in thingList)
        {
            playerHasIt.Add(keyValuePair.Key, false);
        }
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Collections.Look(ref thingList, "thingList", LookMode.Def, LookMode.Value);
        Scribe_Collections.Look(ref playerHasIt, "playerHasIt", LookMode.Def, LookMode.Value);
    }

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