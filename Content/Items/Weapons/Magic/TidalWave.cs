using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Items.Fish;
using TheDeep.Content.Items.Material;
using TheDeep.Content.Items.Misc;
using TheDeep.Content.Items.Placeables;
using TheDeep.Content.Projectiles;

namespace TheDeep.Content.Items.Weapons.Magic
{
    public class TidalWave : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40; // The item texture's width.
            Item.height = 40; // The item texture's height.

            Item.useStyle = ItemUseStyleID.Shoot; // The useStyle of the Item.
            Item.useTime = 20; // The time span of using the weapon. Remember in terraria, 60 frames is a second.
            Item.useAnimation = 20; // The time span of the using animation of the weapon, suggest setting it the same as useTime.
            Item.mana = 14;
            Item.shootSpeed = 3;
            Item.noMelee = true;
            Item.autoReuse = true; // Whether the weapon can be used more than once automatically by holding the use button.
            Item.DamageType = DamageClass.Magic; // Whether your item is part of the melee class.
            Item.damage = 9; // The damage your item deals.
            Item.ArmorPenetration = 5;
            Item.knockBack = 5; // The force of knockback of the weapon. Maximum is 20
            Item.crit = 4; // The critical strike chance the weapon has. The player, by default, has a 4% critical strike chance.
            Item.shoot = ModContent.ProjectileType<TidesCoral>();
            Item.value = Item.buyPrice(gold: 4); // The value of the weapon in copper coins.
            Item.rare = ItemRarityID.Green; // Give this iem our custom rarity.
            Item.UseSound = SoundID.Item21; // The sound when the weapon is being used.
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            const int NumProjectiles = 5; // The number of projectiles that this gun will shoot.

            for (int i = 0; i < NumProjectiles; i++)
            {
                // Rotate the velocity randomly by 30 degrees at max.
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15));

                // Decrease velocity randomly for nicer visuals.
                newVelocity *= 1.2f - Main.rand.NextFloat(0.3f);

                // Create a projectile.
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }

            int type2 = ModContent.ProjectileType<TideInABall>();

            Projectile.NewProjectileDirect(source, position, velocity, type2, damage, knockback, player.whoAmI);

            return false; // return false to stop vanilla from calling Projectile.NewProjectile.


        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2f, -2f);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Coral, 7);
            recipe.AddIngredient(ItemID.WaterCandle, 1);
            recipe.AddIngredient(ItemID.WaterBolt, 2);
            recipe.AddTile(TileID.Bookcases);
            recipe.Register();
        }
    }
}