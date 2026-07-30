using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDeep.Content.Items.Material;

namespace TheDeep.Content.Items.Armor.ReefKnight
{
    [AutoloadEquip(EquipType.Legs)]
    public class ReefKnightLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {

            if (Main.netMode != NetmodeID.Server)
            {
                var equipSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Legs);
                ArmorIDs.Legs.Sets.HidesBottomSkin[equipSlot] = true;
            }
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = Item.sellPrice(silver: 5);
            Item.rare = ItemRarityID.Green;
            Item.defense = 8;
        }


        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.08f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Coral, 5);
            recipe.AddIngredient<ReefGoldBar>(15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
