using TheDeep.Common.GlobalNPCs;
using TheDeep.Common.SubPlayer;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;


namespace TheDeep.Content.Buffs.Debuff
{
    public class Laceration : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<DOTPlayer>().LacerationDOT = true;
            if (Main.rand.NextBool(10))
                Dust.NewDust(player.position, player.width, player.height, DustID.Blood);
 
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<DOTNPC>().LacerationDOTNpc = true;
            if (Main.rand.NextBool(10))
                Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood);
        }
    }
}