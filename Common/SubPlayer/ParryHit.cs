using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using TheDeep.Content.Buffs.Buff;
namespace TheDeep.Common.SubPlayer
{
    public class ParryHit : ModPlayer
    {
        public bool Parry = false;
        private static Player player => Main.LocalPlayer;

        public override void ResetEffects()
        {
            Parry = false;
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Parry && item.DamageType.CountsAsClass<MeleeDamageClass>())
            {
                player.ClearBuff(ModContent.BuffType<ParrySuccess>()); // /effect clear @p thedeep:parry_success
            }


        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Parry && proj.DamageType.CountsAsClass<MeleeDamageClass>())
            {
                player.ClearBuff(ModContent.BuffType<ParrySuccess>()); // /effect clear @p thedeep:parry_success
            }
        }
    }
}