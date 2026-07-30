using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Projectiles.Npc.CrabBoss;

namespace TheDeep.Content.Projectiles.player.Weapons.Magic
{
    public class SandBoulder : ModProjectile
    {
        public int GravityDelayTimer
        {
            get => (int)Projectile.ai[2];
            set => Projectile.ai[2] = value;
        }
        public override void SetDefaults()
        {


            Projectile.width = 40; //The width of projectile hitbox
            Projectile.height = 40; //The height of projectile hitbox
            Projectile.aiStyle = 0; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Melee; // Is the projectile shoot by a ranged weapon?
            Projectile.penetrate = 1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 240; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.light = 0.5f; // How much light emit around the projectile
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true; // Can the projectile collide with tiles?
            Projectile.extraUpdates = 1; // Set to above 0 if you want the projectile to update multiple time in a frame

            AIType = ProjectileID.Bullet; // Act exactly like default Bullet
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;

            DrawOffsetX = -5;
            DrawOriginOffsetY = -4;
        }

        private const int GravityDelay = 30;

        public override void AI()
        {
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] > 10f)
            {
                Projectile.ai[0] = 10f;
                // Roll speed dampening. 
                if (Projectile.velocity.Y == 0f && Projectile.velocity.X != 0f)
                {
                    Projectile.velocity.X = Projectile.velocity.X * 0.96f;

                    if (Projectile.velocity.X > -0.01 && Projectile.velocity.X < 0.01)
                    {
                        Projectile.velocity.X = 0f;
                        Projectile.netUpdate = true;
                    }
                }
                // Delayed gravity
                Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
            }

            Projectile.rotation += Projectile.velocity.X * 0.1f;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item70);
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Sand);
            int damage = Projectile.damage /2;
            int type = ModContent.ProjectileType<PlayerShockwave>();
            var source = Projectile.GetSource_FromAI();
            Vector2 position = Projectile.Center;
            Projectile.NewProjectile(source, position, new Vector2(-8, 5), type, damage, -2f, Main.myPlayer);
            Projectile.NewProjectile(source, position, new Vector2(8, 5), type, damage, -2f, Main.myPlayer);
        }

        public override void PostAI()
        {
            if (Main.rand.NextBool(9))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Sand); // Makes the projectile emit dust.
            }
        }
    }
}