using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Tiles;

namespace TheDeep.Content.Items.Placeables
{
    public class ZincOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
            ItemID.Sets.SortingPriorityMaterials[Item.type] = 58;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<ZincOreTile>());
            Item.width = 12;
            Item.height = 12;
            Item.value = 3000;
        }
    }
}