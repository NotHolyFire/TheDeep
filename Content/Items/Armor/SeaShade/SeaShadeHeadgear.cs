using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TheDeep.Content.Items.Fish;
using TheDeep.Content.Items.Material;

namespace TheDeep.Content.Items.Armor.SeaShade
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    [AutoloadEquip(EquipType.Head)]
    public class SeaShadeHeadgear : ModItem
    {

        public static LocalizedText SetBonusText { get; private set; }

        public override void SetStaticDefaults()
        {
            SetBonusText = this.GetLocalization("SetBonus");
        }

        public override void SetDefaults()
        {
            Item.width = 18; // Width of the item
            Item.height = 18; // Height of the item
            Item.value = Item.sellPrice(silver: 25); // How many coins the item is worth
            Item.rare = ItemRarityID.Blue; // The rarity of the item
            Item.defense = 5; // The amount of defense the item will give when equipped
        }

        // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<SeaShadeChestplate>() && legs.type == ModContent.ItemType<SeaShadeLeggings>();
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.08f;
        }

        // UpdateArmorSet allows you to give set bonuses to the armor.
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = SetBonusText.Value;
            player.accFlipper = true;
            player.moveSpeed += 0.10f;
            player.GetDamage(DamageClass.Generic) += 5 / 100f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<NavalSteelBar>(12);
            recipe.AddIngredient(ItemID.Glass, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}
