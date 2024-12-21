
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
// using TheDeep.Content.Items.Accessories;

namespace TheDeep.Content.NPCs.Boss.CrabBoss
{

    [AutoloadBossHead]
    public class CrabBoss : ModNPC
    {
        public int Timer;
        public int Jumping; // yeah i'm SURE this will work
        public int ReadyToJump;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 12;

            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);

            // Immunities
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Wet] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
            // NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<Pressure>] = true; // Later


            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { 
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public bool SecondStage;

        public override void SetDefaults()
        {

            NPC.width = 236;
            NPC.height = 118;
            NPC.damage = 20;
            NPC.defense = 12;
            NPC.waterMovementSpeed = 1.2f;
            NPC.lifeMax = 3194;
            NPC.HitSound = SoundID.NPCHit31;
            NPC.DeathSound = SoundID.NPCDeath34;
            NPC.GravityIgnoresLiquid = true;
            NPC.value = Item.buyPrice(gold: 5);
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.aiStyle = 3; // fighter ai but i could probably make a custom ai but ehhhh idk

            AIType = NPCID.Crab;
            AnimationType = NPCID.Zombie;

            NPC.BossBar = ModContent.GetInstance<CrabBossBar>();

            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Content/Assets/Music/Shoneyy_SupremeRoyalty"); // hehe funny music..
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            // Do NOT misuse the ModifyNPCLoot and OnKill hooks: the former is only used for registering drops, the latter for everything else

            // The order in which you add loot will appear as such in the Bestiary. To mirror vanilla boss order:
            // 1. Trophy
            // 2. Classic Mode ("not expert")
            // 3. Expert Mode (usually just the treasure bag)
            // 4. Master Mode (relic first, pet last, everything else inbetween)

            // Trophies are spawned with 1/10 chance
            // npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Placeable.Furniture.CrabBossTrophy>(), 10));

            // All the Classic Mode drops here are based on "not expert", meaning we use .OnSuccess() to add them into the rule, which then gets added
            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

            // Notice we use notExpertRule.OnSuccess instead of npcLoot.Add so it only applies in normal mode
            // Boss masks are spawned with 1/7 chance
            // notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SunkenCrown>(), 7)); // Mask

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<krabby_patty>()));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Shellshock>()));

            // Finally add the leading rule
            npcLoot.Add(notExpertRule);

            // Add the treasure bag using ItemDropRule.BossBag (automatically checks for expert mode)
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<CrabBossBag>()));

            // ItemDropRule.MasterModeCommonDrop for the relic
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<CrabBossRelic>()));

            // ItemDropRule.MasterModeDropOnAllPlayers for the pet
            // npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<SecretFormula>(), 4));
        }

        public override void AI()
        {

            Timer++;
            if (Timer == 125 && NPC.collideY == true && SecondStage == false && NPC.wet == false)
            {
                NPC.velocity = new Vector2(NPC.direction * 5, -10f); // basic jump
                ReadyToJump = 1;

            }

            if (Timer == 130 && NPC.wet == true)
            {
                NPC.velocity = new Vector2(NPC.direction * 5, -25f);
                ReadyToJump = 1;
            }

            if (Timer == 100)
            {
                var source = NPC.GetSource_FromAI();
                NPC.NewNPC(source, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Caranguinho>(), 0, 1, 0, 0, 0, 255); // summon some little minions :3
            }

            if (Timer == 90 && NPC.collideY == true && SecondStage == true)
            {
                NPC.velocity = new Vector2(NPC.direction * 8, -10f);
                ReadyToJump = 1;

            }

            if (!Main.player[NPC.target].ZoneBeach && NPC.collideY == true && SecondStage == false)
            {
                NPC.velocity = new Vector2(NPC.direction * 8, -10f);
                ReadyToJump = 1;
                Main.NewText("CRUSH!", Color.Aqua); // ultrakill references...
            }

            if (!Main.player[NPC.target].ZoneBeach)
            {
                NPC.defense = 33;
                NPC.damage = 54;
            }

            if (!Main.player[NPC.target].ZoneBeach && NPC.collideY == true && SecondStage == true)
            {
                NPC.velocity = new Vector2(NPC.direction * 12, -10f);
                ReadyToJump = 1;
                Main.NewText("JUDGEMENT!", Color.Aqua);
            }

            if (NPC.collideY == false && ReadyToJump == 1) // could be way better but eh
            {
                Jumping = 1;
                ReadyToJump = 0;
            } 

            if (Jumping == 1 && NPC.collideY == true)
            {
                var source = NPC.GetSource_FromAI();
                float startingSpawnX = NPC.Center.X;
                Vector2 position = NPC.Center;

                int type = ModContent.ProjectileType<Shockwave>();
                int damage = 35;
                float velocity = 2f;
                int type2 = ModContent.ProjectileType<ShockwaveL>(); // hi holy from the future this sucks please make this an actual direction switch you imbecile
                Projectile.NewProjectile(source, position, velocity * new Vector2(5, 0f), type, damage, 0f);
                Projectile.NewProjectile(source, position, velocity * new Vector2(-5, 0f), type2, damage, 0f);
                SoundEngine.PlaySound(SoundID.Item14);
                Jumping = 0;
                Timer = 0;

            } // oh it did work

            if (Timer >= 130)
            {
                Timer = 0;
            }

            if (NPC.life <= NPC.lifeMax * 0.5f && SecondStage == false)
            {
                var source = NPC.GetSource_FromAI();
                NPC.NewNPC(source, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<RoyalTridentNPC>(), 0, 1, 0, 0, 0, 255);
                SecondStage = true;
                SoundEngine.PlaySound(SoundID.Roar);
            }

            if (Main.getGoodWorld && Timer == 125 && SecondStage == true)
            {
                var source = NPC.GetSource_FromAI();
                NPC.NewNPC(source, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<RoyalTridentNPC>(), 0, 1, 0, 0, 0, 255);
            }

            if (Main.getGoodWorld)
            {
                NPC.scale = 0.35f;
                NPC.width = 100;
                NPC.height = 80;
            }

            if (Main.player[NPC.target].dead)
            {
                NPC.velocity = new Vector2(0, -35);
                NPC.EncourageDespawn(5);
                NPC.noTileCollide = true;
                SoundEngine.PlaySound(SoundID.Roar);
            }

        }
        public override void OnKill()
        {
            if (!DownedBossSystem.downedCrabBoss)
            {
                ModContent.GetInstance<ZincOreSystem>().BlessWorldWithZinc();
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            // play sound and boss DIE
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0)
            {
                // Gores by example mod
                int bodyGoreType = Mod.Find<ModGore>("CrabBossGore_Body").Type;
                int legGoreType = Mod.Find<ModGore>("CrabBossGore_Leg").Type;
                int armGoreType = Mod.Find<ModGore>("CrabBossGore_Arm").Type;

                var entitySource = NPC.GetSource_Death();

                for (int i = 0; i < 2; i++)
                {
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), bodyGoreType);
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), legGoreType);
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), armGoreType);
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
                int startFrame = 0;
                int finalFrame = 12;

            if (SecondStage)
            {
                startFrame = 7;
                finalFrame = 12;

                if (NPC.frame.Y < startFrame * frameHeight)
                {
                    NPC.frame.Y = startFrame * frameHeight;
                }
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("Testes muitos testez."), // wow so muitos testes
            });
        }
        }
    }