using System.Collections.Generic;
using Verse;

namespace AchievementsExpanded;

public class SpartanFoundry_QuestManager_Notify_ThingsProduced_Patch
{
    public static void CheckItemCraftedMultiple(Pawn worker, List<Thing> things)
    {
        if (things == null || worker == null)
        {
            return;
        }

        foreach (var achievementCard in
                 AchievementPointManager.GetCards<ItemCraftTrackerMultipleSpartanFoundry>())
        {
            foreach (var thing in things)
            {
                if (thing == null || achievementCard == null)
                {
                    continue;
                }

                if (((ItemCraftTrackerMultipleSpartanFoundry)achievementCard.tracker).Trigger(thing))
                {
                    achievementCard.UnlockCard();
                }
            }
        }
    }
}