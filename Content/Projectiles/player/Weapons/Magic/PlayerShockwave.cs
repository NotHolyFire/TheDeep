using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Biomes.CaveHouse;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDeep.Content.Projectiles.player.Weapons.Magic
{
    public class  PlayerShockwave : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 90;
            Projectile.height = 40;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
            Projectile.light = 0.5f;
            Projectile.alpha = 255;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.netImportant = true;

            Projectile.usesLocalNPCImmunity = true; // invincability per enimy if true if false every hit you do makes you unable to hit for a perioid of time iirc
            Projectile.localNPCHitCooldown = 10;

            AIType = ProjectileID.SpikyBall;
        }

        public override void AI()
        {
            if (Projectile.velocity.Y == 0)
            {
                Projectile.velocity.Y += 5;
            }

            if (Projectile.velocity.X == 0)
            {
                Projectile.Kill();
            }

            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 5; // Decrease alpha, increasing visibility.
            }

            Projectile.spriteDirection = Projectile.direction;

            if (Projectile.spriteDirection == 1) // facing right
            {
                DrawOffsetX = -30; // These values match the values in SetDefaults
                DrawOriginOffsetX = 31;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;

            return true;
        }

        public override void PostAI()
        {
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Sand); // mm dust
            }
        }
        }
    }