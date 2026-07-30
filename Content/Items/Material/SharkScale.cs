using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Tiles.Metals;

namespace TheDeep.Content.Items.Material
{
    public class SharkScale : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Blue;
            Item.value = 100;
        }
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 50;
            ItemID.Sets.SortingPriorityMaterials[Item.type] = 50; 
        }
    }
}
