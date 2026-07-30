using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using TheDeep.Common.SubPlayer;

namespace TheDeep.Content.Buffs.Buff
{
    public class ParrySuccess : ModBuff
    {

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ParryHit>().Parry = true;
            player.GetDamage(DamageClass.Generic) += 4f;
        }




        }
}