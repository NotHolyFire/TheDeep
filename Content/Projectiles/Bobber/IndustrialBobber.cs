using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDeep.Content.Projectiles.Bobber
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    public class IndustrialBobber : ModProjectile
    {
        public override void SetDefaults()
        {
            // These are copied through the CloneDefaults method
            // Projectile.width = 14;
            // Projectile.height = 14;
            // Projectile.aiStyle = 61;
            // Projectile.bobber = true;
            // Projectile.penetrate = -1;
            // Projectile.netImportant = true;
            Projectile.CloneDefaults(ProjectileID.BobberWooden);

            DrawOriginOffsetY = -8; // Adjusts the draw position
        }

    }
}