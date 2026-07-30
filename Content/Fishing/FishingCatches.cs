    using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Common.SubPlayer;
using TheDeep.Common.Systems;
using TheDeep.Content.Items.Consumables.Bait;
using TheDeep.Content.Items.Consumables.Bait.Special;
using TheDeep.Content.Items.Fish;
using TheDeep.Content.Items.Fish.Quest;
using TheDeep.Content.Items.Material;
using TheDeep.Content.Items.Misc;
using TheDeep.Content.Items.Weapons;
using TheDeep.Content.Items.Weapons.Melee;

namespace TheDeep.Content
{
    public class FishingCatches : ModPlayer // https://tenor.com/view/fish-meme-you-know-what-that-means-gif-12503956388971591256
    {
        public bool hasWaningCresBless;
        public bool hasWaxingCresBless;
        public bool hasWaxingGibBless;
        public bool hasWaningGibBless;
        public bool has14Bless;
        public bool has34Bless;



        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        {
            base.CatchFish(attempt, ref itemDrop, ref npcSpawn, ref sonar, ref sonarPosition);

            #region Fishing Conditions
            ///////// Liquid (NO, THAT IS NOT SOLID SNAKE)
            bool inWater = !attempt.inLava && !attempt.inHoney; // Make the fish only catchable in water
            bool inLava = attempt.inLava; // Make the fish only catchable in lava
            bool inHoney = attempt.inHoney; // Make the fish only catchable in honey
            ///////// Biome
            bool inForest = Player.ZoneForest; // Make fish only catchable in forest
            bool inUnderground = Player.ZoneNormalUnderground; // Make fish only catchable in underground
            bool inCaverns = Player.ZoneNormalCaverns; // Make fish only catchable in caverns
            bool inBeach = Player.ZoneBeach; // Make fish only catchable in beach
            bool inTundra = Player.ZoneSnow; // Make fish only catchable in snow
            bool inDesert = Player.ZoneDesert; // Make fish only catchable in desert
            bool inJungle = Player.ZoneJungle; // Make fish only catchable in jungle
            bool inUndergroundDesert = Player.ZoneUndergroundDesert; // Make fish only catchable in underground desert
            bool inCrimson = Player.ZoneCrimson; // Make fish only catchable in crimson
            bool inCorrupt = Player.ZoneCorrupt; // Make fish only catchable in corrupt
            bool inAether = Player.ZoneShimmer; // Make fish only catchable in aether (Might try fishing in shimmer later)
            ///////// Event
            bool whenBloodMoon = Main.bloodMoon; // Make fish only catchable when a blood moon is happening
            bool whenSuperMoon = Supermoon.SuperMoon; // Make fish only catchable when a super moon is happening
            bool whenRain = Main.raining; // Make fish only catchable when raining
            bool whenDay = Main.dayTime; // Make fish only catchable when in daytime
            bool whenNight = !Main.dayTime; // Make fish only catchable when in nighttime
            ///////// Fish Catches
            int Applefin = ModContent.ItemType<Applefin>();
            int Whetfish = ModContent.ItemType<Whetfish>();
            int Minnow = ModContent.ItemType<Minnow>();
            int DynamiteBass = ModContent.ItemType<DynamiteBass>();
            int Carp = ModContent.ItemType<Carp>();
            int Shrooma = ModContent.ItemType<Shrooma>();
            int Woodskip = ModContent.ItemType<Woodskip>();
            int Kelptopus = ModContent.ItemType<Kelptopus>();
            int Eel = ModContent.ItemType<Eel>();
            int Cactifin = ModContent.ItemType<Cactifin>();
            int Blackfish = ModContent.ItemType<Blackfish>();
            int Bladefish = ModContent.ItemType<Bladefish>();
            ///////// Treasure/Crate Catches
            int SalvageableScrap = ModContent.ItemType<SalvageableScrap>();
            int SalvageablePlate = ModContent.ItemType<SalvageablePlate>();
            ///////// Sea Creature Catches
            int CatchPiranha = NPCID.Piranha;
            int CatchPenguin = NPCID.Penguin;
            int CatchSoulEater = NPCID.EaterofSouls;
            int CatchCrimera = NPCID.Crimera;
            int CatchShark = NPCID.Shark;
            int CatchBlueSlime = NPCID.BlueSlime;
            int CatchSandSlime = NPCID.SandSlime;
            int CatchCaveBat = NPCID.CaveBat;
            int CatchGiantWorm = NPCID.GiantWormHead;
            ///////// Quest Fish
            int QuestOilFish = ModContent.ItemType<Oilfish>();
            int QuestDinofin = ModContent.ItemType<Dinofin>();
            int QuestFisharaoh = ModContent.ItemType<Fisharaoh>();
            int QuestBambooHattedFish = ModContent.ItemType<BambooHattedFish>();
            ///////// Baits
            int MagnetBait = ModContent.ItemType<MagnetBait>();
            int ApplefinBait = ModContent.ItemType<ApplefinBait>();
            int KelptopusBait = ModContent.ItemType<KelptopusBait>();
            int OilfishBait = ModContent.ItemType<OilfishBait>();
            int WhetfishBait = ModContent.ItemType<WhetfishBait>();
            #endregion


            #region Normal Catches
            {
                if (inWater && inForest && attempt.common && !attempt.crate)

                    itemDrop = Applefin; // Fish up Applefin

            }

            {
                if (inWater && inForest && attempt.common && !attempt.crate)

                    itemDrop = Minnow; // Fish up Minnow

            }

            {
                if (inWater && inForest && attempt.common && !attempt.crate)

                    itemDrop = Carp; // Fish up Carp

            }

            {
                if (inWater && inForest && attempt.uncommon && !attempt.crate)

                    itemDrop = Shrooma; // Fish up Shrooma

            }

            {
                if (inWater && inForest && attempt.uncommon && !attempt.crate)

                    itemDrop = Woodskip; // Fish up Woodskip

            }

            {
                if (inWater && inUnderground && attempt.uncommon && !attempt.crate)

                    itemDrop = Whetfish; // Fish up the Whetfish (Uncommon)

            }

            {
                if (inWater && inCaverns && attempt.common && !attempt.crate)

                    itemDrop = Whetfish; // Fish up Whetfish (Common)

            }

            {
                if (inWater && inUnderground && attempt.uncommon && !attempt.crate)

                    itemDrop = DynamiteBass; // Fish up Dynamite Bass

            }

            {
                if (inWater && inCaverns && attempt.uncommon && !attempt.crate)

                    itemDrop = DynamiteBass; // Fish up Dynamite Bass

            }

            {
                if (inWater && inBeach && whenRain && attempt.uncommon && !attempt.crate)

                    itemDrop = Eel; // Fish up Eel

            }

            {
                if (inWater && inTundra && whenRain && attempt.common && !attempt.crate)

                    itemDrop = Blackfish; // Fish up Blackfish

            }

            {
                if (inWater && inDesert && attempt.common && !attempt.crate)

                    itemDrop = Cactifin; // Fish up Cactifin

            }

            {
                if (inWater && inBeach && attempt.uncommon && !attempt.crate)

                    itemDrop = Kelptopus; // Fish up Kelptopus
            }

            {
                if (inWater && inBeach && attempt.rare && !attempt.crate)

                    itemDrop = Bladefish; // Fish up Bladefish
            }
            #endregion
            #region Treasure Catches
            {
                if (inWater && inBeach && attempt.common && attempt.crate)

                    itemDrop = SalvageableScrap; // fish up salvageable scrap
            }

            {
                if (inWater && inBeach && attempt.uncommon && attempt.crate)

                    itemDrop = SalvageablePlate; // fish up salvageable plate
            }
            #endregion
            #region Sea Creature Catches
            if (inWater && inForest && Player.GetModPlayer<SubmergedStats>().CreatureCatch == true)
            {
                npcSpawn = CatchBlueSlime;
            }

            if (inWater && inDesert && Player.GetModPlayer<SubmergedStats>().CreatureCatch == true)
            {
                npcSpawn = CatchSandSlime;
            }

            if (inWater && inUnderground && Player.GetModPlayer<SubmergedStats>().CreatureCatch == true)
            {
                npcSpawn = CatchGiantWorm;
            }

            if (inWater && inCaverns && Player.GetModPlayer<SubmergedStats>().CreatureCatch == true)
            {
                npcSpawn = CatchCaveBat;
            }

            if (inWater && inJungle && Player.GetModPlayer<SubmergedStats>().CreatureCatch == true)
            {
                npcSpawn = CatchPiranha;
            }

            if (inWater && inTundra && Player.GetModPlayer<SubmergedStats>().CreatureCatch == true)
            {
                npcSpawn = CatchPenguin;
            }

            if (inWater && inCrimson && Player.GetModPlayer<SubmergedStats>().CreatureCatch == true)
            {   
                npcSpawn = CatchCrimera;
            }

            if (inWater && inCorrupt && Player.GetModPlayer<SubmergedStats>().CreatureCatch == true)
            {
                npcSpawn = CatchSoulEater;
            }

            if (inWater && inBeach && Player.GetModPlayer<SubmergedStats>().CreatureCatch == true)
            {
                npcSpawn = CatchShark;
            }
            #endregion
            #region Bait Changes

            {
                if (inWater && inBeach && Player.GetFishingConditions().BaitItemType == MagnetBait && Main.rand.NextBool(1, 3))

                    itemDrop = SalvageableScrap;
            }

            {
                if (inWater && inBeach && Player.GetFishingConditions().BaitItemType == MagnetBait && Main.rand.NextBool(1, 5))

                    itemDrop = SalvageablePlate; // """"""""""fish""""""""""" up the plate (with more chance)
            }

            {
                if (inWater && inForest && Player.GetFishingConditions().BaitItemType == ApplefinBait && Main.rand.NextBool(1, 2))

                    itemDrop = Applefin; // Fish up the Applefin with more chance

            }

            {
                if (inWater && inBeach && Player.GetFishingConditions().BaitItemType == KelptopusBait && Main.rand.NextBool(1, 2))

                    itemDrop = Kelptopus; // Fish up the Kelptopus with more chance

            }

            {
                if (inWater && inCaverns && Player.GetFishingConditions().BaitItemType == WhetfishBait && Main.rand.NextBool(1, 2))

                    itemDrop = Whetfish; // Fish up the Whetfish with more chance

            }

            {
                if (inWater && inUnderground && Player.GetFishingConditions().BaitItemType == WhetfishBait && Main.rand.NextBool(1, 2))

                    itemDrop = Whetfish; // Fish up the Whetfish with more chance

            }
            #endregion
            #region Quest Fish
            if (attempt.questFish == QuestBambooHattedFish)
            {
                // also make if their current hp is lower than their max hp, the feesh is catchable
                // quest fish use attempt uncommon, so.. we use that
                if (inJungle && Player.statLife <= Player.statLifeMax2 * 0.5f && attempt.uncommon)
                {
                    itemDrop = QuestBambooHattedFish; // yield my bait to claim their fish
                    return;
                }
            }

            if (attempt.questFish == QuestDinofin)
            {
                if (inLava && inCaverns && attempt.uncommon)
                {

                    itemDrop = QuestDinofin; // Fish up Dinofin
                }

                if (inWater && inUndergroundDesert && attempt.uncommon && !attempt.crate)
                {
                    itemDrop = QuestFisharaoh; // Fish up Fisharaoh
                }

            }
            #endregion


        }

    }
    }