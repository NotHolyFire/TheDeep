using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Graphics.Effects;
using Terraria.Utilities;
using Terraria.ModLoader.IO;
using System.Collections.Generic;

namespace TheDeep.Common.Systems
{
    // Acts as a container for "downed boss" flags.
    // Set a flag like this in your bosses OnKill hook:
    //    NPC.SetEventFlagCleared(ref DownedBossSystem.downedMinionBoss, -1);

    // Saving and loading these flags requires TagCompounds, a guide exists on the wiki: https://github.com/tModLoader/tModLoader/wiki/Saving-and-loading-using-TagCompound
    public class Supermoon : ModSystem
    {

        private static bool dayTimeLast;
        public static bool dayTimeSwitched;

        public static bool SuperMoon = false;
        public static bool downedSuperMoon = false;

        public override void PostUpdateWorld()
        {
            if (Main.dayTime != dayTimeLast)
                dayTimeSwitched = true;
            else
                dayTimeSwitched = false;

            dayTimeLast = Main.dayTime;

            if (SuperMoon && dayTimeSwitched && !downedSuperMoon)
                downedSuperMoon = true;

            if (dayTimeSwitched)
                OnDaySwitch();
        }

        public override void SaveWorldData(TagCompound tag)
        {
            var downed = new List<string>();

            if (downedSuperMoon)
                downed.Add("supermoon");

        }

        public override void LoadWorldData(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");

            downedSuperMoon = downed.Contains("supermoon");
        }

            private static void OnDaySwitch()
        {

            if (!Main.dayTime && Main.moonPhase == 0)
            {
                if (!Main.bloodMoon && ((Main.rand.NextBool(2) && !downedSuperMoon) || (Main.rand.NextBool(2) && downedSuperMoon)))
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                        Main.NewText(Language.GetTextValue("Mods.TheDeep.Dialogue.TheMoon.SuperMoonStart"), new Color(61, 255, 142));
                    else if (Main.netMode == NetmodeID.Server)
                        ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.TheDeep.TheMoon.SuperMoonStart"), new Color(61, 255, 142));

                    SuperMoon = true;
                    downedSuperMoon = true;
                }
            }
            else
                SuperMoon = false;
        }
    }

	internal class SuperMoonScene : ModSceneEffect
    {
        
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Content/Assets/Music/Shoneyy_GraceOfTheMoon");
        public override SceneEffectPriority Priority => SceneEffectPriority.Event;
        public override bool IsSceneEffectActive(Player player) => Supermoon.SuperMoon;

        


    }


}