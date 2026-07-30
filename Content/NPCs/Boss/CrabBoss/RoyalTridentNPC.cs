
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.UI;
using TheDeep.Content.NPCs.Friendly.TownNPC;
using TheDeep.Content.Projectiles;
using TheDeep.Content.Projectiles.Npc.CrabBoss;

namespace TheDeep.Content.NPCs.Boss.CrabBoss
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class RoyalTridentNPC : ModNPC
    {

        public int Timer;
        public bool ChargingUp;
        public int ChargeTimer;

        public override void SetStaticDefaults()
        {

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }


        public override void SetDefaults()
        {
            Main.npcFrameCount[Type] = 7;

            NPC.width = 30;
            NPC.height = 30;
            NPC.damage = 30;
            NPC.defense = 10;
            NPC.immortal = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.lifeMax = 1;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath39;
            NPC.GravityIgnoresLiquid = true;    
            NPC.knockBackResist = 0.0f;
            NPC.aiStyle = 0; // fighter ai but i could probably make a custom ai later

            AnimationType = NPCID.Retinazer; // Use vanilla zombie's type when executing animation code. Important to also match Main.npcFrameCount[NPC.type] in SetStaticDefaults.
        }
        public override void AI()
        {
            Timer++;

            DrawOffsetY = 25; // These values match the values in SetDefaults

            Vector2 toPlayer = Main.player[NPC.target].Center - NPC.Center;

            float offsetX = 200f;

            Vector2 abovePlayer = Main.player[NPC.target].Top + new Vector2(NPC.direction * offsetX, -NPC.height);

            Vector2 toAbovePlayer = abovePlayer - NPC.Center;
            Vector2 toAbovePlayerNormalized = toAbovePlayer.SafeNormalize(Vector2.UnitY);

            // The NPC tries to go towards the offsetX position, but most likely it will never get there exactly, or close to if the player is moving
            // This checks if the npc is "70% there", and then changes direction
            float changeDirOffset = offsetX * 0.7f;

            if (NPC.direction == -1 && NPC.Center.X - changeDirOffset < abovePlayer.X ||
                NPC.direction == 1 && NPC.Center.X + changeDirOffset > abovePlayer.X)
            {
                NPC.direction *= -1;
            }

            float movespeed = 8f;
            float inertia = 40f;

            // If the boss is somehow below the player, move faster to catch up
            if (NPC.Top.Y > Main.player[NPC.target].Bottom.Y)
            {
                movespeed = 12f;
            }

            Vector2 moveTo = toAbovePlayerNormalized * movespeed;
            NPC.velocity = (NPC.velocity * (inertia - 1) + moveTo) / inertia;

            NPC.rotation = toPlayer.ToRotation() - MathHelper.PiOver2;

            if (Timer >= 120)
            {
                ChargingUp = true;
                SoundEngine.PlaySound(SoundID.Item109);
                Timer = 0;

            }

            if (ChargingUp == true)
            {
                ChargeTimer++;

                if (Main.rand.NextBool(2))
                {
                    Dust.NewDust(NPC.position, 30, 30, DustID.HallowSpray);
                }
            }

            
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient && ChargeTimer >= 60 && Main.expertMode)
            {
                var source = NPC.GetSource_FromAI();
                SoundEngine.PlaySound(SoundID.Item125);
                Vector2 position = NPC.Center;
                Vector2 targetPosition = Main.player[NPC.target].Center;
                Vector2 direction = targetPosition - position;
                direction.Normalize();
                float speed = 12f;
                int type = ModContent.ProjectileType<TridentShotHostile>();
                int damage = NPC.damage /2;
                Projectile.NewProjectile(source, position, direction * speed, type, damage, 0f, Main.myPlayer);
                ChargingUp = false;
                ChargeTimer = 0;
                Timer = 0;
            }

            if (!NPC.AnyNPCs(ModContent.NPCType<CrabBoss>()))
                {
                NPC.life = 0;
            }
        }

    public override void OnSpawn(IEntitySource source)
        {
            Dust.NewDust(NPC.position, 70, 70, DustID.Gold);
            Dust.NewDust(NPC.position, 70, 70, DustID.Gold);
            Dust.NewDust(NPC.position, 70, 70, DustID.GemRuby);
            SoundEngine.PlaySound(SoundID.NPCDeath14);
            SoundEngine.PlaySound(SoundID.NPCDeath44);
        }
        
        }
    }