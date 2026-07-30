using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TheDeep.Common.SubPlayer;
using TheDeep.Content.Items.Fish;
using TheDeep.Content.Items.Material;

namespace TheDeep.Content.Items.Armor.SharkScale
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    [AutoloadEquip(EquipType.Legs)]
    public class SharkScaleLeggings : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 18; // Width of the item
            Item.height = 18; // Height of the item
            Item.value = Item.sellPrice(silver: 25); // How many coins the item is worth
            Item.rare = ItemRarityID.Blue; // The rarity of the item
            Item.defense = 3; // The amount of defense the item will give when equipped
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 5;
            player.GetModPlayer<SubmergedStats>().Hostility += 5;
        }
    }
}