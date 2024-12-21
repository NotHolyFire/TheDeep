using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDeep.Content.Projectiles
{
    public class ChargeShot : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16; //The width of projectile hitbox
            Projectile.height = 16; //The height of projectile hitbox
            Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
            Projectile.penetrate = 8; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 600; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f; // How much light emit around the projectile
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true; // Can the projectile collide with tiles?
            Projectile.extraUpdates = 1; // Set to above 0 if you want the projectile to update multiple time in a frame

            Projectile.usesLocalNPCImmunity = true; // invincability per enimy if true if false every hit you do makes you unable to hit for a perioid of time iirc
            Projectile.localNPCHitCooldown = 0;

            AIType = ProjectileID.BulletHighVelocity; // Act exactly like default Bullet
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.velocity *= 2;
        }

        public override void PostAI()
        {
            if (Main.rand.NextBool(1))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric); // Makes the projectile emit dust.
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);
            }
        }
    }
}