using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TheDeep.Content.Buffs
{
    public class Sharpened : ModBuff
    {
        public static readonly int AtkBonus = 10;

        public override LocalizedText Description => base.Description.WithFormatArgs(AtkBonus);

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += AtkBonus / 100f;
        }
    }
}