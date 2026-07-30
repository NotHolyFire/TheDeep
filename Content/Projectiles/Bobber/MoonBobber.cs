using Humanizer;
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
    public class MoonBobber : ModProjectile
    {


        public override void SetDefaults()
        {
            // These are copied through the CloneDefaults method
            Projectile.CloneDefaults(ProjectileID.BobberWooden);
            Projectile.friendly = true;
            
            DrawOriginOffsetY = 1; // Adjusts the draw position
        }

        public override void AI()
        {
            Main.myPlayer = Projectile.owner;
        }

        // TODO: Sticking


    }
}