

using Terraria.ID;
using Terraria.ModLoader;

namespace TheDeep.Content.Projectiles.Bobbers
{
    public class MagnetBobber : ModProjectile
    {
        public override string Texture => "TheDeep/Content/Items/Accessories/FishingMagnet";
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.scale = 0.75f;
            
            Projectile.aiStyle = ProjAIStyleID.Bobber;
            AIType = ProjectileID.FishingBobber;
            Projectile.bobber = true;
        }
    }
}