using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace TheDeep.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    public class PinkPearlNecklace : ModItem
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

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.equipmentBasedLuckBonus += 0.12f;
            player.GetCritChance(DamageClass.Generic) += 0.10f;

        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PinkPearl, 3);
            recipe.AddIngredient(ItemID.GoldBar, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.PinkPearl, 3);
            recipe2.AddIngredient(ItemID.PlatinumBar, 5);
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();
        }
    }
}