
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Items.Material;
using TheDeep.Content.Tiles;

namespace TheDeep.Content.Items.Placeables
{
    public class ReefGoldBrick : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<ReefGoldBrickTile>());
            Item.width = 12;
            Item.height = 12;
        }
        public override void AddRecipes()
        {
            CreateRecipe(5)
                .AddIngredient(ItemID.StoneBlock, 5)
                .AddIngredient<ReefGoldBar>()
                .AddTile(TileID.Furnaces)
                .Register();
        }
    }
}