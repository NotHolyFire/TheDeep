using MonoMod.Cil;
using System;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader.Utilities;
using Terraria.ModLoader;
using Terraria;
using TheDeep.Content.Items.Weapons.Ranged;
using Microsoft.Xna.Framework;

namespace TheDeep.Content.NPCs.Friendly.Critters
{
    public class SeaBunny : ModNPC
{
    private const int ClonedNPCID = NPCID.Frog; // bnuuy

    public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 3;

            Main.npcCatchable[Type] = true;
        NPCID.Sets.CountsAsCritter[Type] = true;
        NPCID.Sets.TownCritter[Type] = true;

        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Wet] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
    }

    public override void SetDefaults()
    {
        NPC.CloneDefaults(ClonedNPCID);

            NPC.catchItem = ModContent.ItemType<SeaBunnyItem>();
        AIType = ClonedNPCID;
        AnimationType = ClonedNPCID;
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
            bestiaryEntry.AddTags(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean);
         new FlavorTextBestiaryInfoElement("Mods.TheDeep.Bestiary.SeaBunny");
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
            return SpawnCondition.Ocean.Chance * 0.50f;
    }

    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life <= 0)
        {
            // Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>($"{Name}_Gore_Head").Type, NPC.scale);
            // Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>($"{Name}_Gore_Leg").Type, NPC.scale);
        }
    }

    public override void OnCaughtBy(Player player, Item item, bool failed)
    {
        if (failed)
        {
            return;
        }

            player.AddBuff(BuffID.Poisoned, 30);
        }
}

public class SeaBunnyItem : ModItem
{

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.Bunny);
        Item.makeNPC = ModContent.NPCType<SeaBunny>();
        Item.value += Item.buyPrice(0, 1, 0, 0);
        Item.rare = ItemRarityID.Blue;
    }
}
}