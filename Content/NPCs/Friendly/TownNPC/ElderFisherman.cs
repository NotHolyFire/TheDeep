
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using TheDeep.Common.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.UI;
using TheDeep.Content.Items.Consumables;
using TheDeep.Content.Items.Fish;
using TheDeep.Content.Items.Misc;
using TheDeep.Content.Items.Weapons;
using TheDeep.Content.Projectiles;
using TheDeep.Content.Tiles;
using TheDeep.Content.Items.Placeables;
using TheDeep.Content.Items.Weapons.Ranged;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria.DataStructures;
using Terraria.GameContent.Personalities;
using Terraria.GameContent.UI;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using TheDeep.Content.Items.Tools;
using TheDeep.Content.Items.Weapons.Summoning;
using TheDeep.Content.Items.Weapons.Melee;
using TheDeep.Content.NPCs.Boss.CrabBoss;
using TheDeep.Content.Items.Accessories;
using TheDeep.Content.Items.Consumables.Bait.Special;
using TheDeep.Content.Pets.Juberto;
using TheDeep.Content.Projectiles.player.Weapons.Ranged;

namespace TheDeep.Content.NPCs.Friendly.TownNPC
{

    public class ElderFisherman : ModNPC
    {
        public int NumberOfTimesTalkedTo = 0;
        public const string shopName = "Fishing Supplies";

        private static Profiles.StackedNPCProfile NPCProfile;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 26; // The total amount of frames the NPC has
            NPCID.Sets.AllowDoorInteraction[Type] = true;
            NPCID.Sets.ExtraFramesCount[Type] = 9; // Generally for Town NPCs, but this is how the NPC does extra things such as sitting in a chair and talking to other NPCs. This is the remaining frames after the walking frames.
            NPCID.Sets.AttackFrameCount[Type] = 4; // The amount of frames in the attacking animation.
            NPCID.Sets.DangerDetectRange[Type] = 700; // The amount of pixels away from the center of the NPC that it tries to attack enemies.
            NPCID.Sets.AttackType[Type] = 1; // The type of attack the Town NPC performs. 0 = throwing, 1 = shooting, 2 = magic, 3 = melee
            NPCID.Sets.AttackTime[Type] = 50; // The amount of time it takes for the NPC's attack animation to be over once it starts.
            NPCID.Sets.AttackAverageChance[Type] = 10; // The denominator for the chance for a Town NPC to attack. Lower numbers make the Town NPC appear more aggressive.
            NPCID.Sets.HatOffsetY[Type] = 4; // For when a party is active, the party hat spawns at a Y offset.
            NPCID.Sets.ShimmerTownTransform[NPC.type] = true; // This set says that the Town NPC has a Shimmered form. Otherwise, the Town NPC will become transparent when touching Shimmer like other enemies.
            NPCID.Sets.SpawnsWithCustomName[Type] = true;
            NPCID.Sets.ShimmerTownTransform[Type] = true; // Allows for this NPC to have a different texture after touching the Shimmer liquid.
            NPCID.Sets.ActsLikeTownNPC[Type] = true;
            NPCID.Sets.NoTownNPCHappiness[Type] = true;

            // Connects this NPC with a custom emote.
            // This makes it when the NPC is in the world, other NPCs will "talk about him".
            // By setting this you don't have to override the PickEmote method for the emote to appear.
            // emote

            // Influences how the NPC looks in the Bestiary
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
                Direction = -1 // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
                               // Rotation = MathHelper.ToRadians(180) // You can also change the rotation of an NPC. Rotation is measured in radians
                               // If you want to see an example of manually modifying these when the NPC is drawn, see PreDraw
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            // Set Example Person's biome and neighbor preferences with the NPCHappiness hook. You can add happiness text and remarks with localization (See an example in ExampleMod/Localization/en-US.lang).
            // NOTE: The following code uses chaining - a style that works due to the fact that the SetXAffection methods return the same NPCHappiness instance they're called on.

            // This creates a "profile" for ExamplePerson, which allows for different textures during a party and/or while the NPC is shimmered.

        }

        public override void SetDefaults()
        {
            NPC.friendly = true; // NPC Will not attack player
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 10;
            NPC.defense = 30;
            NPC.lifeMax = 450;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.knockBackResist = 0.5f;

            AnimationType = NPCID.Guide;
        }

        public override void AI()
        {

            if (!Main.dayTime)
            {
                NPC.active = false;

                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Cloud);
                }

                for (int i = 0; i < 3; i++) {
                    SoundEngine.PlaySound(SoundID.Run, NPC.Center);
            }

            }

        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("Mods.TheDeep.Bestiary.ElderFisherman"),
            });
        }

        public override ITownNPCProfile TownNPCProfile()
        {
            return NPCProfile;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            //If any player is on a beach/ocean, he will have a slight chance to spawn.
            if (spawnInfo.Player.ZoneBeach && Main.dayTime && !NPC.AnyNPCs(ModContent.NPCType<ElderFisherman>()))
            {
                return 0.34f;
            }

            //Else, the example bone merchant will not spawn if the above conditions are not met.
            return 0f;
        }

        public override bool CanChat()
        {
            return true;
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>() {
                "Charon",
                "Achilles",
                "Vergilius",
                "Minos"
            };
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            // Causes dust to spawn when the NPC DIES.
            if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Cloud);
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            /*npc.frame.Width = 40;
			if (((int)Main.time / 10) % 2 == 0)
			{
				npc.frame.X = 40;
			}
			else    
			{
				npc.frame.X = 0;
			}*/
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            int angler = NPC.FindFirstNPC(NPCID.Angler);
            if (angler >= 0 && Main.rand.NextBool(4))
            {
                chat.Add(Language.GetTextValue("Mods.TheDeep.Dialogue.ElderFisherman.AnglerDialogue", Main.npc[angler].GivenName));
            }
            // These are things that the NPC has a chance of telling you when you talk to it.
            chat.Add(Language.GetTextValue("Mods.TheDeep.Dialogue.ElderFisherman.StandardDialogue1"));
            chat.Add(Language.GetTextValue("Mods.TheDeep.Dialogue.ElderFisherman.StandardDialogue2"));
            chat.Add(Language.GetTextValue("Mods.TheDeep.Dialogue.ElderFisherman.StandardDialogue3"));
            chat.Add(Language.GetTextValue("Mods.TheDeep.Dialogue.ElderFisherman.StandardDialogue4"));
            chat.Add(Language.GetTextValue("Mods.TheDeep.Dialogue.ElderFisherman.UncommonDialogue"), 0.4);
            chat.Add(Language.GetTextValue("Mods.TheDeep.Dialogue.ElderFisherman.RareDialogue"), 0.1);

            NumberOfTimesTalkedTo++;
            if (NumberOfTimesTalkedTo >= 10)
            {
                //This counter is linked to a single instance of the NPC, so if ExamplePerson is killed, the counter will reset.
                chat.Add(Language.GetTextValue("Mods.TheDeep.Dialogue.ElderFisherman.TalkALot"));
            }

            string chosenChat = chat; // chat is implicitly cast to a string. This is where the random choice is made.

            return chosenChat;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        { // What the chat buttons are when you open up the chat UI
            button = Language.GetTextValue("LegacyInterface.28"); //This is the key to the word "Shop"
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shop)
        {
            if (firstButton)
            {
                shop = "Shop";
            }
        }


        public override void AddShops()
        {
            new NPCShop(Type)
                .Add(new Item(ModContent.ItemType<MultiRod>()) { shopCustomPrice = Item.buyPrice(gold: 10) })
                .Add(new Item(ModContent.ItemType<SoggyDie>()) { shopCustomPrice = Item.buyPrice(gold: 3) })
                .Add(new Item(ItemID.FrogLeg) { shopCustomPrice = Item.buyPrice(gold: 8) })
                .Add(new Item(ModContent.ItemType<KrillionKrill>()) { shopCustomPrice = Item.buyPrice(platinum: 1) })

                .Add(new Item(ModContent.ItemType<ApplefinBait>()) { shopCustomPrice = Item.buyPrice(silver: 15) }, Condition.MoonPhaseFull)
                 .Add(new Item(ModContent.ItemType<ApplefinBait>()) { shopCustomPrice = Item.buyPrice(silver: 15) }, Condition.MoonPhaseWaningGibbous)
                .Add(new Item(ModContent.ItemType<WhetfishBait>()) { shopCustomPrice = Item.buyPrice(silver: 15) }, Condition.MoonPhaseThirdQuarter)
                 .Add(new Item(ModContent.ItemType<WhetfishBait>()) { shopCustomPrice = Item.buyPrice(silver: 15) }, Condition.MoonPhaseWaningCrescent)
                 .Add(new Item(ModContent.ItemType<KelptopusBait>()) { shopCustomPrice = Item.buyPrice(silver: 15) }, Condition.MoonPhaseNew)
                 .Add(new Item(ModContent.ItemType<KelptopusBait>()) { shopCustomPrice = Item.buyPrice(silver: 15) }, Condition.MoonPhaseWaxingCrescent)
                 .Add(new Item(ModContent.ItemType<OilfishBait>()) { shopCustomPrice = Item.buyPrice(silver: 15) }, Condition.MoonPhaseFirstQuarter)
                .Add(new Item(ModContent.ItemType<OilfishBait>()) { shopCustomPrice = Item.buyPrice(silver: 15) }, Condition.MoonPhaseWaxingGibbous)

                .Add(new Item(ModContent.ItemType<Oilfish>()) { shopCustomPrice = Item.buyPrice(gold: 2 )}, Condition.MoonPhaseFull) 
                .Add(new Item(ModContent.ItemType<Oilfish>()) { shopCustomPrice = Item.buyPrice(gold: 2) }, Condition.MoonPhaseWaningGibbous)
                .Add(new Item(ModContent.ItemType<Kelptopus>()) { shopCustomPrice = Item.buyPrice(gold: 2) }, Condition.MoonPhaseThirdQuarter)
                .Add(new Item(ModContent.ItemType<Kelptopus>()) { shopCustomPrice = Item.buyPrice(gold: 2) }, Condition.MoonPhaseWaningCrescent)
                .Add(new Item(ModContent.ItemType<Whetfish>()) { shopCustomPrice = Item.buyPrice(gold: 2) }, Condition.MoonPhaseNew)
                .Add(new Item(ModContent.ItemType<Whetfish>()) { shopCustomPrice = Item.buyPrice(gold: 2) }, Condition.MoonPhaseWaxingCrescent)
                .Add(new Item(ModContent.ItemType<Applefin>()) { shopCustomPrice = Item.buyPrice(gold: 2) }, Condition.MoonPhaseFirstQuarter)
                .Add(new Item(ModContent.ItemType<Applefin>()) { shopCustomPrice = Item.buyPrice(gold: 2) }, Condition.MoonPhaseWaxingGibbous)
                .Register();
        }


        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.RainHat));

        }

        public override void LoadData(TagCompound tag)
        {
            NumberOfTimesTalkedTo = tag.GetInt("numberOfTimesTalkedTo");
        }

        public override void SaveData(TagCompound tag)
        {
            tag["numberOfTimesTalkedTo"] = NumberOfTimesTalkedTo;
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 30;
            knockback = 2f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 0;
            randExtraCooldown = 1;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<RedCoralShot>();
            attackDelay = 1;

            // This code progressively delays subsequent shots.
            if (NPC.localAI[3] > attackDelay)
            {
                attackDelay = 12;
            }
            if (NPC.localAI[3] > attackDelay)
            {
                attackDelay = 24;
            }
            if (NPC.localAI[3] > attackDelay)
            {
                attackDelay = 36;
            }
            if (NPC.localAI[3] > attackDelay)
            {
                attackDelay = 48;
            }
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 14f;
        }

        public override void TownNPCAttackShoot(ref bool inBetweenShots)
        {
            if (NPC.localAI[3] > 1)
            {
                inBetweenShots = true;
            }
        }

        public override void DrawTownAttackGun(ref Texture2D item, ref Rectangle itemFrame, ref float scale, ref int horizontalHoldoutOffset)
        {
                // If using an existing item, use this approach
                int itemType = ModContent.ItemType<Shellshocker>();
                Main.GetItemDrawFrame(itemType, out item, out itemFrame);
                horizontalHoldoutOffset = (int)Main.DrawPlayerItemPos(1f, itemType).X - 15;
            }
    }
}