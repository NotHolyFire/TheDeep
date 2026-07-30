using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TheDeep.Common.GlobalNPCs;
using TheDeep.Common.SubPlayer;

namespace TheDeep.Content.Buffs.Debuff
{
    public class ParryFail : ModBuff
    {
        public const int DefenseReductionPercent = 50; // Isso aqui é % btw. Acho que vamo deixar -10% de defesa, tá funny
        public static float DefenseMultiplier = 1 - DefenseReductionPercent / 100f;

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ParryFailPlayer>().ParryFailDebuff = true;
            player.statDefense *= DefenseMultiplier;
        }

    }


    public class ParryFailPlayer : ModPlayer
    {
        public const int DamageReductionpercentage = 10;
        public static int DamageMultiplier = 1 - DamageReductionpercentage / 100;
        public bool ParryFailDebuff = false;

        public override void ResetEffects()
        {
            ParryFailDebuff = false;
        }
        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            item.damage *= DamageMultiplier; 
        }
    }
}