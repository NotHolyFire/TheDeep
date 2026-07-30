using Terraria;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using TheDeep.Content.Buffs;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using TheDeep.Content.Items.Placeables;
using TheDeep.Content.Buffs.Debuff;


namespace TheDeep.Content.NPCs.Hostile
{
    public class ScubaSlime : ModNPC
    {
        public int Timer;
        public int AnimTime;
        public int ReadyToJump;
        public int JumpTimer;

        public static int JumpVelocity = -8; // Altura do pulo
        public static int JumpCool = 60; // Tempo entre pulos

        public bool Jumped;


        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 2;

            NPCID.Sets.ShimmerTransformToNPC[Type] = NPCID.ShimmerSlime;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers();

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }
        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 32;

            NPC.damage = 10;
            NPC.defense = 1;
            NPC.lifeMax = 30;           
            NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<Pressure>()] = true;


            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;

            NPC.value = 60f;
            NPC.knockBackResist = 0.8f;
            NPC.aiStyle = -1;

            Banner = Type;
            BannerItem = ModContent.ItemType<ScubaSlimeBanner>();

            AnimationType = NPCID.BlueSlime;
        }
        #region animation, drops and spawn conditions
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
        }
        public override void ModifyNPCLoot(NPCLoot loot)
        {
            var SlimeDropRules = Main.ItemDropsDB.GetRulesForNPCID(NPCID.BlueSlime, false);
            foreach (var SlimeDropRule in SlimeDropRules)
            {
                loot.Add(SlimeDropRule);
            }

            loot.Add(ItemDropRule.Common(ItemID.IronAnvil, 1000));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!Main.dayTime)
                return 0f;
                
            return SpawnCondition.Ocean.Chance * 0.2f;
        }
        #endregion

        public ref float AI_State => ref NPC.ai[0];

        private enum ActionState
        {
            Idle,
            Jump,
            Fall
        }

        public override void AI()
        {
            var entitySource = NPC.GetSource_FromAI();
            if (!NPC.collideX && !NPC.collideY) NPC.velocity.X += (float)NPC.direction * 0.06f;
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }

            switch (AI_State)
            {
                case (float)ActionState.Idle:
                    Idle();
                    break;
                case (float)ActionState.Jump:
                    Jump();
                    break;
                case (float)ActionState.Fall:
                    Fall();
                    break;
            }
        } // I feel it's an obligation for me to say that this code isn't mine, it was made by my friend Klon, thx man - Azure

        private void Idle()
        {
            Player player = Main.player[NPC.target];

            if (NPC.collideY) { ReadyToJump--; NPC.velocity.X = 0; }

            if (NPC.wet)
            {
                if (ReadyToJump <= 0)
                {
                    AnimTime = 0;
                    AI_State = (float)ActionState.Jump;
                }
            }
            else
            {
                if (ReadyToJump <= 0)
                {
                    AnimTime = 0;
                    AI_State = (float)ActionState.Jump;
                }
            }
        }

        private void Jump()
        {
            Player player = Main.player[NPC.target];

            if (!Jumped || NPC.collideY) { JumpTimer++; }

            if (player.Center.X > NPC.Center.X)
            {
                NPC.direction = 1;
            }
            else
            {
                NPC.direction = -1;
            }

            NPC.velocity.X = (float)NPC.direction * 0.03f;


            if (!Jumped && JumpTimer >= 90)
            {
                NPC.velocity.Y = JumpVelocity;
                NPC.velocity.X *= 100f;
                AnimTime = 0;
                JumpTimer = 0;
                Jumped = true;
                ReadyToJump = JumpCool;
                AI_State = (float)ActionState.Fall;
            }
        }

        private void Fall()
        {
            if (NPC.collideY) { AI_State = (float)ActionState.Idle; AnimTime = 0; }

            if (NPC.wet)
            {
                if (Jumped || !NPC.collideY)
                {
                    if (NPC.collideY) { Jumped = false; JumpTimer = 0; }
                    if (NPC.velocity.Y == 0) { Jumped = false; JumpTimer = 0; }

                    if (NPC.velocity.Y >= 0.2f)
                    {
                        NPC.velocity.Y += -0.02f;
                    }
                }
            }
            else
            {
                if (Jumped || !NPC.collideY)
                {
                    if (NPC.collideY) { Jumped = false; JumpTimer = 0; }
                    if (NPC.velocity.Y == 0) { Jumped = false; JumpTimer = 0; }

                    if (NPC.velocity.Y >= 0.2f)
                    {
                        NPC.velocity.Y += -0.02f;
                    }
                }
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.t_Slime, hit.HitDirection, -1f, 0, Color.LightBlue, 1f);
            }
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 25; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.t_Slime, hit.HitDirection, -1f, 0, Color.LightBlue, 1f);
                }
            }
        }


        //Bestiary
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {

            bestiaryEntry.Info.AddRange([

				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,

				new FlavorTextBestiaryInfoElement("Mods.SubmergedMod.Bestiary.ScubaSlime"),
            ]);
        }
    }
}
