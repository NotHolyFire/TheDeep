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


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.equipmentBasedLuckBonus += 0.03f;
            player.GetCritChance(DamageClass.Generic) += 0.03f;

        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.WhitePearl, 1);
            recipe.AddIngredient(ItemID.Leather, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}