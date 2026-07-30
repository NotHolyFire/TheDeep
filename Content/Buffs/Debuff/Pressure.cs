using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Common.GlobalNPCs;
using TheDeep.Common.SubPlayer;



namespace TheDeep.Content.Buffs.Debuff
{
    public class Pressure : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<DOTPlayer>().PressureDOT = true;
            Dust.NewDust(player.position, player.width, player.height, DustID.Water);
 
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<DOTNPC>().PressureDOTNpc = true;
            Dust.NewDust(npc.position, npc.width, npc.height, DustID.Water);
        }
    }

    
}