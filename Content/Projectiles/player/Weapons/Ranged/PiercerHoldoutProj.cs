using TheDeep.Content.Items.Weapons.Ranged;
using Terraria.Audio;
using TheDeep.Common.SubPlayer;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;

namespace TheDeep.Content.Projectiles.player.Weapons.Ranged
{
    public class PiercerHoldoutProj : ModProjectile
    {
        
        public bool dying;
        public int _recoilTimer;
        public int Charges;
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
        public bool CanHold => Owner.HeldItem.ModItem is HoldoutPiercer && Owner.channel && !Owner.CCed && !Owner.noItems;
        public Vector2 armPosition => Owner.RotatedRelativePoint(Owner.MountedCenter, true) + new Vector2(25f, 0f).RotatedBy(Projectile.rotation) + armOffset;
        public Vector2 armOffset;
        public bool superShots = false;
        public override string Texture => "TheDeep/Content/Items/Weapons/Ranged/Piercer";
        public override bool? CanDamage() => false;
        public override void SetDefaults()
        {
            Projectile.width = 28; // ts needs to be the exact size of the sprite, Dont forget
            Projectile.height = 14;
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
                    if ( Charges <= 5 && superShots == false)
                    {
                        SpawnProjectiles();
                        Charges += 1;
                        Projectile.velocity.RotatedByRandom(0.5f); 
                        _recoilTimer += Main.rand.Next(7, 15);
                        if (Charges > 5 && superShots == false) // this shit is so fucking scuffed
                            superShots = true;
                    }
                    if (Charges > 0 && superShots == true)
                    {

                        SpawnChargedProjectiles();
                        Charges -= 1;
                        Projectile.velocity.RotatedByRandom(0.5f);
                        _recoilTimer += Main.rand.Next(9, 18);

                        
                    }
                    if (Charges == 0 && superShots == true) superShots = false;
                }
            }
            else
            {
                UpdateHeldProjectile(false, false);
            }
        
        }
        public void SpawnChargedProjectiles()
        {  
            Item heldItem = Owner.HeldItem;

            SoundEngine.PlaySound(new SoundStyle($"{nameof(TheDeep)}/Content/Assets/Sounds/slab"){ Volume = 0.9f, PitchVariance = 0.2f, MaxInstances = 3, });
            for (int i = 0; i < 1; i++)
            {
                bool ammoConsumed = Owner.PickAmmo(heldItem, out int projToShoot, out float speed, out int damage, out float knockBack, out int usedAmmoItemId);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Normalize(Projectile.velocity) * speed, ModContent.ProjectileType<ChargeShot>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
            
        }
        public void SpawnProjectiles()
        {
            
            Item heldItem = Owner.HeldItem;

            SoundEngine.PlaySound(SoundID.Item36 with { Volume = 0.9f, PitchVariance = 0.2f, MaxInstances = 3, }, Projectile.position);
            for (int i = 0; i < 1; i++)
            {
                bool ammoConsumed = Owner.PickAmmo(heldItem, out int projToShoot, out float speed, out int damage, out float knockBack, out int usedAmmoItemId);

                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Normalize(Projectile.velocity) * speed, ProjectileID.NanoBullet, Projectile.damage, Projectile.knockBack, Projectile.owner);
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
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Owner.DirectionTo(Main.MouseWorld), 0.3f);

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
            var texGlow = ModContent.Request<Texture2D>(Texture + "_Glow").Value;

            SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            Vector2 position = armPosition - Main.screenPosition;

            float rotation = Projectile.rotation + (spriteEffects == SpriteEffects.FlipHorizontally ? MathHelper.Pi : 0);

            float fadeIn = 1f;

            if (Timer < 5f)
                fadeIn = Timer / 10f;
            else if (dying)
                fadeIn = Projectile.timeLeft / 20f;

            Main.spriteBatch.Draw(tex, position, null, lightColor * fadeIn, rotation, tex.Size() / 2f, Projectile.scale, spriteEffects, 0f);

            Main.spriteBatch.Draw(texGlow, position, null, Color.White, rotation, texGlow.Size() / 2f, Projectile.scale, spriteEffects, 0f);


            return false;
        }
    } 
    public class PiercerParry : ModProjectile
    {
        #region Parry
        
        public bool dying;
        public int maxHeals = 3;
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
        public bool CanHold => Owner.HeldItem.ModItem is HoldoutPiercer && Owner.channel && !Owner.CCed && !Owner.noItems;
        public Vector2 armPosition => Owner.RotatedRelativePoint(Owner.MountedCenter, true) + new Vector2(25f, 0f).RotatedBy(Projectile.rotation) + armOffset;
        public Vector2 armOffset;
        public bool superShots = false;
        public override string Texture => "TheDeep/Content/Items/Weapons/Ranged/Piercer";
        public override void SetDefaults()
        {
            Projectile.width = 50; // ts needs to be the exact size of the sprite, Dont forget
            Projectile.height = 50;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;

        }

        public override void AI()
        {
             if (!CanHold && !dying)
            {
                dying = true;
                Projectile.timeLeft = 30;
            }

            if (animationTime > 0)
                animationTime--;

            Lighting.AddLight(Projectile.Center, Color.LightBlue.ToVector3());

            if (Projectile.soundDelay == 0)
            {
                Projectile.soundDelay = 10;
                SoundEngine.PlaySound(SoundID.Item60 with { Volume = 0.5f }, Projectile.position);
                SoundEngine.PlaySound(SoundID.Item71 with { Volume = 0.5f }, Projectile.position);
            }

            foreach (Projectile target in Main.ActiveProjectiles)
			{
				if (target.whoAmI == Projectile.whoAmI || !target.hostile)
					continue;

				if (target.damage > 10000 / 4 || Projectile.alpha > 0 || target.width + target.height > Projectile.width + Projectile.height)
					continue;

				if (target.velocity.Length() == 0 || !Projectile.Hitbox.Intersects(target.Hitbox) || target.alpha > 0)
					continue;
				
				SoundEngine.PlaySound(new SoundStyle($"{nameof(TheDeep)}/Content/Assets/Sounds/Parry")
            {
                Volume = 0.9f,
                PitchVariance = 0.2f,
                MaxInstances = 3,
            });
                
                
				for (int i = 0; i < 10; i++)
            	{
                	Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 10f));

                	Dust.NewDustPerfect(target.Center, DustID.IceTorch, velocity * 2f, 0, default, 3f).noGravity = true;
            	}	
				if (target.hostile || target.friendly)
				{
					target.hostile = false;
					target.friendly = true;
				}
				maxHeals -= 1;
				if (maxHeals >= 0)
				{
					Owner.Heal(5);
				}
				CombatText.NewText(Owner.Hitbox, Color.LightGreen, "+Parry", true, true);
				target.velocity.X = -target.velocity.X * 2f;
				target.velocity.Y = -target.velocity.Y * 2f;
				Owner.GetModPlayer<SubmergedModPlayer>().AddShake(10);
                //Owner.AddBuff(ModContent.BuffType<DarkSlashBuff>(), 600);
			}
			Owner.immune = true;
			Owner.immuneNoBlink = true;
			Owner.immuneTime = 20;

            if (Timer == 0f)
            {
                if (Main.myPlayer == Projectile.owner)
                    Projectile.velocity = Owner.DirectionTo(Main.MouseWorld);

                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
                Projectile.netUpdate = true;
                useTime = CombinedHooks.TotalUseTime(Owner.itemTime, Owner, Owner.HeldItem);
            }
            float dustRotation = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 0.7f;
            Vector2 dustPosition = Projectile.Center + dustRotation.ToRotationVector2() * 42f * Projectile.scale;
            Vector2 dustVelocity = (dustRotation + Projectile.ai[0] * MathHelper.PiOver2).ToRotationVector2();
            if (Main.rand.NextFloat() * 2f < Projectile.Opacity)
            {
                Color dustColor = Color.Lerp(Color.SkyBlue, Color.White, Main.rand.NextFloat() * 0.3f);
                Dust coloredDust = Dust.NewDustPerfect(Projectile.Center + dustRotation.ToRotationVector2() * (Main.rand.NextFloat() * 80f * Projectile.scale + 20f * Projectile.scale), DustID.FireworksRGB, dustVelocity * 1f, 100, dustColor, 0.4f);
                coloredDust.fadeIn = 0.4f + Main.rand.NextFloat() * 0.15f;
                coloredDust.noGravity = true;
            }

            if (Main.rand.NextFloat() * 1.5f < Projectile.Opacity)
            {
                Dust.NewDustPerfect(dustPosition, DustID.TintableDustLighted, dustVelocity, 100, Color.SkyBlue * Projectile.Opacity, 1.2f * Projectile.Opacity);
            }

            if (!dying)
            {
                if (Timer % useTime == 0)
                    animationTime = 100;

                UpdateHeldProjectile();

                Timer++;

                /*const int ticks = 25;
                if (animationTime > 0 && animationTime % ticks == 0)
                {
                    if ( Charges <= 5 && superShots == false)
                    {
                        SpawnProjectiles();
                        Charges += 1;
                        Projectile.velocity.RotatedByRandom(0.5f); 
                        _recoilTimer += Main.rand.Next(7, 15);
                        if (Charges > 5 && superShots == false) // this shit is so fucking scuffed
                            superShots = true;
                    }
                    if (Charges > 0 && superShots == true)
                    {

                        SpawnChargedProjectiles();
                        Charges -= 1;
                        Projectile.velocity.RotatedByRandom(0.5f);
                        _recoilTimer += Main.rand.Next(9, 18);

                        
                    }
                    if (Charges == 0 && superShots == true) superShots = false;
                }*/

            }
            else
            {
                UpdateHeldProjectile(false, false);
            }
        
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			
				SoundEngine.PlaySound(new SoundStyle($"{nameof(TheDeep)}/Content/Assets/Sounds/Parry")
            {
                Volume = 0.9f,
                PitchVariance = 0.2f,
                MaxInstances = 3,
            });
				for (int i = 0; i < 10; i++)
            	{
                	Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 10f));

                	Dust.NewDustPerfect(target.Center, DustID.IceTorch, velocity * 2f, 0, default, 3f).noGravity = true;
            	}
				maxHeals -= 1;
				if (maxHeals >= 0)
				{
					Owner.Heal(5);
				}	
				CombatText.NewText(Owner.Hitbox, Color.LightGreen, "+Parry", true, true);
				Owner.GetModPlayer<SubmergedModPlayer>().AddShake(10);
				//Owner.AddBuff(ModContent.BuffType<DarkSlashBuff>(), 600);
        }
        public void UpdateHeldProjectile(bool updateTimeLeft = true, bool updateVelocity = true)
        {
            Owner.ChangeDir(Projectile.direction);
            Owner.heldProj = Projectile.whoAmI;

            Owner.itemTime = 2;
            Owner.itemAnimation = 2;

            if (updateTimeLeft)
                Projectile.timeLeft = 2;

            Projectile.rotation += 0.7f * Owner.direction; //Projectile.velocity.ToRotation();
            Owner.itemRotation = Utils.ToRotation(Projectile.velocity * Projectile.direction);

            Owner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - MathHelper.ToRadians(80f));

            Projectile.position = armPosition - Projectile.Size * 0.5f;

            if (Main.myPlayer == Projectile.owner && updateVelocity)
            {
                Vector2 oldVelocity = Projectile.velocity;
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Owner.DirectionTo(Main.MouseWorld), 0.3f);

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
            var texGlow = ModContent.Request<Texture2D>("TheDeep/Content/Assets/Textures/Swoon").Value;

            SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            Rectangle sourceRectangle = texGlow.Frame(1, 4);
            Vector2 position = Owner.MountedCenter - Main.screenPosition;

            float rotation = Projectile.rotation + (spriteEffects == SpriteEffects.FlipHorizontally ? MathHelper.Pi : 0);


            Main.spriteBatch.Draw(tex, position, null, lightColor, rotation, tex.Size() / 2f, Projectile.scale, spriteEffects, 0f);

            Main.spriteBatch.Draw(texGlow, position, null, new Color(127, 197, 238), rotation + MathHelper.ToRadians(90), texGlow.Size() / 2f, Projectile.scale / 2f, spriteEffects, 0f);
            //Main.spriteBatch.Draw(texGlow, position, null, new Color(129, 138, 155), rotation + MathHelper.ToRadians(90), texGlow.Size() / 1.9f, Projectile.scale / 2f, spriteEffects, 0f);


            return false;
        }
        #endregion
    } 
}