using TheDeep.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Tiles.Metals;


namespace TheDeep.Content.Items.Material
{
    public class ReefGoldBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
            ItemID.Sets.SortingPriorityMaterials[Item.type] = 60;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 28;
            Item.value = Item.sellPrice(silver: 2);
            Item.rare = ItemRarityID.Green;
            Item.DefaultToPlaceableTile(ModContent.TileType<ReefGoldBarTile>());
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GoldBar, 1); //this gonna change later
            recipe.AddIngredient(ItemID.Coral, 10); //this gonna change later aswell
            recipe.AddTile(TileID.Anvils); //there will be a new crafting station in the future
            recipe.Register();

        }
    }
}