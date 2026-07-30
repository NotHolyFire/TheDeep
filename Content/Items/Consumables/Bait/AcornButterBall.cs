using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Items.Fish;

namespace TheDeep.Content.Items.Consumables.Bait
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    public class AcornButterBall : ModItem
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
            Item.bait = 5;
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(copper: 0);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(2);
            recipe.AddIngredient(ItemID.Acorn, 1);
            recipe.AddIngredient(ItemID.Gel, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();

        }
    }
}
