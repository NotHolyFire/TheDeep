using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Biomes.CaveHouse;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDeep.Content.Projectiles.Npc.CrabBoss
{
    public class  Shockwave : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 90;
            Projectile.height = 40;
            Projectile.aiStyle = 0;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
            Projectile.light = 0.5f;
            Projectile.alpha = 255;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.netImportant = true;


            AIType = ProjectileID.BulletHighVelocity;
        }

        public override void AI()
        {


            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 15; // Decrease alpha, increasing visibility.
            }

            Projectile.spriteDirection = Projectile.direction;

            if (Projectile.spriteDirection == 1) // facing right
            {
                DrawOffsetX = -30; // These values match the values in SetDefaults
                DrawOriginOffsetX = 31;
            }
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