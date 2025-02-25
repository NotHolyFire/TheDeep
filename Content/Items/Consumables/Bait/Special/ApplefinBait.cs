using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDeep.Content.Items.Fish;

namespace TheDeep.Content.Items.Consumables.Bait.Special
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    public class ApplefinBait : ModItem
    {

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 30;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.bait = 5;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(copper: 0);
        }
    }
}
