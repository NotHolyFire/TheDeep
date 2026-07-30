using Terraria.Audio;
using Terraria.GameContent;
using TheDeep.Content.Items.Weapons.Magic;
using Microsoft.Xna.Framework.Graphics;
using TheDeep.Content.Projectiles.player.Weapons.Magic;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using TheDeep.Content.Buffs;
using TheDeep.Content.Buffs.Debuff;

namespace TheDeep.Content.Projectiles.player.Weapons.Magic // A long time ago in a galaxy far far away...
{
    public class TidalWaveHeldProj : ModProjectile
    {
        public bool dying;
        public int _recoilTimer;
        public int Timer
        {
            get => (int) Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        public int useTime
        {
            get => (int)Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }
        public int animationTime
        {
            get => (int)Projectile.ai[2];
            set => Projectile.ai[2] = value;
        }
        public Player Owner => Main.player[Projectile.owner];
        public bool CanHold => Owner.HeldItem.ModItem is TidalWave && Owner.channel && !Owner.CCed && !Owner.noItems;
        public Vector2 armPosition => Owner.RotatedRelativePoint(Owner.MountedCenter, true) + new Vector2(15f, 2f).RotatedBy(Projectile.rotation) + armOffset;
        public Vector2 armOffset;
        public override bool? CanDamage() => false;
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }
        public override void AI()
        {

            if (!CanHold && !dying)
            {
                dying = true;
                Projectile.timeLeft = 10;
            }

            if (animationTime > 0)
                animationTime--;

            if (_recoilTimer > 0)
            {
                int offset = (int)MathHelper.Min(60, _recoilTimer);

                armOffset = new Vector2(-20f * offset/ 60f, 0f).RotatedBy(Projectile.rotation);

                _recoilTimer--;
            }

            if (Timer == 0f)
            {
                if (Main.myPlayer == Projectile.owner)
                    Projectile.velocity = Owner.DirectionTo(Main.MouseWorld);

                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
                Projectile.netUpdate = true;
                useTime = CombinedHooks.TotalUseTime(Owner.itemTime, Owner, Owner.HeldItem);
            }

            if (!dying)
            {
                if (Timer % useTime == 0)
                    animationTime = 100;

                UpdateHeldProjectile();

                Timer++;

                const int ticks = 25;
                if (animationTime > 0 && animationTime % ticks == 0)
                {
                    SpawnProjectiles();
                    Projectile.velocity.RotatedByRandom(0.5f);
                    _recoilTimer += Main.rand.Next(7, 15);
                }
            }
            else
            {
                UpdateHeldProjectile(false, false);
            }

        }

        public void SpawnProjectiles()
        {
            if (Owner.statMana < 5)
            {
                dying = true;
                Projectile.timeLeft = 5;
                return;
            }
            else
                Owner.statMana -= 5;
                
            SoundEngine.PlaySound(SoundID.Item66 with { PitchVariance = 0.3f, Volume = 0.9f }, Projectile.Center);
            for (int i = 0; i < 5; i++)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Projectile.velocity.RotatedByRandom(0.5f) * Main.rand.NextFloat(8f, 12f), ModContent.ProjectileType<TidalWaveWaterProjectile>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                
                Dust.NewDustPerfect(Projectile.Center + new Vector2(15f, 2f).RotatedBy(Projectile.rotation) + Main.rand.NextVector2Circular(5f, 5f), ModContent.DustType<TidalWaveDust>(),
                 Projectile.velocity.RotatedByRandom(0.5f) * Main.rand.NextFloat(8f, 12f), 0, default, Main.rand.NextFloat(0.7f, 1.1f));
            }

            for (int i = 0; i < 1 + Main.rand.Next(2); i++)
            {

                Dust.NewDustPerfect(Projectile.Center + new Vector2(15f, 2f).RotatedBy(Projectile.rotation) + Main.rand.NextVector2Circular(5f, 5f), ModContent.DustType<TidalWaveSparkle>(),
                 Projectile.velocity.RotatedByRandom(0.5f) * Main.rand.NextFloat(8f, 12f), 0, Color.Lerp(new Color(50, 107, 197), new Color(40, 97, 187), Main.rand.NextFloat()), Main.rand.NextFloat(0.7f, 1.1f)).customData = true;
                
            }
        }
        public void UpdateHeldProjectile(bool updateTimeLeft = true, bool updateVelocity = true)
        {
            Owner.ChangeDir(Projectile.direction);
            Owner.heldProj = Projectile.whoAmI;

            Owner.itemTime = 2;
            Owner.itemAnimation = 2;

            if (updateTimeLeft)
                Projectile.timeLeft = 2;

            Projectile.rotation = Projectile.velocity.ToRotation();
            Owner.itemRotation = Utils.ToRotation(Projectile.velocity * Projectile.direction);

            Owner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - MathHelper.ToRadians(80f));

            Projectile.position = armPosition - Projectile.Size * 0.5f;

            if (Main.myPlayer == Projectile.owner && updateVelocity)
            {
                Vector2 oldVelocity = Projectile.velocity;
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Owner.DirectionTo(Main.MouseWorld), 0.05f);

                if (Projectile.velocity != oldVelocity)
                {
                    Projectile.netSpam = 0;
                    Projectile.netUpdate = true;
                }
            }

            Projectile.spriteDirection = Projectile.direction;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            var tex = ModContent.Request<Texture2D>(Texture).Value;
            var texOutline = ModContent.Request<Texture2D>(Texture + "_Outline").Value;

            SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            Vector2 position = armPosition - Main.screenPosition;

            float rotation = Projectile.rotation + (spriteEffects == SpriteEffects.FlipHorizontally ? MathHelper.Pi : 0) + MathHelper.PiOver4 * Projectile.spriteDirection;

            float fadeIn = 1f;

            if (Timer < 5f)
                fadeIn = Timer / 10f;
            else if (dying)
                fadeIn = Projectile.timeLeft / 20f;

            Main.spriteBatch.Draw(tex, position, null, lightColor * fadeIn, rotation, tex.Size() / 2f, Projectile.scale, spriteEffects, 0f);

            if (_recoilTimer > 0)
            {
                float opacity = MathHelper.Min(60, _recoilTimer) / 60f;

                Main.spriteBatch.Draw(texOutline, position, null, new Color(102, 227, 238) * opacity, rotation, texOutline.Size() / 2f, Projectile.scale, spriteEffects, 0f);

            }
            return false;
        }
    }

    public class TidalWaveWaterProjectile : ModProjectile
    {
        
        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.friendly = true;

            Projectile.penetrate = 10;
            Projectile.timeLeft = 240;
            Projectile.tileCollide = false;
            Projectile.alpha = 75;

            Projectile.scale *= Main.rand.NextFloat(0.75f, 1.25f);
            Projectile.rotation = Main.rand.NextFloat(6.28f);

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;

            Projectile.frame = Main.rand.Next(3);

            Projectile.light = 0.1f;
        }

        public override void AI()
        {
            Projectile.velocity *= 1.01f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
    
            Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(15f, 15f) * Projectile.scale, DustID.Water, Main.rand.NextVector2Circular(1.5f, 1.5f), Main.rand.Next(50, 200), default, Main.rand.NextFloat(0.8f, 1.65f));
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDustPerfect(target.Center, DustID.AncientLight, Main.rand.NextVector2Circular(1.5f, 1.5f), Main.rand.Next(50, 200), default, Main.rand.NextFloat(0.8f, 1.65f)).noGravity = true;
            }

           target.AddBuff(ModContent.BuffType<Pressure>(), 300); 
        }
    }
    public class TidalWaveDust : ModDust
    {
        public override string Texture => "TheDeep/Content/Projectiles/player/Weapons/Magic/TidalWaveWaterProjectile_Old";
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.frame = new Rectangle(0, Main.rand.Next(2) * 64, 64, 64);
            dust.rotation = Main.rand.NextFloat(6.28f);
        }
        public override bool Update(Dust dust)
        {
            dust.velocity *= 0.95f;

            dust.scale += 0.02f;
            dust.alpha += 5;

            dust.position += dust.velocity;
            dust.rotation += dust.velocity.Length() * 0.02f;

            if (dust.alpha >= 255)
                dust.active = false;
                
            return false;
        }
    }
    public class TidalWaveSparkle : ModDust
    {
        public override string Texture => "TheDeep/Content/Assets/Textures/Invisible"; //when you can't even say, my name

        public override void OnSpawn(Dust dust)
        {
            dust.frame = new Rectangle(0, 0, 4, 0);
        }
        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.velocity *= 0.95f;

            if (dust.customData is not null && (bool)dust.customData)
                dust.rotation += dust.velocity.Length() * 0.05f;

            dust.scale *= 0.95f;

            if (dust.scale < 0.02f)
                dust.active = false;

            Lighting.AddLight(dust.position, dust.color.ToVector3() * 0.15f);

            return false;
        }
        public override bool PreDraw(Dust dust)
        {
            Color color = dust.color;
            float lerper = 1f - dust.alpha / 255f;

            Texture2D startex = TextureAssets.Projectile[79].Value;
            Texture2D bloomtex = TextureAssets.Projectile[540].Value;

            Main.spriteBatch.Draw(bloomtex, dust.position - Main.screenPosition, null, color * lerper * 0.25f, dust.rotation, bloomtex.Size() / 2f, dust.scale * 0.5f * lerper, 0f, 0f);

            Main.spriteBatch.Draw(startex, dust.position - Main.screenPosition, null, color * lerper, dust.rotation, startex.Size() / 2f, dust.scale * lerper, 0f, 0f);
            Main.spriteBatch.Draw(startex, dust.position - Main.screenPosition, null, Color.White with { A = 0 } * lerper, dust.rotation, startex.Size() / 2f, dust.scale * lerper, 0f, 0f);
            
            return false;
        }
    }
}