using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Items.Fish;
using TheDeep.Content.Items.Material;
using TheDeep.Content.Items.Material.Components;
using TheDeep.Content.Items.Misc;
using TheDeep.Content.Items.Placeables;
using TheDeep.Content.Projectiles;

namespace TheDeep.Content.Items.Weapons.Summoning
{
    public class RemoteControl : ModItem
    {

        public override void SetStaticDefaults()
        {
            Item.staff[Type] = true; // This makes the useStyle animate as a staff instead of as a gun.
        }
        public override void SetDefaults()
        {
            Item.width = 32; // The item texture's width.
            Item.height = 28; // The item texture's height.

            Item.useTime = 30; // The time span of using the weapon. Remember in terraria, 60 frames is a second.
            Item.useAnimation = 30; // The time span of the using animation of the weapon, suggest setting it the same as useTime.
            Item.mana = 8;
            Item.shootSpeed = 4;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.autoReuse = true; // Whether the weapon can be used more than once automatically by holding the use button.
            Item.DamageType = DamageClass.MagicSummonHybrid; // Whether your item is part of the melee class.
            Item.damage = 39; // The damage your item deals.
            Item.knockBack = 5; // The force of knockback of the weapon. Maximum is 20
            Item.shoot = ModContent.ProjectileType<Airstrike>();
            Item.value = Item.buyPrice(gold: 2); // The value of the weapon in copper coins.
            Item.rare = ItemRarityID.Green; // Give this iem our custom rarity.
            Item.UseSound = SoundID.MenuTick; // The sound when the weapon is being used.
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            float ceilingLimit = target.Y;
            if (ceilingLimit > player.Center.Y - 200f)
            {
                ceilingLimit = player.Center.Y - 200f;
            }
            // Loop these functions 3 times.
            for (int i = 0; i < 2; i++)
            {
                position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
                position.Y -= 100 * i;
                Vector2 heading = target - position;

                if (heading.Y < 0f)
                {
                    heading.Y *= -1f;
                }

                if (heading.Y < 20f)
                {
                    heading.Y = 20f;
                }

                heading.Normalize();
                heading *= velocity.Length();
                heading.Y += Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(source, position, heading, type, damage, knockback, player.whoAmI, 0f, ceilingLimit);
            }

            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddIngredient(ItemID.Wire, 6);
            recipe.AddIngredient(ItemID.Grenade, 13);
            recipe.AddIngredient<ZincBattery>();
            recipe.AddIngredient<CopperWire>(3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }
    }
}