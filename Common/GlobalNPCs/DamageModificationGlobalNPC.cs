using TheDeep.Content.Projectiles;
using TheDeep.Content.Buffs;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace TheDeep.Common.GlobalNPCs
{
    internal class DamageModificationGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool StunDebuff;

        public override void ResetEffects(NPC npc)
        {
            StunDebuff = false;
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (StunDebuff)
            {
                // For best results, defense debuffs should be multiplicative
                modifiers.Defense *= Stunned.DefenseMultiplier;
            }
        }

        public override void AI(NPC npc)
        {
            if (Main.rand.NextBool(8) && StunDebuff)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, DustID.GemAmber); // erm wattesigka
            }
        }
    }
}