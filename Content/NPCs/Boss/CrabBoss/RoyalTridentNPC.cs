
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
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
using TheDeep.Content.Projectiles;

namespace TheDeep.Content.NPCs.Boss.CrabBoss
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class RoyalTridentNPC : ModNPC
    {

        public int Timer;
        public override void SetStaticDefaults()
        {

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public int ParentIndex
        {
            get => (int)NPC.ai[0] - 1;
            set => NPC.ai[0] = value + 1;
        }

        public bool HasParent => ParentIndex > -1;

        public override void SetDefaults()
        {
            Main.npcFrameCount[Type] = 6;

            NPC.width = 50;
            NPC.height = 50;
            NPC.damage = 30;
            NPC.defense = 10;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.lifeMax = 500;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath39;
            NPC.GravityIgnoresLiquid = true;    
            NPC.knockBackResist = 0.0f;
            NPC.aiStyle = 23; // fighter ai but i could probably make a custom ai later

            AIType = NPCID.EnchantedSword;
            AnimationType = NPCID.EnchantedSword; // Use vanilla zombie's type when executing animation code. Important to also match Main.npcFrameCount[NPC.type] in SetStaticDefaults.
        }
        public override void AI()
        {
            Timer++;
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient && Timer > 250 && Main.expertMode)
            {
                var source = NPC.GetSource_FromAI();
                Vector2 position = NPC.Center;
                Vector2 targetPosition = Main.player[NPC.target].Center;
                Vector2 direction = targetPosition - position;
                direction.Normalize();
                float speed = 5f;
                int type = ModContent.ProjectileType<TridentShotHostile>();
                int damage = 23;
                Projectile.NewProjectile(source, position, direction * speed, type, damage, 0f, Main.myPlayer);
                Timer = 0;
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