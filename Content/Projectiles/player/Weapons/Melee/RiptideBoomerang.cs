using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;
using Terraria.DataStructures;
using System;
using Microsoft.Xna.Framework;

namespace TheDeep.Content.Projectiles.player.Weapons.Melee
{
    public class RiptideBoomerang : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 5;
        }
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.penetrate = 6;
            Projectile.timeLeft = 240;
            Projectile.tileCollide = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
            Projectile.light = 1f;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Lighting.AddLight(Projectile.Center, 0, (255 - Projectile.alpha) * 0.7f / 255f, (255 - Projectile.alpha) / 255f);

            if (Main.rand.NextBool(3))
            {
                int dus = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 100, default, 0f);
                Main.dust[dus].position = Projectile.Center;
            }
            Vector2 playerCenter = owner.Center;
            float xDist = playerCenter.X - Projectile.Center.X;
            float yDist = playerCenter.Y - Projectile.Center.Y;
            float dist = (float)Math.Sqrt((double)(xDist * xDist + yDist * yDist));
            if (dist > 3000f)
            Projectile.Kill();


            if (++Projectile.frame >= Main.projFrames[Type])
                Projectile.frame = 0;


            if (++Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Type])
                    Projectile.frame = 0;
            }

        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 0);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Item120, Projectile.Center);
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
            }
            for (int i = 0; i < 10; i++)
            {
                Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 10f));

                Dust.NewDustPerfect(Projectile.Center, DustID.IceTorch, velocity * 5f, 0, default, 2f).noGravity = true;
            }
            return false;
        }
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.velocity *= 7;
            //Riptide.SuperAttackCharge += 1;
            SoundEngine.PlaySound(SoundID.Item120 with { Volume = 0.6f}, Projectile.Center);
        }
        public override void PostAI()
        {
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric); // Makes the projectile emit dust.
            }
        }

    }
    
}