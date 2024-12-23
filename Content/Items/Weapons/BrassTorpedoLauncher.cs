using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Items.Fish;
using TheDeep.Content.Items.Material;
using TheDeep.Content.Items.Misc;
using TheDeep.Content.Projectiles;

namespace TheDeep.Content.Items.Weapons
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    public class BrassTorpedoLauncher : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 42; // The width of item hitbox
            Item.height = 30; // The height of item hitbox

            Item.autoReuse = true;  // Whether or not you can hold click to automatically use it again.
            Item.damage = 26; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.DamageType = DamageClass.Ranged; // What type of damage does this item affect?
            Item.knockBack = 4f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.
            Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.
            Item.shootSpeed = 10f; // The speed of the projectile (measured in pixels per frame.)
            Item.useAnimation = 35; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useTime = 35; // The item's use time in ticks (60 ticks == 1 second.)
            Item.UseSound = SoundID.Item11; // The sound that this item plays when used.
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, shoot, etc.)
            Item.value = Item.buyPrice(gold: 2); // The value of the weapon in copper coins

            // Custom ammo and shooting homing projectiles
            Item.shoot = ModContent.ProjectileType<TorpedoProjectile>();
            Item.useAmmo = ModContent.ItemType<Torpedo>(); // Restrict the type of ammo the weapon can use, so that the weapon cannot use other ammos
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<BrassBar>(8);
            recipe.AddIngredient<TorpedoLauncher>();
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8f, 2f); // Moves the position of the weapon in the player's hand.
        }
    }
}