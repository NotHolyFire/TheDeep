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
    [AutoloadEquip(EquipType.Head)]
    public class SharkScaleHelmet : ModItem
    {

        public static LocalizedText SetBonusText { get; private set; }

        public override void SetStaticDefaults()
        {
            SetBonusText = this.GetLocalization("SetBonus");
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(silver: 25);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 3;
        }

        // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<SharkScaleBreastplate>() && legs.type == ModContent.ItemType<SharkScaleLeggings>();
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 5;
            player.GetModPlayer<SubmergedStats>().Hostility += 5;
        }

        // UpdateArmorSet allows you to give set bonuses to the armor.
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = SetBonusText.Value;
            player.GetModPlayer<SubmergedStats>().Hostility += 5;
            player.GetDamage(DamageClass.Generic) += 0.06f;
            player.GetCritChance(DamageClass.Generic) += 2f;
        }

    }
}
