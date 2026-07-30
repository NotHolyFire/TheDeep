using Humanizer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
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


    public class Bait : ModPlayer // https://tenor.com/view/fish-meme-you-know-what-that-means-gif-12503956388971591256
    {
        public bool hasWaningCresBless;
        public bool hasWaxingCresBless;
        public bool hasWaxingGibBless;
        public bool hasWaningGibBless;
        public bool has14Bless;
        public bool has34Bless;
        public bool isMagnetEquipped; // this is so scuffed, I'm gonna be impressed if it actually works ngl 

        public override void ResetEffects()
        {
            isMagnetEquipped = false;
        }

        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        {
            base.CatchFish(attempt, ref itemDrop, ref npcSpawn, ref sonar, ref sonarPosition);

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

            int SalvageableScrap = ModContent.ItemType<SalvageableScrap>(); //
            int SalvageablePlate = ModContent.ItemType<SalvageablePlate>(); //
            int Applefin = ModContent.ItemType<Applefin>(); //
            int Whetfish = ModContent.ItemType<Whetfish>(); //
            int Minnow = ModContent.ItemType<Minnow>(); //
            int DynamiteBass = ModContent.ItemType<DynamiteBass>(); //
            int Carp = ModContent.ItemType<Carp>(); //
            int Shrooma = ModContent.ItemType<Shrooma>(); //
            int OilFish = ModContent.ItemType<Oilfish>(); // 
            int Woodskip = ModContent.ItemType<Woodskip>(); //
            int Kelptopus = ModContent.ItemType<Kelptopus>(); //
            int Eel = ModContent.ItemType<Eel>(); //
            int Fisharaoh = ModContent.ItemType<Fisharaoh>(); //
            int Cactifin = ModContent.ItemType<Cactifin>(); //
            int Blackfish = ModContent.ItemType<Blackfish>(); //
            int Bladefish = ModContent.ItemType<Bladefish>(); //

            int MagnetBait = ModContent.ItemType<MagnetBait>(); //
            int ApplefinBait = ModContent.ItemType<ApplefinBait>(); //
            int KelptopusBait = ModContent.ItemType<KelptopusBait>(); //
            int OilfishBait = ModContent.ItemType<OilfishBait>(); //
            int WhetfishBait = ModContent.ItemType<WhetfishBait>(); //


            {
                if (inWater && inBeach && Player.GetFishingConditions().BaitItemType == MagnetBait && Main.rand.NextBool(1, 3))

                    itemDrop = SalvageableScrap; // """""fish""""" up the scrap (with more chance)
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
                if (inWater && inUnderground && Player.GetFishingConditions().BaitItemType == OilfishBait && Main.rand.NextBool(1, 2))

                    itemDrop = OilFish; // Fish up the Oilfish with more chance

            }

            {
                if (inWater && inCaverns && Player.GetFishingConditions().BaitItemType == OilfishBait && Main.rand.NextBool(1, 2))

                    itemDrop = OilFish; // Fish up the Oilfish with more chance

            }

            {
                if (inWater && inCaverns && Player.GetFishingConditions().BaitItemType == WhetfishBait && Main.rand.NextBool(1, 2))

                    itemDrop = Whetfish; // Fish up the Whetfish with more chance

            }

            {
                if (inWater && inUnderground && Player.GetFishingConditions().BaitItemType == WhetfishBait && Main.rand.NextBool(1, 2))

                    itemDrop = Whetfish; // Fish up the Whetfish with more chance

            }

            if (hasWaningCresBless && !attempt.crate)
            {
                if (Main.rand.Next(100) < 30)
                {
                    attempt.crate = true;
                }
            }

            if (hasWaxingCresBless && attempt.crate)
            {
                if (Main.rand.Next(100) < 10)
                {
                    attempt.crate = false;
                }
            }

            if (hasWaxingCresBless && attempt.common)
            {
                if (Main.rand.Next(100) < 25)
                {
                    attempt.uncommon = true;
                }
            }

            if (hasWaxingCresBless && attempt.uncommon)
            {
                if (Main.rand.Next(100) < 25)
                {
                    attempt.rare = true;
                }
            }

            if (hasWaxingCresBless && attempt.rare)
            {
                if (Main.rand.Next(100) < 25)
                {
                    attempt.veryrare = true;
                }
            }

            if (hasWaxingCresBless && attempt.veryrare)
            {
                if (Main.rand.Next(100) < 25)
                {
                    attempt.legendary = true;
                }
            }

            if (hasWaningCresBless && attempt.legendary)
            {
                if (Main.rand.Next(100) < 10)
                {
                    attempt.veryrare = true;
                }
            }

            if (hasWaningCresBless && attempt.veryrare)
            {
                if (Main.rand.Next(100) < 10)
                {
                    attempt.rare = true;
                }
            }

            if (hasWaningCresBless && attempt.rare)
            {
                if (Main.rand.Next(100) < 10)
                {
                    attempt.uncommon = true;
                }
            }

            if (hasWaningCresBless && attempt.uncommon)
            {
                if (Main.rand.Next(100) < 10)
                {
                    attempt.common = true;
                }
            }

            if (has34Bless)
            {
                if (Main.rand.Next(100) < 15)
                {
                    itemDrop += Main.rand.Next(1, 2);
                }
            }

            if (has14Bless)
            {
                if (Main.rand.Next(100) < 5)
                {
                    itemDrop = 0;
                }
            }



        }



        public override bool? CanConsumeBait(Item bait)
        {
            PlayerFishingConditions conditions = Player.GetFishingConditions();

            if (has14Bless)
            {

                if (Main.rand.Next(100) < 20)
                {
                    return false;
                }

            }

            if (has34Bless)
            {

                if (Main.rand.Next(100) < 15)
                {
                    return true;
                }

            }

            return null; // Let the default logic run
        }


    }

    }
