using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using TheDeep.Content.Buffs;
using TheDeep.Content.Projectiles.player.Weapons.Ranged;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Common.SubPlayer;
using TheDeep.Content.Buffs.Cooldown;

namespace TheDeep.Content.Items.Weapons.Ranged
{
    public class Piercer : ModItem
    {
        private static Asset<Texture2D> glowTexture;

        int ChargeShots = 6;
        public int comboExpireTimer = 0;

        public override void Load()
        {
            glowTexture = ModContent.Request<Texture2D>(Texture + "_Glow");
        }

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 30;

            Item.autoReuse = true;
            Item.damage = 45;
            Item.ArmorPenetration = 15;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4f; 
            Item.noMelee = true; 
            Item.rare = ItemRarityID.Orange; 
            Item.useAnimation = 20; 
            Item.useTime = 20; 

            Item.UseSound = (new SoundStyle($"{nameof(TheDeep)}/Content/Assets/Sounds/slab")
            {
                Volume = 0.9f,
                PitchVariance = 0.2f,
                MaxInstances = 3,
            });

            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot; 
            Item.value = Item.buyPrice(gold: 2);

            Item.scale = 0.75f;
            Item.shoot = ProjectileID.PurificationPowder; 
            Item.shootSpeed = 20f;
            Item.useAmmo = AmmoID.Bullet;
        }



        public override bool AltFunctionUse(Player player)
        {
            if (player.HasBuff(ModContent.BuffType<LowBattery>()))
            {
            return false;
            }
            else 
            return true;
        }

        //Essa e uma das coisas mais complexas q eu ja fiz - Azure 04/03/2026
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {


            if (type == ProjectileID.Bullet)
            {
                type = ProjectileID.NanoBullet;
            }

            if (player.altFunctionUse == 2 && ChargeShots == 0 && !player.HasBuff(ModContent.BuffType<LowBattery>()))
            {
                ChargeShots = 6;
            }

            if (player.altFunctionUse == 2 && ChargeShots >= 1  && !player.HasBuff(ModContent.BuffType<LowBattery>())) 
            {
                ChargeShots -= 1;
                type = ModContent.ProjectileType<ChargeShot>();
                Item.shootSpeed = 30f;
            }

            if (ChargeShots == 0 && player.altFunctionUse == 2 && type == ModContent.ProjectileType<ChargeShot>())
            {

                player.AddBuff(ModContent.BuffType<LowBattery>(), 360);
            }
             if (ChargeShots == 5 && player.altFunctionUse == 2) CombatText.NewText(player.Hitbox, new Color(100, 185, 242, 255), ChargeShots, true);
             if (ChargeShots <= 4 && ChargeShots != 0 && player.altFunctionUse == 2) CombatText.NewText(player.Hitbox, new Color(79, 165, 222, 255), ChargeShots, true);
             if (ChargeShots == 0 && player.altFunctionUse == 2) CombatText.NewText(player.Hitbox, Color.Red, ChargeShots, true);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1f, 0f);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = glowTexture.Value;
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            ); // funciona no chao mas n funciona na mão, e eu já sofri o suficiente pra tentar fazer funcionar - Azure
        }
    }
}