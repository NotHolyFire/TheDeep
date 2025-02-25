using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDeep.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    public class WhitePearlNecklace : ModItem
    {



        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 26;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(gold: 1);
            Item.accessory = true;
        }

        public override void Load()
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            EquipLoader.AddEquipTexture(Mod, "TheDeep/Content/Items/Accessories/WhitePearlNecklace_Neck", EquipType.Neck, this);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.luck += 0.04f;
            player.GetCritChance(DamageClass.Generic) += 0.03f;

        }
    }
}