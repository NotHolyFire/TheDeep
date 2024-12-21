using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Items.Fish;

namespace TheDeep.Content.Items.Misc
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    public class MagnetBait : ModItem
    {

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 5;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.bait = 10;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 2);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(2);
            recipe.AddIngredient(ItemID.CopperBar, 1);
            recipe.AddRecipeGroup("IronBar", 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            Recipe recipe2 = CreateRecipe(2);
            recipe2.AddIngredient(ItemID.TinBar, 1);
            recipe2.AddRecipeGroup("IronBar", 2);
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();

        }
    }
}
