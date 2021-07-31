using System.Collections.Generic;
using Verse;

namespace AchievementsExpanded
{
    // Token: 0x02000004 RID: 4
    public class SpartanFoundry_QuestManager_Notify_ThingsProduced_Patch
    {
        // Token: 0x0600000D RID: 13 RVA: 0x0000255C File Offset: 0x0000075C
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

                    if (((ItemCraftTrackerMultipleSpartanFoundry) achievementCard.tracker).Trigger(thing))
                    {
                        achievementCard.UnlockCard();
                    }
                }
            }
        }
    }
}