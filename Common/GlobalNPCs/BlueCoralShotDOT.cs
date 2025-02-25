using TheDeep.Content.Projectiles;
using Terraria;
using Terraria.ModLoader;
using TheDeep.Content.Projectiles.player.Weapons.Ranged;

namespace TheDeep.Common.GlobalNPCs
{
    internal class BlueCoralShotDOT : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool BlueCoralShotDebuff;

        public override void ResetEffects(NPC npc)
        {
            BlueCoralShotDebuff = false;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (BlueCoralShotDebuff)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                // Count how many ExampleJavelinProjectile are attached to this npc.
                int harpoonCount = 0;
                foreach (var p in Main.ActiveProjectiles)
                {
                    if (p.type == ModContent.ProjectileType<BlueCoralShot>() && p.ai[0] == 1f && p.ai[1] == npc.whoAmI)
                    {
                        harpoonCount++;
                    }
                }
                // Remember, lifeRegen affects the actual life loss, damage is just the text.
                // The logic shown here matches how vanilla debuffs stack in terms of damage numbers shown and actual life loss.
                npc.lifeRegen -= harpoonCount * 2 * 3;
                if (damage < harpoonCount * 3)
                {
                    damage = harpoonCount * 3;
                }
            }

        }

    }
}