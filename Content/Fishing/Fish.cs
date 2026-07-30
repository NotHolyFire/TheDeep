    using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Items.Consumables.Bait;
using TheDeep.Content.Items.Fish;
using TheDeep.Content.Items.Fish.Quest;
using TheDeep.Content.Items.Material;
using TheDeep.Content.Items.Misc;
using TheDeep.Content.Items.Weapons;
using TheDeep.Content.Items.Weapons.Melee;

namespace TheDeep.Content
{
    public class Fish : ModPlayer // https://tenor.com/view/fish-meme-you-know-what-that-means-gif-12503956388971591256
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
            /////////
            bool inWater = !attempt.inLava && !attempt.inHoney; // Make the fish only catchable in water
            bool inLava = attempt.inLava; // Make the fish only catchable in lava
            bool inHoney = attempt.inHoney; // Make the fish only catchable in honey (tbh I don't like it)
            /////////
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
            /////////
            bool whenBloodMoon = Main.bloodMoon; // Make fish only catchable when a blood moon is happening
            bool whenRain = Main.raining; // Make fish only catchable when raining
            bool whenDay = Main.dayTime; // Make fish only catchable when in daytime
            bool whenNight = !Main.dayTime; // Make fish only catchable when in nighttime
            /////////
            int SalvageableScrap = ModContent.ItemType<SalvageableScrap>();
            int SalvageablePlate = ModContent.ItemType<SalvageablePlate>();
            int Applefin = ModContent.ItemType<Applefin>();
            int Whetfish = ModContent.ItemType<Whetfish>();
            int Minnow = ModContent.ItemType<Minnow>();
            int DynamiteBass = ModContent.ItemType<DynamiteBass>();
            int Carp = ModContent.ItemType<Carp>();
            int Shrooma = ModContent.ItemType<Shrooma>();
            int Woodskip = ModContent.ItemType<Woodskip>();
            int Kelptopus = ModContent.ItemType<Kelptopus>();
            int Eel = ModContent.ItemType<Eel>();
            int Fisharaoh = ModContent.ItemType<Fisharaoh>();
            int Cactifin = ModContent.ItemType<Cactifin>();
            int Blackfish = ModContent.ItemType<Blackfish>();
            int Bladefish = ModContent.ItemType<Bladefish>();
            /////////
            int QuestOilFish = ModContent.ItemType<Oilfish>(); int OilFish = ModContent.ItemType<Oilfish>();
            int QuestDinofin = ModContent.ItemType<Dinofin>();
            int QuestBambooHattedFish = ModContent.ItemType<BambooHattedFish>();

            {
                if (inWater && inForest && attempt.common && !attempt.crate)

                    itemDrop = Applefin; // Fish up the Applefin

            }

            {
                if (inWater && inBeach && Main.rand.NextBool(1, 7) && !attempt.crate)

                    itemDrop = SalvageableScrap; // """""fish""""" up the scrap
            }

            {
                if (inWater && inBeach && Main.rand.NextBool(1, 10) && !attempt.crate)

                    itemDrop = SalvageablePlate; // """"""""""fish""""""""""" up the plate
            }

            {
                if (inWater && inForest && attempt.common && !attempt.crate)

                    itemDrop = Minnow; // Fish up the Minnow

            }

            {
                if (inWater && inForest && attempt.common && !attempt.crate)

                    itemDrop = Carp; // Fish up the Carp

            }

            {
                if (inWater && inForest && attempt.uncommon && !attempt.crate)

                    itemDrop = Shrooma; // Fish up the Shrooma

            }

            {
                if (inWater && inForest && attempt.uncommon && !attempt.crate)

                    itemDrop = Woodskip; // Fish up the Shrooma

            }

            {
                if (inWater && inUnderground && attempt.rare && !attempt.crate)

                    itemDrop = OilFish; // Fish up the Oil Fish

            }

            {
                if (inWater && inCaverns && attempt.uncommon && !attempt.crate)

                    itemDrop = OilFish; // Fish up the Oil Fish

            }

            {
                if (inWater && inUnderground && attempt.uncommon && !attempt.crate)

                    itemDrop = Whetfish; // Fish up the Whetfish

            }

            {
                if (inWater && inCaverns && attempt.common && !attempt.crate)

                    itemDrop = Whetfish; // Fish up the Whetfish

            }

            {
                if (inWater && inUnderground && attempt.uncommon && !attempt.crate)

                    itemDrop = DynamiteBass; // Fish up the Gunpowderfish

            }

            {
                if (inWater && inCaverns && attempt.uncommon && !attempt.crate)

                    itemDrop = DynamiteBass; // Fish up the Gunpowderfish

            }

            {
                if (inWater && inBeach && whenRain && attempt.uncommon && !attempt.crate)

                    itemDrop = Eel; // Fish up the Eel

            }

            {
                if (inWater && inTundra && whenRain && attempt.common && !attempt.crate)

                    itemDrop = Blackfish; // Fish up the Blackfish

            }

            {
                if (inWater && inDesert && attempt.common && !attempt.crate)

                    itemDrop = Cactifin; // Fish up the Cactifin (I can't take it anymore)

            }

            {
                if (inWater && inUndergroundDesert && attempt.uncommon && !attempt.crate)

                    itemDrop = Fisharaoh; // CURSE OF RAH

            }

            {
                if (inWater && inBeach && attempt.uncommon && !attempt.crate)

                    itemDrop = Kelptopus; // Fish up the kelptopus
            }

            {
                if (inWater && inBeach && attempt.rare && !attempt.crate)

                    itemDrop = Bladefish; // Fish up the slapfish
            }

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

                    itemDrop = QuestDinofin; // Fish up the Dinofin
                }

            }
        

        }

    }
    }