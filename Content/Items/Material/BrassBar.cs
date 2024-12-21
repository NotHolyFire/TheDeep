using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Items.Fish;
using TheDeep.Content.Items.Placeables;
using TheDeep.Content.Tiles;

namespace TheDeep.Content.Items.Material
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    public class BrassBar : ModItem
    {

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
            ItemID.Sets.SortingPriorityMaterials[Item.type] = 60; // Influences the inventory sort order. 59 is PlatinumBar, higher is more valuable.
        }

        public override void SetDefaults()
        {
            // ModContent.TileType returns the ID of the tile that this item should place when used. ModContent.TileType<T>() method returns an integer ID of the tile provided to it through its generic type argument (the type in angle brackets)
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(silver: 12);
            Item.DefaultToPlaceableTile(ModContent.TileType<BrassBarTile>());
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<ZincBar>(2);
            recipe.AddIngredient(ItemID.CopperBar, 2);
            recipe.AddTile<Industrial_forge>();
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient<ZincBar>(2);
            recipe2.AddIngredient(ItemID.TinBar, 2);
            recipe2.AddTile<Industrial_forge>();
            recipe2.Register();
        }
    }
    }