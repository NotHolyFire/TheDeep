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
    public class ABobberMostIdeal : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = 61;
            Projectile.friendly = true;
            Projectile.bobber = true;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;

            DrawOriginOffsetY = -8; // Adjusts the draw position
        }

    }
}