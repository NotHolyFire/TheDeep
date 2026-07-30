using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TheDeep.Common.SubPlayer;
using TheDeep.Content.Items.Fish;
using TheDeep.Content.Items.Material;

namespace TheDeep.Content.Items.Armor.SharkScale
{

    [AutoloadEquip(EquipType.Body)]
    public class SharkScaleBreastplate : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(silver: 25);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 5;
            player.GetModPlayer<SubmergedStats>().Hostility += 5;
        }
    }
}