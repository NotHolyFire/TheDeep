using Microsoft.Xna.Framework;
using Mono.Cecil;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Buffs;
using TheDeep.Content.Items.Fish;
using TheDeep.Content.Items.Material;
using TheDeep.Content.Items.Misc;
using TheDeep.Content.Projectiles;

namespace TheDeep.Content.Items.Weapons
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    public class Piercer : ModItem
    {
		public override void SetDefaults()
        {
            Item.width = 42; // The width of item hitbox
            Item.height = 30; // The height of item hitbox

            Item.autoReuse = true;  // Whether or not you can hold click to automatically use it again.
            Item.damage = 45; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.ArmorPenetration = 15;
            Item.DamageType = DamageClass.Ranged; // What type of damage does this item affect?
            Item.knockBack = 4f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.
            Item.rare = ItemRarityID.Orange; // The color that the item's name will be in-game
            Item.useAnimation = 50; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useTime = 50; // The item's use time in ticks (60 ticks == 1 second.)

            Item.UseSound = new SoundStyle($"{nameof(TheDeep)}/Content/Assets/Sounds/slab")
            {
                Volume = 0.9f,
                PitchVariance = 0.2f,
                MaxInstances = 3,
            };

            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, shoot, etc.)
            Item.value = Item.buyPrice(gold: 2); // The value of the weapon in copper coins

            Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
            Item.shootSpeed = 10f; // The speed of the projectile (measured in pixels per frame.) This value equivalent to Handgun
            Item.useAmmo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bone, 30);
            recipe.AddIngredient(ItemID.Wire, 8);
            recipe.AddIngredient(ItemID.Diamond, 3);
            recipe.AddIngredient(ItemID.Handgun);
            recipe.AddIngredient<NavalSteelBar>(12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            // Every projectile shot from this gun has a 1/3 chance of being an ExampleInstancedProjectile
            if (type == ProjectileID.Bullet)
            {
                type = ProjectileID.BulletHighVelocity;
            }

            if (player.altFunctionUse == 2 && !player.HasBuff(ModContent.BuffType<LowBattery>()))
            {
                player.AddBuff(ModContent.BuffType<LowBattery>(), 360);
                type = ModContent.ProjectileType<ChargeShot>();
            }

        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1f, 0f); // Moves the position of the weapon in the player's hand.
        }
    }
}
