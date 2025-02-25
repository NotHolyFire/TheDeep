using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace TheDeep.Content.Items.Accessories {
    [AutoloadEquip(EquipType.Neck)]
    public class BlackPearlNecklace : ModItem
{


        public override void SetStaticDefaults()
        {
            int equipSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Neck);
        }

        public override void SetDefaults()
    {
        Item.width = 24;
        Item.height = 26;
        Item.rare = ItemRarityID.Green;
        Item.value = Item.buyPrice(gold: 3);
        Item.accessory = true;
    }

        public override void Load()
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            EquipLoader.AddEquipTexture(Mod, "TheDeep/Content/Items/Accessories/BlackPearlNecklace_Neck", EquipType.Neck, this);
        }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
            player.luck += 0.08f;
            player.GetCritChance(DamageClass.Generic) += 0.06f;

        }
}
}