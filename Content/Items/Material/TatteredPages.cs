using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDeep.Content.Items.Material
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    public class TatteredPages : ModItem
    {

        public override void SetDefaults()
        {
            // ModContent.TileType returns the ID of the tile that this item should place when used. ModContent.TileType<T>() method returns an integer ID of the tile provided to it through its generic type argument (the type in angle brackets)
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 3;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.buyPrice(copper: 0);
        }
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 3;
            ItemID.Sets.SortingPriorityMaterials[Item.type] = 712; // Influences the inventory sort order. 59 is PlatinumBar, higher is more valuable.
        }
    }
}
