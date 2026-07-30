using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace TheDeep.Content.Items.Accessories {
    [AutoloadEquip(EquipType.Face)]
    public class CoroaReal : ModItem
{


        public override void SetStaticDefaults()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                int equipSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Face);
                ArmorIDs.Face.Sets.PreventHairDraw[equipSlot] = true;
            }
        }

        public override void SetDefaults()
    {
        Item.width = 24;
        Item.height = 26;
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.buyPrice(gold: 2);
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.statDefense += 2;

            if (player.wet == true)
            {
                player.statDefense += 4;
                player.endurance += 0.1f;
                player.lifeRegen += 1;
                player.accFlipper = true;
            }

    }
}
}