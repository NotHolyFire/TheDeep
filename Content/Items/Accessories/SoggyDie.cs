using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace TheDeep.Content.Items.Accessories {
public class SoggyDie : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 24;
        Item.height = 26;
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.buyPrice(silver: 1);
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.luck += 0.1f;
    }
}
}