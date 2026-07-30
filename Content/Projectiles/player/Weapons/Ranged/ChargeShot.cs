using Microsoft.Xna.Framework;
using TheDeep.Content.Dusts;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Common.SubPlayer;


namespace TheDeep.Content.Projectiles.player.Weapons.Ranged
{
    public class ChargeShot : ModProjectile
    {
        int DeathTimer;

        public override void SetDefaults()
        {
            Projectile.width = 16; 
            Projectile.height = 16; 
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.friendly = true; 
            Projectile.hostile = false; 
            Projectile.DamageType = DamageClass.Ranged; 
            Projectile.penetrate = -1; 
            Projectile.timeLeft = 600; 
            Projectile.light = 0.5f;
            Projectile.ignoreWater = true; 
            Projectile.tileCollide = true; 
            Projectile.extraUpdates = 1; 
            

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;

            AIType = ProjectileID.BulletHighVelocity;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Player owner = Main.player[Projectile.owner];

            owner.GetModPlayer<SubmergedModPlayer>().AddShake(2);

            DeathTimer = 25;
            Projectile.velocity *= 2;
        }

        public override bool PreDraw(ref Color LightColor)
        {
            Main.instance.LoadProjectile(79);

            Texture2D tex = ModContent.Request<Texture2D>(Texture).Value;
            Texture2D BulletTex = TextureAssets.Projectile[658].Value;

            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, LightColor, Projectile.rotation, tex.Size() / 2f, Projectile.scale, 0f, 0f);

            if (DeathTimer > 0)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(default, BlendState.Additive, default, default, default, null, Main.GameViewMatrix.TransformationMatrix);

                float fade = DeathTimer / 25f;

                Main.spriteBatch.Draw(BulletTex, Projectile.Center - Main.screenPosition, null,
                    new Color(79, 165, 222, 255) * fade, Projectile.rotation, BulletTex.Size() / 2f, 0.75f, 0f, 0f);

                Main.spriteBatch.End();
                Main.spriteBatch.Begin(default, default, default, default, default, null, Main.GameViewMatrix.TransformationMatrix);

            }
            return false;
        }
        

        public override void PostAI()
        {
            if (Main.rand.NextBool(10))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);
                Dust.NewDustPerfect(Projectile.Center + new Vector2(0, -5).RotatedBy(Projectile.rotation), ModContent.DustType<ChargeShotDust>(), Vector2.Zero, 0, default, 0.2f).noGravity = true;
            }
        }
    }
}