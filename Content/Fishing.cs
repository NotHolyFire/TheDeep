using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Items.Fish;
using TheDeep.Content.Items.Material;
using TheDeep.Content.Items.Misc;
using TheDeep.Content.Items.Weapons;

namespace TheDeep.Content
{
    public class Fishing : ModPlayer // https://tenor.com/view/fish-meme-you-know-what-that-means-gif-12503956388971591256
    {

        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        {
            base.CatchFish(attempt, ref itemDrop, ref npcSpawn, ref sonar, ref sonarPosition);

            bool inWater = !attempt.inLava && !attempt.inHoney; // Make the fish only catchable in water
            bool inForest = Player.ZoneForest; // Make fish only catchable in surface
            bool inUnderground = Player.ZoneNormalUnderground; // Make fish only catchable in underground
            bool inCaverns = Player.ZoneNormalCaverns; // Make fish only catchable in caverns
            bool inBeach = Player.ZoneBeach; // Make fish only catchable in beach
            bool inJungle = Player.ZoneJungle; // Make fish only catchable in jungle

            bool whenBloodMoon = Main.bloodMoon; // Make fish only catchable when a blood moon is happening
            bool whenRain = Player.ZoneRain; // Make fish only catchable when raining

            int SalvageableScrap = ModContent.ItemType<SalvageableScrap>();
            int SalvageablePlate = ModContent.ItemType<SalvageablePlate>();
            int Applefin = ModContent.ItemType<Applefin>();
            int Whetfish = ModContent.ItemType<Whetfish>();
            int Minnow = ModContent.ItemType<Minnow>();
            int DynamiteBass = ModContent.ItemType<DynamiteBass>();
            int Carp = ModContent.ItemType<Carp>();
            int Shrooma = ModContent.ItemType<Shrooma>();
            int OilFish = ModContent.ItemType<OilFish>();
            int Woodskip = ModContent.ItemType<Woodskip>();
            int Kelptopus = ModContent.ItemType<Kelptopus>();
            int Bladefish = ModContent.ItemType<Bladefish>();
            int QuestBambooHattedFish = ModContent.ItemType<BambooHattedFish>();

            {
                if (inWater && inForest && Main.rand.NextBool(1, 12))

                    itemDrop = Applefin; // Fish up the Applefin

            }

            {
                if (inWater && inBeach && Main.rand.NextBool(1, 7))

                    itemDrop = SalvageableScrap; // """""fish""""" up the scrap
            }

            {
                if (inWater && inBeach && Main.rand.NextBool(1, 10))

                    itemDrop = SalvageablePlate; // """"""""""fish""""""""""" up the plate
            }

            {
                if (inWater && inForest && Main.rand.NextBool(1, 12))

                    itemDrop = Minnow; // Fish up the Minnow

            }

            {
                if (inWater && inForest && Main.rand.NextBool(1, 14))

                    itemDrop = Carp; // Fish up the Carp

            }

            {
                if (inWater && inForest && Main.rand.NextBool(1, 11))

                    itemDrop = Shrooma; // Fish up the Shrooma

            }

            {
                if (inWater && inForest && Main.rand.NextBool(1, 12))

                    itemDrop = Woodskip; // Fish up the Shrooma

            }

            {
                if (inWater && inUnderground && Main.rand.NextBool(1, 19))

                    itemDrop = OilFish; // Fish up the Oil Fish

            }

            {
                if (inWater && inCaverns && Main.rand.NextBool(1, 17))

                    itemDrop = OilFish; // Fish up the Oil Fish

            }

            {
                if (inWater && inUnderground && Main.rand.NextBool(1, 20))

                    itemDrop = Whetfish; // Fish up the Whetfish

            }

            {
                if (inWater && inCaverns && Main.rand.NextBool(1, 11))

                    itemDrop = Whetfish; // Fish up the Whetfish

            }

            {
                if (inWater && inUnderground && Main.rand.NextBool(1, 12))

                    itemDrop = DynamiteBass; // Fish up the Gunpowderfish

            }

            {
                if (inWater && inCaverns && Main.rand.NextBool(1, 19))

                    itemDrop = DynamiteBass; // Fish up the Gunpowderfish

            }

            {
                if (inWater && inBeach && Main.rand.NextBool(1, 20))

                    itemDrop = Kelptopus; // Fish up the kelptopus
            }

            if (attempt.questFish == QuestBambooHattedFish)
            {
                // also make if their current hp is lower than their max hp, the feesh is catchable
                // quest fish use attempt uncommon, so.. we use that
                if (Player.statLife <= Player.statLifeMax2 * 0.5f && attempt.uncommon)
                {
                    itemDrop = QuestBambooHattedFish; // yield my bait to claim their fish
                    return;
                }

            }
        }
        int SalvageableScrap = ModContent.ItemType<SalvageableScrap>(); // don't ask me.
        int SalvageablePlate = ModContent.ItemType<SalvageablePlate>();
        // If fishing with magnet, we will receive multiple """"fish"""" per bobber.
        public override void ModifyCaughtFish(Item fish)

        {
            int MagnetBait = ModContent.ItemType<Content.Items.Misc.MagnetBait>(); // a bait that is magnetic uh huh
            // Needs to have a Magnet as bait
            if (Player.GetFishingConditions().BaitItemType == MagnetBait && fish.type == SalvageableScrap)
            {
                fish.stack += Main.rand.Next(2, 4);
            }

            if (Player.GetFishingConditions().BaitItemType == MagnetBait && fish.type == SalvageablePlate)
            {
                fish.stack += Main.rand.Next(2, 3);
            }
        }

    }
    }