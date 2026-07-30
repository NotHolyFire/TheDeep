using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDeep.Content.Projectiles.player.Weapons.Ranged
{
    public class ShellshockerMk2Projectile : ModProjectile
    {
        public ref float HoldTimer => ref Projectile.ai[0];
        public ref float ShootTimer => ref Projectile.ai[1];

        public int ShootCount = 0;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 2;
            ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;

            DrawOffsetX = -15;
            DrawOriginOffsetY = -4;
        }
        public override bool? CanDamage() => false;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter);


            HoldTimer += 1f;
            int animationSpeed = Math.Min((int)HoldTimer / 40, 3);
            int initialShootDelay = 24;
            int shootDelayAdjustmentRate = 6;

            ShootTimer += 1f;
            bool shouldShootBullet = false;
            if (ShootTimer >= initialShootDelay - shootDelayAdjustmentRate * animationSpeed)
            {
                ShootTimer = 0f;
                shouldShootBullet = true;
            }


            Projectile.frameCounter += 1 + animationSpeed;
            if (Projectile.frameCounter >= 2)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = ++Projectile.frame % Main.projFrames[Type];
            }


            if (Projectile.soundDelay <= 0)
            {
                Projectile.soundDelay = initialShootDelay - shootDelayAdjustmentRate * animationSpeed;
                if (HoldTimer != 1f)
                {
                    SoundEngine.PlaySound(SoundID.Item11, Projectile.position);
                }
            }

            if (ShootTimer == 1f && HoldTimer != 1f)
            {
                Vector2 dustSpawnLocation = Projectile.Center + new Vector2(30, 0).RotatedBy(Projectile.rotation - (Projectile.direction == 1 ? 0 : MathHelper.Pi)) - new Vector2(8, 8);
                for (int i = 0; i < 2; i++)
                {
                    var dust = Dust.NewDustDirect(dustSpawnLocation, 16, 16, DustID.IceTorch, Projectile.velocity.X / 2f, Projectile.velocity.Y / 2f, 100);
                    dust.velocity *= 0.66f;
                    dust.noGravity = true;
                    dust.scale = 1.4f;
                }
            }

            if (shouldShootBullet && Main.myPlayer == Projectile.owner)
            {
                Item heldItem = player.HeldItem;
                if (player.channel && player.HasAmmo(heldItem) && !player.noItems && !player.CCed)
                {
                    float holdoutDistance = Items.Weapons.Ranged.Shellshocker.HoldoutDistance * Projectile.scale;
                    Vector2 holdoutOffset = holdoutDistance * Vector2.Normalize(Main.MouseWorld - playerCenter);
                    if (holdoutOffset.X != Projectile.velocity.X || holdoutOffset.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }

                    Projectile.velocity = holdoutOffset;

                    int projectileCount = 2;
                    if (ShootCount == 3)
                    {
                        projectileCount = 3;
                    }

                    for (int j = 0; j < projectileCount; j++)
                    {
                        var spawnLocation = playerCenter + holdoutOffset + Main.rand.NextVector2Circular(6, 6);
                        bool ammoConsumed = player.PickAmmo(heldItem, out int projToShoot, out float speed, out int damage, out float knockBack, out int usedAmmoItemId);

                        if (ammoConsumed)
                        {

                            if (ShootCount >= 0)
                            {
                                projToShoot = Main.rand.Next(new int[] { ModContent.ProjectileType<RedCoralShot>(), ModContent.ProjectileType<RedCoralShot>(), ModContent.ProjectileType<BlueCoralShot>(), ModContent.ProjectileType<OrangeCoralShot>(), ModContent.ProjectileType<PinkCoralShot>(), ModContent.ProjectileType<GreenCoralShot>(), ModContent.ProjectileType<RedCoralShot>() });
                            }
                            var source = player.GetSource_ItemUse_WithPotentialAmmo(heldItem, usedAmmoItemId);
                            Projectile.NewProjectile(source, spawnLocation, Vector2.Normalize(Projectile.velocity) * speed, projToShoot, damage, knockBack, Projectile.owner);
                        }
                    }
                    ShootCount = (ShootCount + 1) % 4;
                }
                else
                {
                    Projectile.Kill();
                }
            }

            Projectile.direction = Projectile.velocity.X < 0 ? -1 : 1;
            Projectile.spriteDirection = Projectile.direction;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.SetDummyItemTime(2);
            Projectile.Center = playerCenter;
            float rotationOffset = Projectile.spriteDirection == -1 ? MathHelper.Pi : 0;
            Projectile.rotation = Projectile.velocity.ToRotation() + rotationOffset;
            player.itemRotation = (Projectile.velocity * Projectile.direction).ToRotation();
            Projectile.timeLeft = 2;

        }
    }
}
