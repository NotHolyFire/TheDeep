using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;
using TheDeep.Content.Tiles.Banners;

namespace TheDeep.Content.Items.Placeables
{
    public class ScubaSlimeBanner : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<EnemyBanner>(), (int)EnemyBanner.StyleID.ScubaSlime);
            Item.width = 10;
            Item.height = 24;
            Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(silver: 10));
        }
    }
}
