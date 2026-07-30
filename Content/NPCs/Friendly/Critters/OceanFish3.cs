using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace TheDeep.Content.NPCs.Friendly.Critters
{
    public class OceanFish3 : ModNPC 
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
            NPCID.Sets.CountsAsCritter[NPC.type] = true;
        }

        public override void SetDefaults()
        {
            NPC.npcSlots = 1f;
            NPC.noGravity = true;
            NPC.damage = 0;

            NPC.width = 12;
            NPC.height = 12;

            NPC.defense = 0;
            NPC.lifeMax = 5;

            NPC.aiStyle = -1;
            AIType = NPCID.Goldfish;

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;

            NPC.chaseable = false;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
                new FlavorTextBestiaryInfoElement("Mods.SubmergedMod.Bestiary.Oceanfish")
            });
        }

        public override void AI()
        {
            
            NPC.spriteDirection = (NPC.direction > 0) ? 1 : -1;
            NPC.noGravity = true;
            bool shouldSwimAway = false;
            if (NPC.direction == 0)
            {
                NPC.TargetClosest(true);
            }
            if (NPC.wet)
            {
                NPC.TargetClosest(false);
                if (Main.player[NPC.target].wet && !Main.player[NPC.target].dead &&
                    (Main.player[NPC.target].Center - NPC.Center).Length() < 150f)
                {
                    shouldSwimAway = true;
                }

                if ((!Main.player[NPC.target].wet || Main.player[NPC.target].dead) && shouldSwimAway)
                {
                    shouldSwimAway = false;
                }

                if (!shouldSwimAway)
                {
                    if (NPC.collideX || NPC.velocity.X == 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X * -3f;
                        NPC.direction *= -1;
                        NPC.netUpdate = true;
                    }
                    if (NPC.collideY)
                    {
                        NPC.netUpdate = true;
                        if (NPC.velocity.Y > 0f)
                        {
                            NPC.velocity.Y = Math.Abs(NPC.velocity.Y) * -3f;
                            NPC.directionY = -1;
                            NPC.ai[0] = -1f;
                        }
                        else if (NPC.velocity.Y < 0f)
                        {
                            NPC.velocity.Y = Math.Abs(NPC.velocity.Y);
                            NPC.directionY = 1;
                            NPC.ai[0] = 1f;
                        }
                    }
                }

                if (shouldSwimAway)
                {
                    NPC.TargetClosest(true);
                    NPC.velocity.X = NPC.velocity.X - (float)NPC.direction * 0.25f;
                    NPC.velocity.Y = NPC.velocity.Y - (float)NPC.directionY * 0.15f;
                    if (NPC.velocity.X > 6f)
                    {
                        NPC.velocity.X = 6f;
                    }
                    if (NPC.velocity.X < -6f)
                    {
                        NPC.velocity.X = -6f;
                    }
                    if (NPC.velocity.Y > 6f)
                    {
                        NPC.velocity.Y = 6f;
                    }
                    if (NPC.velocity.Y < -6f)
                    {
                        NPC.velocity.Y = -6f;
                    }
                    NPC.direction *= -1;
                }
                else
                {
                    NPC.velocity.X = NPC.velocity.X + (float)NPC.direction * 0.1f;
                    if (NPC.velocity.X < -2.5f || NPC.velocity.X > 2.5f)
                    {
                        NPC.velocity.X = NPC.velocity.X * 0.95f;
                    }
                    if (NPC.ai[0] == -1f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - 0.01f;
                        if ((double)NPC.velocity.Y < -0.3)
                        {
                            NPC.ai[0] = 1f;
                        }
                    }
                    else
                    {
                        NPC.velocity.Y = NPC.velocity.Y + 0.01f;
                        if ((double)NPC.velocity.Y > 0.3)
                        {
                            NPC.ai[0] = -1f;
                        }
                    }
                }

                int NPCTileX = (int)(NPC.position.X + (float)(NPC.width / 2)) / 16;
                int NPCTileY = (int)(NPC.position.Y + (float)(NPC.height / 2)) / 16;

                if (Main.tile[NPCTileX, NPCTileY - 1].LiquidAmount > 128)
                {
                    if (Main.tile[NPCTileX, NPCTileY + 1].HasTile)
                    {
                        NPC.ai[0] = -1f;
                    }
                    else if (Main.tile[NPCTileX, NPCTileY + 2].HasTile)
                    {
                        NPC.ai[0] = -1f;
                    }
                }
                if ((double)NPC.velocity.Y > 0.4 || (double)NPC.velocity.Y < -0.4)
                {
                    NPC.velocity.Y = NPC.velocity.Y * 0.95f;
                }
            }
            else
            {
                if (NPC.velocity.Y == 0f)
                {
                    NPC.velocity.X = NPC.velocity.X * 0.94f;
                    if ((double)NPC.velocity.X > -0.2 && (double)NPC.velocity.X < 0.2)
                    {
                        NPC.velocity.X = 0f;
                    }
                }
                NPC.velocity.Y = NPC.velocity.Y + 0.3f;
                if (NPC.velocity.Y > 10f)
                {
                    NPC.velocity.Y = 10f;
                }
                NPC.ai[0] = 1f;
            }
                NPC.rotation = NPC.velocity.X * 0.05f;
                if ((double)NPC.rotation < -0.1)
                {
                    NPC.rotation = -0.1f;
                }
                if ((double)NPC.rotation > 0.1)
                {
                    NPC.rotation = 0.1f;
                    return;
                }
        }
        public override void FindFrame(int frameHeight)
        {
            if (!NPC.wet && !NPC.IsABestiaryIconDummy)
            {
                NPC.frameCounter = 0.0;
                return;
            }
            NPC.frameCounter += 0.075f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.Ocean.Chance * 0.7f;
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f, 0, default, 1f);
            }
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 25; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f, 0, default, 1f);
                }
                if (Main.netMode != NetmodeID.Server)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("OceanFish3Gore").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("OceanFish3Gore_2").Type, 1f);

                }
            }
        }
    }
}
