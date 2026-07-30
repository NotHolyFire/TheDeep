using Terraria.Enums;
using TheDeep.Content.Items.Weapons.Melee;
using Terraria.DataStructures;
using TheDeep.Content.Buffs;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using TheDeep.Content.Buffs.Buff;
using TheDeep.Content.Items.Weapons.Melee.Rapiers;

namespace TheDeep.Content.Projectiles.player.Weapons.Melee.Rapiers
{
    public class DarkSlashProj : ModProjectile
    {
        public const int fadeInDuration = 2;
        public const int fadeOutDuration = 1;
        public const int TotalDuration = 8;

        public Player Owner => Main.player[Projectile.owner];
        public bool CanHold => Owner.HeldItem.ModItem is DarkSlash && Owner.channel && !Owner.CCed && !Owner.noItems;
        public float collisionWidth => 10f * Projectile.scale;
        public Vector2 armPosition => Owner.RotatedRelativePoint(Owner.MountedCenter, true) + new Vector2(15f, 2f).RotatedBy(Projectile.rotation);

        public bool dying;


        		public int Timer {
			get => (int)Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(18);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.scale = 1f;
			Projectile.DamageType = DamageClass.Melee;
            Projectile.ownerHitCheck = true;
            Projectile.timeLeft = 360;
            Projectile.hide = true;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            
			Timer += 1;
			if (Timer >= TotalDuration) {
				Projectile.Kill();
				return;
			}
            else {
				player.heldProj = Projectile.whoAmI;
			}
            Projectile.Opacity = Utils.GetLerpValue(0f, fadeInDuration, Timer, clamped: true) * Utils.GetLerpValue(TotalDuration, TotalDuration - fadeOutDuration, Timer, clamped: true);

            Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: false, addGfxOffY: false);
			Projectile.Center = playerCenter + Projectile.velocity * (Timer - 1f);
            
			Projectile.spriteDirection = (Vector2.Dot(Projectile.velocity, Vector2.UnitX) >= 0f).ToDirectionInt();
            
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection;

            SetVisualOffsets();
            
            if (player.HasBuff(ModContent.BuffType<DarkSlashBuff>()))
                Projectile.damage = player.HeldItem.damage * 2;
        }
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.velocity *= 2f;
        }

        
        private void SetVisualOffsets()
        {
            const int HalfSpriteWidth = 52 / 2;
			const int HalfSpriteHeight = 52 / 2;

            int HalfProjWidth = Projectile.width / 2;
			int HalfProjHeight = Projectile.height / 2;

            DrawOriginOffsetX = 0;
			DrawOffsetX = -(HalfSpriteWidth - HalfProjWidth);
			DrawOriginOffsetY = -(HalfSpriteHeight - HalfProjHeight);
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override void CutTiles() 
        {
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			Vector2 start = Projectile.Center;
			Vector2 end = start + Projectile.velocity.SafeNormalize(-Vector2.UnitY) * 10f;
			Utils.PlotTileLine(start, end, collisionWidth, DelegateMethods.CutTiles);
		}
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) 
        {
			// "Hit anything between the player and the tip of the sword"
			// shootSpeed is 2.1f for reference, so this is basically plotting 12 pixels ahead from the center
			Vector2 start = Projectile.Center;
			Vector2 end = start + Projectile.velocity * 6f;
			float collisionPoint = 0f; // Don't need that variable, but required as parameter
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, collisionWidth, ref collisionPoint);
		}
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            if (player.HasBuff(ModContent.BuffType<DarkSlashBuff>()))
            {
                target.AddBuff(BuffID.ShadowFlame, 300);
                Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Shadowflame, -Projectile.velocity.X / 2, -Projectile.velocity.Y / 2, 0, default, 1f);
                
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            var texOutline = ModContent.Request<Texture2D>(Texture + "_Outline").Value;

            SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            Vector2 position = Projectile.Center - Main.screenPosition;

            float rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection;

            float fadeIn = 1f;

            if (Timer < 5f)
                fadeIn = Timer / 10f;
            else if (dying)
                fadeIn = Projectile.timeLeft / 20f;

            if (Projectile.timeLeft > 0 && Owner.HasBuff(ModContent.BuffType<DarkSlashBuff>()))
            {
                float opacity = MathHelper.Min(60, Projectile.timeLeft) / 60f;

                Main.spriteBatch.Draw(texOutline, position, null, new Color(102, 12, 212) * fadeIn, rotation, texOutline.Size() / 2f, Projectile.scale, spriteEffects, 0f);

            }
            return true;
        }
    }
}