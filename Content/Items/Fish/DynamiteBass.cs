using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDeep.Content.Items.Fish
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    public class DynamiteBass : ModItem
    {

        public override void SetStaticDefaults()
        {
            ItemID.Sets.CanBePlacedOnWeaponRacks[Type] = true; // All vanilla fish can be placed in a weapon rack.
            Item.ResearchUnlockCount = 30;
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<Content.Projectiles.DynamiteBassBoing>();
            Item.width = 8;
            Item.height = 28;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.value = Item.buyPrice(0, 0, 8, 0);
            Item.rare = ItemRarityID.Blue;
        }
        public override void AddRecipes()
        {

            Recipe recipe = Recipe.Create(ItemID.TNTBarrel);
            Recipe recipe1 = recipe.AddIngredient(ModContent.ItemType<Content.Items.Fish.DynamiteBass>());
            recipe.AddIngredient(ItemID.Barrel);
            recipe.Register();
        }
    }
}
