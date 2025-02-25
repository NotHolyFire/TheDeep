using Microsoft.Xna.Framework;
using Mono.Cecil;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDeep.Content.Projectiles.Npc.CrabBoss
{
    public class ToxinBubble : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 16; //The width of projectile hitbox
            Projectile.height = 16; //The height of projectile hitbox
            Projectile.aiStyle = 0; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = false; // Can the projectile deal damage to enemies?
            Projectile.hostile = true; // Can the projectile deal damage to the player?
            Projectile.penetrate = 8; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 120; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f; // How much light emit around the projectile
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false; // Can the projectile collide with tiles?
            Projectile.extraUpdates = 1; // Set to above 0 if you want the projectile to update multiple time in a frame


            AIType = ProjectileID.BulletHighVelocity; // Act exactly like default Bullet
        }

        public override void PostAI()
        {

            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 5;
            }
        }

        public override void OnKill(int timeLeft)
        {
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.ToxicBubble);
            if (Main.expertMode == true)
            {
                if (Main.rand.NextBool(2))
                {
                    int type = ModContent.ProjectileType<ToxinPool>();
                    int damage = 5;
                    var source = Projectile.GetSource_FromAI();
                    Vector2 position = Projectile.Center;
                    Projectile.NewProjectile(source, position, new Vector2(0, 1), type, damage, -2f, Main.myPlayer);

                }
            }


        }

    }
    }