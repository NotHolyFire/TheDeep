using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TheDeep.Common.GlobalNPCs;

namespace TheDeep.Content.Buffs.Weapon
{
    public class CoastalWhipDebuff : ModBuff
    {
        public static readonly int TagDamage = 5;

        public override void SetStaticDefaults()
        {
            BuffID.Sets.IsATagBuff[Type] = true;
        }

        public class CoastalWhipDebuffNPC : GlobalNPC
        {
            public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
            {
                // Only player attacks should benefit from this buff, hence the NPC and trap checks.
                if (projectile.npcProj || projectile.trap || !projectile.IsMinionOrSentryRelated)
                    return;


                // SummonTagDamageMultiplier scales down tag damage for some specific minion and sentry projectiles for balance purposes.
                var projTagMultiplier = ProjectileID.Sets.SummonTagDamageMultiplier[projectile.type];
                if (npc.HasBuff<CoastalWhipDebuff>())
                {
                    // Apply a flat bonus to every hit
                    modifiers.FlatBonusDamage += CoastalWhipDebuff.TagDamage * projTagMultiplier;
                }
            }
        }

    }
}