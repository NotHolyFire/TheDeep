using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDeep.Content.Items.Material;

namespace TheDeep.Content.Items.Armor.ReefKnight
{
    [AutoloadEquip(EquipType.Head)]
    public class ReefKnightHelmet : ModItem
    {
        public static LocalizedText SetBonusText { get; private set; }

        public override void SetStaticDefaults()
        {
            SetBonusText = this.GetLocalization("SetBonus");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.value = Item.sellPrice(silver: 5);
            Item.rare = ItemRarityID.Green;
            Item.defense = 6;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ReefKnightChestplate>() && legs.type == ModContent.ItemType<ReefKnightLeggings>();
        }
        public override void UpdateEquip(Player player)
        {
            player.accDivingHelm = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = SetBonusText.Value;
            player.ignoreWater = true;
            player.accFlipper = true;
            player.moveSpeed += 0.10f;
            player.GetDamage(DamageClass.Generic) += 5 / 100f;
            player.armorEffectDrawShadow = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ShellPileBlock, 25);
            recipe.AddIngredient<ReefGoldBar>(12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }

    }
}

