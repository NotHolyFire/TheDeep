
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using System;
using Terraria;
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
    public class Caranguinho : ModNPC
    {
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
            Main.npcFrameCount[Type] = 4;

            NPC.width = 30;
            NPC.height = 30;
            NPC.damage = 20;
            NPC.defense = 3;
            NPC.waterMovementSpeed = 1;
            NPC.lifeMax = 40;
            NPC.HitSound = SoundID.NPCHit31;
            NPC.DeathSound = SoundID.NPCDeath34;
            NPC.GravityIgnoresLiquid = true;    
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = 3;

            AIType = NPCID.WalkingAntlion;
            AnimationType = NPCID.Zombie;
            }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {

            npcLoot.Add(ItemDropRule.Common(ItemID.ShrimpPoBoy, 25));
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {

            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
				new FlavorTextBestiaryInfoElement("Testes muitos testez."),


            });
        }

        }
    }