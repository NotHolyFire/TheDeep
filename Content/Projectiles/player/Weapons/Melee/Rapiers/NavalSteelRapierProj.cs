using Terraria.Enums;
using Terraria.DataStructures;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheDeep.Content.Projectiles.player.Weapons.Melee.Rapiers
{
    public class NavalSteelRapierProj : ModProjectile
    {
        public const int fadeInDuration = 2;
        public const int fadeOutDuration = 1;
        public const int TotalDuration = 8;
        
        public float collisionWidth => 10f * Projectile.scale;

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
        }
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.velocity *= 2f;
        }

        
        private void SetVisualOffsets()
        {
            const int HalfSpriteWidth = 46 / 2;
			const int HalfSpriteHeight = 46 / 2;

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
    }
}