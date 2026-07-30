using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Buffs.Weapon;

namespace TheDeep.Content.Projectiles.player.Weapons.Ranged
{
    public class BlueCoralShot : ModProjectile
    {

        public bool IsStickingToTarget
        {
            get => Projectile.ai[0] == 1f;
            set => Projectile.ai[0] = value ? 1f : 0f;
        }

        // Index of the current target
        public int TargetWhoAmI
        {
            get => (int)Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        public float StickTimer
        {
            get => Projectile.localAI[0];
            set => Projectile.localAI[0] = value;
        }

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.DontAttachHideToAlpha[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 16; //The width of projectile hitbox
            Projectile.height = 16; //The height of projectile hitbox
            Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
            Projectile.penetrate = 2; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 600; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true; // Can the projectile collide with tiles?
            Projectile.extraUpdates = 1; // Set to above 0 if you want the projectile to update multiple time in a frame

            AIType = ProjectileID.Bullet;
        }

        public override void AI()
        {
            UpdateAlpha();
            // Run either the Sticky AI or Normal AI
            // Separating into different methods helps keeps your AI clean
            if (IsStickingToTarget)
            {
                StickyAI();
            }
            else
            {
                AIType = ProjectileID.Bullet;
            }
        }

        private const int StickTime = 60 * 15; // 15 seconds
        private void StickyAI()
        {
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            StickTimer += 1f;

            // Every 30 ticks, the javelin will perform a hit effect
            bool hitEffect = StickTimer % 30f == 0f;
            int npcTarget = TargetWhoAmI;
            if (StickTimer >= StickTime || npcTarget < 0 || npcTarget >= 200)
            { // If the index is past its limits, kill it
                Projectile.Kill();
            }
            else if (Main.npc[npcTarget].active && !Main.npc[npcTarget].dontTakeDamage)
            {
                // If the target is active and can take damage
                // Set the projectile's position relative to the target's center
                Projectile.Center = Main.npc[npcTarget].Center - Projectile.velocity * 2f;
                Projectile.gfxOffY = Main.npc[npcTarget].gfxOffY;
                if (hitEffect)
                {
                    // Perform a hit effect here, causing the npc to react as if hit.
                    // Note that this does NOT damage the NPC, the damage is done through the debuff.
                    Main.npc[npcTarget].HitEffect(0, 1.0);
                }
            }
            else
            { // Otherwise, kill the projectile
                Projectile.Kill();
            }
        }

        private const int MaxStickingJavelin = 10; // This is the max amount of javelins able to be attached to a single NPC
        private readonly Point[] stickingJavelins = new Point[MaxStickingJavelin]; // The point array holding for sticking javelins

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            IsStickingToTarget = true; // we are sticking to a target
            TargetWhoAmI = target.whoAmI; // Set the target whoAmI
            Projectile.velocity = (target.Center - Projectile.Center) *
                0.75f; // Change velocity based on delta center of targets (difference between entity centers)
            Projectile.netUpdate = true; // netUpdate this javelin
            Projectile.damage = 0; // Makes sure the sticking javelins do not deal damage anymore

            // HarpoonStuck handles the damage over time (DoT)
            target.AddBuff(ModContent.BuffType<BlueCoralShotDebuff>(), 900);

            // KillOldestJavelin will kill the oldest projectile stuck to the specified npc.
            // It only works if ai[0] is 1 when sticking and ai[1] is the target npc index, which is what IsStickingToTarget and TargetWhoAmI correspond to.
            Projectile.KillOldestJavelin(Projectile.whoAmI, Type, target.whoAmI, stickingJavelins);
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            // For going through platforms and such, javelins use a tad smaller size
            width = height = 10; // notice we set the width to the height, the height to 10. so both are 10
            return true;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            // By shrinking target hitboxes by a small amount, this projectile only hits if it more directly hits the target.
            // This helps the javelin stick in a visually appealing place within the target sprite.
            if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
            {
                targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
            }
            // Return if the hitboxes intersects, which means the javelin collides or not
            return projHitbox.Intersects(targetHitbox);
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            // If attached to an NPC, draw behind tiles (and the npc) if that NPC is behind tiles, otherwise just behind the NPC.
            if (IsStickingToTarget)
            {
                int npcIndex = TargetWhoAmI;
                if (npcIndex >= 0 && npcIndex < 200 && Main.npc[npcIndex].active)
                {
                    if (Main.npc[npcIndex].behindTiles)
                    {
                        behindNPCsAndTiles.Add(index);
                    }
                    else
                    {
                        behindNPCsAndTiles.Add(index);
                    }

                    return;
                }
            }
            // Since we aren't attached, add to this list
            behindNPCsAndTiles.Add(index);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCHit3, Projectile.position);
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.BlueCrystalShard);
        }

        // Change this number if you want to alter how the alpha changes
        private const int AlphaFadeInSpeed = 25;

        private void UpdateAlpha()
        {
            // Slowly remove alpha as it is present
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= AlphaFadeInSpeed;
            }

            // If alpha gets lower than 0, set it to 0
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
        }
    }
}