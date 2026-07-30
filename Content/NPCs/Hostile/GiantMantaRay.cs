using TheDeep.Content.Buffs;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.ItemDropRules;
using TheDeep.Content.Items.Placeables;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Items.Weapons.Magic;
using TheDeep.Content.Buffs.Debuff;
using Microsoft.Xna.Framework;

namespace TheDeep.Content.NPCs.Hostile
{
    public class GiantMantaRay : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 6;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers();

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<Pressure>()] = true;
        }
        public override void SetDefaults()
        {
            NPC.noGravity = true;

            NPC.width = 140;
            NPC.height = 42;

            NPC.damage = 30;
            NPC.defense = 8;
            NPC.lifeMax = 340;
            NPC.knockBackResist = 0.3f;

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;

            NPC.value = 600f;
            NPC.aiStyle = NPCAIStyleID.Piranha;

            NPC.scale = Main.rand.NextFloat(0.75f ,1f);
            
            Banner = Type;
            BannerItem = ModContent.ItemType<GiantMantaRayBanner>();

            AIType = NPCID.Shark;
            AnimationType = NPCID.Shark;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.dayTime)
                return 0f;

            return SpawnCondition.Ocean.Chance * 0.2f;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
                new FlavorTextBestiaryInfoElement("Mods.SubmergedMod.Bestiary.GiantMantaRay")
            });
        }
        public override void ModifyNPCLoot(NPCLoot loot)
        {
            loot.Add(ItemDropRule.Common(ModContent.ItemType<TidalWave>(), 90, 1));
            loot.Add(ItemDropRule.Common(ItemID.Flipper, 1000));
            loot.Add(ItemDropRule.Common(ItemID.DivingHelmet, 1000));
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f, 0, Color.LightBlue, 1f);
            }
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 25; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f, 0, Color.LightBlue, 1f);
                }
                if (NPC.life <= 0)
            {
                for (int k = 0; k < 25; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f, 0, default, 1f);
                }
                if (Main.netMode != NetmodeID.Server)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GiantMantaRayGore_1").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GiantMantaRayGore_2").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GiantMantaRayGore_3").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GiantMantaRayGore_3").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GiantMantaRayGore_4").Type, 1f);
                }
            }

            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            int buffType = ModContent.BuffType<Pressure>();
            int timeToAdd = 5 * 60;
            target.AddBuff(buffType, timeToAdd);
        }
    }
}