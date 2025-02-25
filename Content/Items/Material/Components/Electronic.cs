using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDeep.Content.Items.Material.Components
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    public class Electronic : ModItem
    {

        public override void SetDefaults()
        {
            // ModContent.TileType returns the ID of the tile that this item should place when used. ModContent.TileType<T>() method returns an integer ID of the tile provided to it through its generic type argument (the type in angle brackets)
            Item.width = 30;
            Item.height = 30;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.buyPrice(silver: 20);
        }
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
            ItemID.Sets.SortingPriorityMaterials[Item.type] = 99; // Influences the inventory sort order. 59 is PlatinumBar, higher is more valuable.
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<CopperWire>(3);
            recipe.AddIngredient<Plastic>(3);
            recipe.AddIngredient(ItemID.GoldBar, 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient<CopperWire>(3);
            recipe2.AddIngredient<Plastic>(3);
            recipe2.AddIngredient(ItemID.PlatinumBar, 2);
            recipe2.AddTile(TileID.WorkBenches);
            recipe2.Register();

        }

    }
}
