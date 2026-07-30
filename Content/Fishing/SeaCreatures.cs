using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Common.SubPlayer;
using TheDeep.Content.Items.Consumables.Bait;
using TheDeep.Content.Items.Fish;
using TheDeep.Content.Items.Fish.Quest;
using TheDeep.Content.Items.Material;
using TheDeep.Content.Items.Misc;
using TheDeep.Content.Items.Weapons;
using TheDeep.Content.Items.Weapons.Melee;

namespace TheDeep.Content
{
    public class SeaCreatures : ModPlayer // https://tenor.com/view/fish-meme-you-know-what-that-means-gif-12503956388971591256
    {

        public bool hasWaningCresBless;
        public bool hasWaxingCresBless;
        public bool hasWaxingGibBless;
        public bool hasWaningGibBless;
        public bool has14Bless;
        public bool has34Bless;

        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        {

            bool inWater = !attempt.inLava && !attempt.inHoney; // Make the fish only catchable in water
            bool inLava = attempt.inLava; // Make the fish only catchable in lava
            bool inHoney = attempt.inHoney; // Make the fish only catchable in honey (tbh I don't like it)

            bool inForest = Player.ZonePurity; // Make fish only catchable in surface
            bool inUnderground = Player.ZoneNormalUnderground; // Make fish only catchable in underground
            bool inCaverns = Player.ZoneNormalCaverns; // Make fish only catchable in caverns
            bool inBeach = Player.ZoneBeach; // Make fish only catchable in beach
            bool inTundra = Player.ZoneSnow; // Make fish only catchable in snow
            bool inDesert = Player.ZoneDesert; // Make fish only catchable in desert
            bool inJungle = Player.ZoneJungle; // Make fish only catchable in jungle
            bool inUndergroundDesert = Player.ZoneUndergroundDesert; // Make fish only catchable in UNDERGROUND desert :scream:
            bool inCrimson = Player.ZoneCrimson; // Make fish only catchable in crimson
            bool inCorrupt = Player.ZoneCorrupt; // Make fish only catchable in corrupt

            bool whenBloodMoon = Main.bloodMoon; // Make fish only catchable when a blood moon is happening
            bool whenRain = Main.raining; // Make fish only catchable when raining
            bool whenDay = Main.dayTime; // Make fish only catchable when in daytime
            bool whenNight = !Main.dayTime; // Make fish only catchable when in nighttime

            if (inWater && inForest && Main.rand.Next(100) < Player.GetModPlayer<SubmergedStats>().Hostility && attempt.rare)
            {
                npcSpawn = NPCID.BlueSlime;
            }

            if (hasWaxingGibBless && inWater && inForest && attempt.uncommon)
            {
                npcSpawn = NPCID.BlueSlime;
            }

            if (hasWaningGibBless && inWater && inForest && attempt.veryrare)
            {
                npcSpawn = NPCID.BlueSlime;
            }

            //

            if (inWater && inJungle && attempt.rare)
            {
                npcSpawn = NPCID.Piranha;
            }

            if (hasWaxingGibBless && inJungle && attempt.uncommon)
            {
                npcSpawn = NPCID.Piranha;
            }

            if (hasWaningGibBless && inJungle && attempt.veryrare)
            {
                npcSpawn = NPCID.Piranha;
            }

            //

            if (inWater && inTundra && attempt.rare)
            {
                npcSpawn = NPCID.Penguin;
            }

            if (hasWaxingGibBless && inWater && inTundra && attempt.uncommon)
            {
                npcSpawn = NPCID.Penguin;
            }

            if (hasWaningGibBless && inWater && inTundra && attempt.veryrare)
            {
                npcSpawn = NPCID.Penguin;
            }

            //

            if (inWater && inCrimson && attempt.rare)
            {
                npcSpawn = NPCID.BloodFeeder;
            }

            if (hasWaxingGibBless && inWater && inCrimson && attempt.uncommon)
            {
                npcSpawn = NPCID.BloodFeeder;
            }

            if (hasWaningGibBless && inWater && inCrimson && attempt.veryrare)
            {
                npcSpawn = NPCID.BloodFeeder;
            }

            //

            if (inWater && inCorrupt && attempt.rare)
            {
                npcSpawn = NPCID.CorruptSlime;
            }

            if (hasWaxingGibBless && inWater && inCorrupt && attempt.uncommon)
            {
                npcSpawn = NPCID.CorruptSlime;
            }

            if (hasWaningGibBless && inWater && inCorrupt && attempt.veryrare)
            {
                npcSpawn = NPCID.CorruptSlime;
            }

            //

            if (inWater && inBeach && attempt.rare)
            {
                npcSpawn = NPCID.Shark;
            }

            if (hasWaxingGibBless && inWater && inBeach && attempt.uncommon)
            {
                npcSpawn = NPCID.Shark;
            }

            if (hasWaningGibBless && inWater && inBeach && attempt.veryrare)
            {
                npcSpawn = NPCID.Shark;
            }

            //
        }
    }
    }