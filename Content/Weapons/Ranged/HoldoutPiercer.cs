using TheDeep.Content.Projectiles.player.Weapons.Ranged;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Microsoft.Xna.Framework;


namespace TheDeep.Content.Items.Weapons.Ranged
{
    public class HoldoutPiercer : ModItem
    {
        public override string Texture => "TheDeep/Content/Items/Weapons/Ranged/Piercer";
        
        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[Item.shoot] <= 0;
        
        private static Asset<Texture2D> glowTexture;
        public const int HoldoutDistance = 20;

        public override void Load()
        {
            glowTexture = ModContent.Request<Texture2D>("TheDeep/Content/Items/Weapons/Ranged/Piercer_Glow");
        }

       

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 32;
            Item.damage = 45;
            Item.ArmorPenetration = 15;
            Item.knockBack = 10f;
            Item.rare = ItemRarityID.Orange;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.value = Item.buyPrice(gold: 2);

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<PiercerHoldoutProj>();
            Item.noMelee = true;

            Item.shootSpeed = 20f;
            Item.useAmmo = AmmoID.Bullet;
            Item.noUseGraphic = true;
            Item.channel = true;
        }

                public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bone, 30);
            recipe.AddIngredient(ItemID.Wire, 8);
            recipe.AddIngredient(ItemID.Handgun);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {   
            if (player.altFunctionUse == 0)
            {
                type = ModContent.ProjectileType<PiercerHoldoutProj>();
                velocity = Vector2.Normalize(velocity) * HoldoutDistance;

                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer);
            }
            if (player.altFunctionUse == 2)
            {
                type = ModContent.ProjectileType<PiercerParry>();
                velocity = Vector2.Normalize(velocity) * HoldoutDistance;

                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer);
            }
            return false;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            // This prevents the item from consuming ammo when initially used. The projectile will "spin up" and then it will consume ammo instead.
            if (player.ItemTimeIsZero)
            {
                return false;
            }
            return true;
        }
        
        public override bool AltFunctionUse(Player player)  
        {
            return true;
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
            );
        }
    }
}