
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
using static System.Net.Mime.MediaTypeNames;
using Terraria.Localization;
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using TheDeep.Content.Projectiles.Npc.CrabBoss;
// using TheDeep.Content.Items.Accessories;

namespace TheDeep.Content.NPCs.Boss.CrabBoss
{

    [AutoloadBossHead]
    public class CrabBoss : ModNPC
    {
        // int
        public int Timer;
        public int KiteTimer;
        public int StaminaRegen;
        public int Stamina;
        public int MaxStamina;
        public int SprintTime;
        public int BubbleAmount;
        public int BubblesShot;
        public int BubbleTimer;

        // states
        public bool SecondStage;
        public bool Enraged;

        // actions
        public bool AttemptJump; // Slam Jump
        public bool Jumping;
        public bool ReadyToJump;
        public bool AttemptSprint; // Sprint
        public bool Sprinting;
        public bool AttemptKite; // what if you're far away but coastal king said AttemptKite
        public bool AttemptBubble; // https://bulbapedia.bulbagarden.net/wiki/Bubble_Beam_(move)
        public bool BubbleBeaming;




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

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("A crab banned from the ocean by a higher force found a trident and a crown. The other crabs living near the coast had no option but follow the new tyrant and leader of the beach."),
            });
        }


        public override void SetDefaults()
        {

            NPC.width = 236;
            NPC.height = 118;
            NPC.damage = 30;
            NPC.defense = 23;
            NPC.waterMovementSpeed = 1.2f;
            NPC.lifeMax = 3300;
            NPC.HitSound = SoundID.NPCHit31;
            NPC.DeathSound = SoundID.NPCDeath34;
            NPC.GravityIgnoresLiquid = true;
            NPC.npcSlots = 6f;
            NPC.value = Item.buyPrice(gold: 5);
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.SpawnWithHigherTime(30);
            NPC.aiStyle = 3; // fighter ai but i could probably make a custom ai but ehhhh idk

            AIType = NPCID.Crab;
            AnimationType = NPCID.Zombie;

            NPC.BossBar = ModContent.GetInstance<CrabBossBar>();

            if (!Main.dedServ && !Main.getGoodWorld)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Content/Assets/Music/Shoneyy_SupremeRoyalty"); // hehe funny music..
            }

            if (!Main.dedServ && Main.getGoodWorld)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Content/Assets/Music/Noisestorm_CrabRave");
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
            // Timer

            Timer += 2;

            // Stamina
            string currentStamina = Stamina.ToString();

            if (Stamina <= 0)
            {
                StaminaRegen++;
            }

            if (StaminaRegen >= Main.rand.Next(180, 360)) // 3 to 6 seconds of recovery
            {
                Stamina = MaxStamina;
                StaminaRegen = 0;
            }

            if (Stamina > MaxStamina)
            {
                Stamina = MaxStamina; // This is to stop from, after enraging, it to just have a lot of stamina even when un-raged
            }

            if (Main.expertMode == false)
            {
                MaxStamina = 6;
            }

            if (Main.expertMode == true && Main.masterMode == false)
            {
                MaxStamina = 9;
            }

            if (Main.masterMode == true && Main.getGoodWorld == false)
            {
                MaxStamina = 12;
            }

            if (Main.getGoodWorld == true)
            {
                MaxStamina = 99999;
            }

            if (Enraged == true)
            {
                MaxStamina = 99999;
            }

            // Slam Jump

            if (Timer == 150 && Stamina >= 2)
            {
                if (Main.rand.NextBool(3))
                {
                    AttemptJump = true;
                }
            }


            if (AttemptJump == true && NPC.collideY == true && SecondStage == false && NPC.wet == false)
            {
                NPC.velocity = new Vector2(NPC.direction * 5, -10f); // basic jump
                ReadyToJump = true;

            }

            if (AttemptJump == true && NPC.wet == true)
            {
                NPC.velocity = new Vector2(NPC.direction * 5, -25f);
                ReadyToJump = true;
            }


            if (AttemptJump == true && NPC.collideY == true && SecondStage == true)
            {
                NPC.velocity = new Vector2(NPC.direction * 8, -10f);
                ReadyToJump = true;

            }


            if (Enraged == true && AttemptJump == true && NPC.collideY == true && SecondStage == false)
            {
                NPC.velocity = new Vector2(NPC.direction * 8, -10f);
                ReadyToJump = true;

                Color messageColor = Color.Cyan;
                Rectangle location = new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height);
                CombatText.NewText(location, messageColor, "CRUSH!", true);
            }


            if (Enraged == true && AttemptJump == true && NPC.collideY == true && SecondStage == true)
            {
                NPC.velocity = new Vector2(NPC.direction * 12, -10f);
                ReadyToJump = true;
                Color messageColor = Color.Cyan;
                Rectangle location = new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height);
                CombatText.NewText(location, messageColor, "JUDGEMENT!", true);
            }

            if (NPC.collideY == false && ReadyToJump == true && SecondStage == false)
            {
                Jumping = true;
                ReadyToJump = false;
                AttemptJump = false;
            }

            if (NPC.collideY == false && ReadyToJump == true && SecondStage == true)
            {
                Jumping = true;
                ReadyToJump = false;
            }

            if (Jumping == true && NPC.collideY == true)
            {
                var source = NPC.GetSource_FromAI();
                Vector2 position = NPC.Bottom + new Vector2(0, -20f);

                int type = ModContent.ProjectileType<Shockwave>();
                int damage = NPC.damage / 2;
                float velocity = 2.5f;

                Projectile.NewProjectile(source, position, velocity * new Vector2(5, 0f), type, damage, 0f);
                Projectile.NewProjectile(source, position, velocity * new Vector2(-5, 0f), type, damage, 0f);
                SoundEngine.PlaySound(SoundID.Item14);
                Jumping = false;
                AttemptJump = false;


                Timer = 0;
                Stamina -= 2;

            } // oh it did work

            // Sprint

            if (Timer == 150 && Stamina >= 1 && AttemptJump == false)
            {
                if (Main.rand.NextBool(4))
                {
                    AttemptSprint = true;
                }

            }

            if (AttemptSprint == true && NPC.collideY == true)
            {
                Sprinting = true;
                AttemptSprint = false;
            }

            if (Sprinting == true)
            {
                NPC.velocity = new Vector2(NPC.direction * 22, -3);
                NPC.damage = 40;
                Vector2 position = NPC.Bottom + new Vector2(0, -30f);
                Dust.NewDust(position, 70, 70, DustID.Cloud);
                Stamina -= 1;
                Timer = 0;
                Sprinting = false;
            }

            // Kite

            if (Vector2.Distance(Main.player[NPC.target].Center, NPC.Center) > 1000 && AttemptKite == false && Main.netMode != NetmodeID.MultiplayerClient && NPC.wet == false)
            {
                KiteTimer++;
            }

            if (KiteTimer >= 60)
            {
                KiteTimer = 0;
                AttemptKite = true;
            }

            if (AttemptKite == true && Vector2.Distance(Main.player[NPC.target].Center, NPC.Center) < 900 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                AttemptKite = false;
                KiteTimer = -120; // Fail Kiting
                Stamina -= 3;
            }

            if (AttemptKite == true && Vector2.Distance(Main.player[NPC.target].Center, NPC.Center) > 900 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                AttemptKite = false;
                KiteTimer = 0;
                float kitingOffsetX = Utils.Clamp(Main.player[NPC.target].velocity.X * 16, -100, 100);
                    Vector2 position = Main.player[NPC.target].Bottom + new Vector2(kitingOffsetX + Main.rand.Next(-25, 25), Main.rand.Next(40, 80));

                int type = ModContent.ProjectileType<TridentShotHostile>();
                    int damage = NPC.damage / 3;
                    var entitySource = NPC.GetSource_FromAI();

                    Projectile.NewProjectile(entitySource, position, new Vector2(0f, -2.5f), type, damage, 0f, Main.myPlayer);
                
            }

            // Bubble

            if (Timer == 100 && Stamina >= 2)
            {
                if (Main.rand.NextBool(4))
                {
                    AttemptBubble = true;
                }

            }

            if (AttemptBubble == true)
            {
                BubbleBeaming = true;
                BubbleTimer = 60;
            }

            if (BubbleBeaming == true && BubbleTimer >= 60)
            {
                    var source = NPC.GetSource_FromAI();
                    Vector2 position = NPC.Center;
                    Vector2 targetPosition = Main.player[NPC.target].Center;
                    Vector2 direction = targetPosition - position;
                    direction.Normalize();
                    float speed = 5f;
                    int type = ModContent.ProjectileType<ToxinBubble>();
                    int damage = 10;
                    Projectile.NewProjectile(source, position, direction * speed, type, damage, 0f, Main.myPlayer);
                    SoundEngine.PlaySound(SoundID.Item85);
                    BubblesShot += 1;
                    BubbleTimer = 0;
            }

            if (BubblesShot > 0)
            {
                BubbleTimer++;
            }

            if (BubblesShot >= 3)
            {
                BubblesShot = 0;
                AttemptBubble = false;
                BubbleBeaming = false;
                Stamina -= 1;
                BubbleTimer = 0;
            }


            // Rest
            if (Timer >= 200)
            {
                Timer = 50;
                Main.NewText(currentStamina, 50, 255, 130);
            }


            // Phase 2

            if (NPC.life <= NPC.lifeMax * 0.5f && SecondStage == false && Main.expertMode == true)
            {
                var source = NPC.GetSource_FromAI();
                NPC.NewNPC(source, (int)NPC.Top.X, (int)NPC.Top.Y, ModContent.NPCType<RoyalTridentNPC>(), 0, 1, 0, 0, 0, 255);
                SecondStage = true;
                SoundEngine.PlaySound(SoundID.Roar);
            }

            // Enrage

            if (!Main.player[NPC.target].ZoneBeach)
            {
                Enraged = true;
            }
            else
                Enraged = false;

            // Despawn

            if (Main.player[NPC.target].dead)
            {
                NPC.velocity = new Vector2(0, -45);
                NPC.EncourageDespawn(5);
                NPC.noTileCollide = true;
                SoundEngine.PlaySound(SoundID.Roar);
            }

            // Suicide Save 

            if (Vector2.Distance(Main.player[NPC.target].Center, NPC.Center) > 3000)
            {
                Vector2 Position = Main.player[NPC.target].Top;
                NPC.Teleport(Position + new Vector2(0, -200));
                Jumping = true;
                NPC.life -= 300;
                Main.NewText(Language.GetTextValue("Mods.TheDeep.Dialogue.Boss.Oops"), Color.Red);
            }

            // target

            if (Vector2.Distance(Main.player[NPC.target].Center, NPC.Center) > 2500)
            {
                NPC.TargetClosest();
            }

            // other

            
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

                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), bodyGoreType);

                for (int i = 0; i < 2; i++)
                {
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), legGoreType);
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), armGoreType);
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            int startFrame = 0;
            int finalFrame = 5;

            if (SecondStage)
            {
                startFrame = 6;
                finalFrame = 12;

                if (NPC.frame.Y < startFrame * frameHeight)
                {
                    NPC.frame.Y = startFrame * frameHeight;
                }
            }

            int frameSpeed = 14;
            NPC.frameCounter += 0.5f;
            if (NPC.frameCounter > frameSpeed)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;

                if (NPC.frame.Y > finalFrame * frameHeight)
                {
                    NPC.frame.Y = startFrame * frameHeight;
                }
            }
        }
    }
}