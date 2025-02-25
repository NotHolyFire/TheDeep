using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using TheDeep.Common.GlobalNPCs;
using TheDeep.Common.Players;

namespace TheDeep.Content.Buffs.Debuff
{
    public class Stunned : ModBuff
    {
        public const int DefenseReductionPercent = 10; // Isso aqui é % btw. Acho que vamo deixar -10% de defesa, tá funny
        public static float DefenseMultiplier = 1 - DefenseReductionPercent / 100f;

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true; // This buff can be applied by other players in PvP, so we need this to be true.
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<DamageModificationGlobalNPC>().StunDebuff = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<PlayerDamageModification>().StunDebuff = true;
            player.statDefense *= DefenseMultiplier;
        }

    }
}